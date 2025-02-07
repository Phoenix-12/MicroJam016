using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    private void Start()
    {
        transform.position = _target.position;
    }

    private void LateUpdate() 
    {
        transform.position = _target.position * _speed;
    }
}
