using SpaceUtils.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Oxygen : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    private Rigidbody2D _rb;

    public void Pickup()
    {
        //Effect Oxygen
        Destroy(gameObject);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Utils.GetRandomDir() * _speed;
        _rb.rotation = UnityEngine.Random.Range(-180, 180);
    }

    void FixedUpdate()
    {
        _rb.rotation += 10f * Time.fixedDeltaTime;
    }
}
