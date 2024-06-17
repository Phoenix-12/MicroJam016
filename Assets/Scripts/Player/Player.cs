using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IPlayerControllable 
{
    public event Action GameRestarted;
    //public event Action BulletHited;
    public int GemCounter = 0;
    public int NeedGemCount = 20;



    [SerializeField] private GameObject _minigemPrefab;
    [SerializeField] private Transform _gemHolderPosition;
    private List<GameObject> _gems = new List<GameObject>();
    private int _gemRendered;



    [Header("IN GAME STATS")]

    [SerializeField] private float _maxOxygen = 100;
    [SerializeField] private float _oxygen;
    
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _health = 0;

    [Header("Properties")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveVelocity; 
    [SerializeField] private float _oxygenBuff = 10f;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnLeft;
    [SerializeField] private Transform _bulletSpawnRight;
    [SerializeField] private Transform _spawnPointTransform;

    [Header("Other")]
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameObject _levelGeneratorPrefab;
    [SerializeField] private BarManager _barManager;
    [SerializeField] private Score _scoreView;
    [SerializeField] private GemsViewCounter _gemsCounterView;
    [SerializeField] private Navigator _navigator;

    private AudioSource _audio;
    private Rigidbody2D _rb;


    private float _reloadTimeMax = 0.5f;
    private float _reloadTimer = 0f;

    //public Vector2 MoveDirection;
    private bool _isLeftGunShoot;
   

    private void Awake()
    {
        PlayerPosition.Transform = transform;
        GameRestarted += RestartLevel;

        _navigator = GetComponent<Navigator>();
        _audio = GetComponent<AudioSource>();
        _oxygen = _maxOxygen;
        _scoreView = GetComponent<Score>();
        _rb = GetComponent<Rigidbody2D>();
        _barManager = GetComponent<BarManager>();
        _barManager.SetMaxOxygen((int)_maxOxygen);
        _barManager.SetMaxHealthBar((int)_maxHealth);
        _health = _maxHealth;
        _gemsCounterView.UpdateGems(GemCounter, NeedGemCount);
    }


    public void TakeBulletHit()
    {
        _health--;
        if (_health <= 0)
        {
            _health = 0;
            GameRestarted?.Invoke();
            
        }
        _barManager.SetHealthBar(_health);
    }

    public void Move(Vector2 velocity)
    {
        _rb.velocity = (Vector3.up * velocity.y + Vector3.right * velocity.x) * _moveVelocity;
    }

    public void SetPosition(Vector2 position, float rotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void Shoot()
    {
        if (_reloadTimer < 0)
        {
            if (_isLeftGunShoot)
            {
                Instantiate(_bullet, _bulletSpawnLeft.position, _bulletSpawnLeft.rotation);
                _isLeftGunShoot = false;
            }
            else
            {
                Instantiate(_bullet, _bulletSpawnRight.position, _bulletSpawnRight.rotation);
                _isLeftGunShoot = true;
            }
            _audio.Play();
            _reloadTimer = _reloadTimeMax;
        }
    }

    public void Dodge()
    {
        Debug.Log("Dodge");
    }


    internal void PlaceOnSpawn()
    {
        transform.position = _spawnPointTransform.position;
        transform.rotation = _spawnPointTransform.rotation;
    }

    private void FixedUpdate()
    {
        _scoreView.UpdateScore(GemCounter * 100 + (int)Time.time);
        _reloadTimer -= Time.fixedDeltaTime;
        _oxygen -= Time.fixedDeltaTime;
        _barManager.SetOxygen((int)_oxygen);
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90);

        if(_oxygen < 0)
        {
            GameRestarted?.Invoke();
        }


        if (GemCounter > 0)
        {
            for (int i = 0; i < GemCounter - _gemRendered; i++)
            {
                var gem = Instantiate(_minigemPrefab, _gemHolderPosition, false);
                gem.GetComponent<Minigem>().Target = _gemHolderPosition;
                _gems.Add(gem);
                _gemRendered++;
            }
        }
        if (GemCounter < _gemRendered)
        {
            Destroy(_gems.Last());
            _gems.Remove(_gems.Last());
            _gemRendered--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Gate>(out Gate gate))
        {
            if (GemCounter > NeedGemCount)
            {
                _navigator.HideArrows();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Minigem>(out Minigem gem))
        {
            if (gem.TryPickup())
            {
                GemCounter++;
                _gemsCounterView.UpdateGems(GemCounter, NeedGemCount);
                //UPDATE UI
            }
        }
        if(collision.collider.TryGetComponent<Oxygen>(out var oxy))
        {
            _oxygen += _oxygenBuff;
            if (_oxygen > _maxOxygen)
            {
                _oxygen = _maxOxygen;
            }
            _barManager.SetOxygen((int)_oxygen);
            oxy.Pickup();
        }
    }

    public void RestartLevel()
    {
        _health = _maxHealth;
        _oxygen = _maxOxygen;
        _barManager.SetHealthBar(_health);
        _barManager.SetOxygen((int)_oxygen);
        
        GemCounter = 0;
        _gemsCounterView.UpdateGems(GemCounter, NeedGemCount);
        transform.parent = null;

        foreach(var obj in GameObject.FindGameObjectsWithTag("Generator"))
        {
            if (obj.activeInHierarchy)
            {
                obj.GetComponent<LevelGenerator>().RestartLevel();
            }
        }
        
        transform.position = Vector3.zero;

        //SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
        Instantiate(_levelGeneratorPrefab, transform.position, Quaternion.identity);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Additive);
        //SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("SampleScene");
    }

}
