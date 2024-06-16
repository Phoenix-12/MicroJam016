using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IPlayerControllable 
{
    public event Action BulletHited;

    private Rigidbody2D _rb;
    [Header("IN GAME STATS")]
    [SerializeField] private float _oxygen;
    [SerializeField] private int _gemCounter = 0;

    [Header("Properties")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveVelocity; 
    [SerializeField] private float _oxygenBuff = 10f;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnLeft;
    [SerializeField] private Transform _bulletSpawnRight;
    [SerializeField] private Transform _spawnPointTransform;
    
    

    
    private float _reloadTimeMax = 0.5f;
    private float _reloadTimer = 0f;

    //public Vector2 MoveDirection;
    private bool _isLeftGunShoot;
    

    public void TakeBulletHit()
    {
        BulletHited?.Invoke();
    }

    private void Awake()
    {
        PlayerPosition.Transform = transform;
        _rb = GetComponent<Rigidbody2D>();
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
            _reloadTimer = _reloadTimeMax;
        }
        Debug.Log("SHOOT!");
    }


    internal void PlaceOnSpawn()
    {
        transform.position = _spawnPointTransform.position;
        transform.rotation = _spawnPointTransform.rotation;
    }

    private void FixedUpdate()
    {
        _reloadTimer -= Time.fixedDeltaTime;
        _oxygen -= Time.fixedDeltaTime;
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Minigem>(out Minigem gem))
        {
            if (gem.TryPickup())
            {
                _gemCounter++;
                //UPDATE UI
            }
        }
        if(collision.collider.TryGetComponent<Oxygen>(out var oxy))
        {
            _oxygen += _oxygenBuff;
            oxy.Pickup();
        }
    }
}
