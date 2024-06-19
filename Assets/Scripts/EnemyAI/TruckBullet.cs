using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class TruckBullet : MonoBehaviour
{
    public float Boomtime;
    public float Speed;
    
    [SerializeField] private float _lifetime = 7f;
    
    private Rigidbody2D _rb;
    private RocketEffector _effector;
    [SerializeField] private float _boomRadius;
    [SerializeField] private GameObject _boomEffect;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = transform.up * Speed;
        Boomtime -= Time.fixedDeltaTime;
        if (Boomtime <= 0)
        {
            Boom();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _boomRadius);
    }

    private void BoomEffect()
    {
        Destroy(Instantiate(_boomEffect, transform.position, Quaternion.identity), 0.3f);
    }

    private void Boom()
    {
        //BOOM EFFECT
        var overlaps = Physics2D.OverlapCircleAll(transform.position, _boomRadius);
        BoomEffect();
        foreach (var overlap in overlaps)
        {
            if (overlap.TryGetComponent<Player>(out Player player))
            {
                player.TakeBulletHit();
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Boom();
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}

