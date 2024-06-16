using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceUtils.Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteora : MonoBehaviour
{
    [SerializeField] private int _minCountGem;
    [SerializeField] private int _maxCountGem;

    [SerializeField] private float _speed;
    
    [SerializeField] private int _countGem;

    //[SerializeField] private GemList GemList;

    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private List<Transform> _gemSpawns = new List<Transform>();

    private Rigidbody2D _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _countGem = UnityEngine.Random.Range(_minCountGem, _maxCountGem);
        var countGemTmp = _countGem;
        foreach(var gemTransform in _gemSpawns)
        {
            if (countGemTmp > 0)
            {
                var gem = Instantiate(_gemPrefab, gemTransform, false);
                countGemTmp--;
                GemList.AddGem(gem.GetComponent<Gem>());
            }
            else break;
        }
        _rb.velocity = Utils.GetRandomDir() * _speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.rotation += 10f * Time.fixedDeltaTime;
    }
}
