using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Minigem : MonoBehaviour
{
    //private Rigidbody2D _rb;
    public Transform Target;
    private float _offsetTime = 0;

    private void Awake()
    {
        _offsetTime = UnityEngine.Random.Range(0, 10f);
        //_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float k = 0.1f;
        float R = 1.5f * k;
        float r = 0.66f * k;
        float h = 1.3f * k;
        float fi = (Time.time + _offsetTime) * 2f;
        var x = (R - r) * Mathf.Cos(fi) + h * Mathf.Cos(((R - r) / r) * fi);
        var y = (R - r) * Mathf.Sin(fi) - h * Mathf.Sin(((R - r) / r) * fi);
        transform.position = Target.position + new Vector3(x, y, 0);
    }

    public bool TryPickup()
    {
        if (transform.parent == null)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}
