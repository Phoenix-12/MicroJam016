using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketEffector : MonoBehaviour
{
    public float TimerExplosionMax;
    private SpriteRenderer _spriteRocket;
    private float _timerExplosion;
    private Vector3 _startScale;
    private Vector3 _endScale;

    //var minimum = 10.0;
    //var maximum = 20.0;
    // Fades from minimum to maximum in one second


    private void Start()
    {
        _spriteRocket = GetComponent<SpriteRenderer>();
        _timerExplosion = TimerExplosionMax;
        _startScale = transform.localScale;
        _endScale = _startScale * 2;
    }



    private void FixedUpdate()
    {
        Debug.Log($"{_timerExplosion} - {TimerExplosionMax}");
        if (_timerExplosion < TimerExplosionMax / 2)
        {
            _spriteRocket.color = Color.red;

            //transform.localScale = _startScale * Mathf.Lerp(_startScale.x, _endScale.x, Time.fixedDeltaTime + TimerExplosionMax / 2);
        }
        _timerExplosion -= Time.fixedDeltaTime;
    }
}
