using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _boomEffect;
    [SerializeField] private float _speed;
    private float _lifeTime = 10f;
    private Rigidbody2D _rb;


    private void Awake()
    {
        //_boomEffect.transform.localScale = new Vector3(1f, 1f, 1f);
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _lifeTime -= Time.fixedDeltaTime;
        _rb.velocity = transform.up * _speed;
        if (_lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Hit();
            
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        BoomEffect();
    }

    private void BoomEffect()
    {
        var go = Instantiate(_boomEffect, transform.position, Quaternion.identity);
        //go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Destroy(go, 0.3f);

    }
}
