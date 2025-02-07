using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CanvasWorldFollower : MonoBehaviour
    {
        private float _offset;

        private void Awake()
        {
            _offset = (transform.parent.position - transform.position).magnitude;
        }

        private void FixedUpdate()
        {
            transform.position = transform.parent.position + Vector3.down * _offset;
            transform.rotation = Quaternion.identity;
        }
    }
}