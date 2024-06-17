using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform _center;
    private Transform _playerTransform;
    private float _startScale;
    private Vector3 _startScaleVector;
    private float _endScale;
    private float t;

    [SerializeField] private WinScreen _winScreen;

    private void Awake()
    {
        //_winScreen = GetComponent<WinScreen>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            if (player.GemCounter > player.NeedGemCount)
            {
                if(collision.TryGetComponent<PlayerInput>(out PlayerInput playerInput)){
                    playerInput.enabled = false;
                }
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                player.GetComponent<Rigidbody2D>().isKinematic = true;
                _playerTransform = player.transform;
                collision.gameObject.transform.position = _center.transform.position;
                //_startScale = player.transform.localScale.x;
                //_startScaleVector = player.transform.localScale;
                //_endScale = player.transform.localScale.x * 1f;
                Debug.Log("WIN!!!!");
                _winScreen.Show();
            }
            
        }
    }
    private void FixedUpdate()
    {

        if (_playerTransform != null)
        {
            //_playerTransform.position = _center.position;
            //_playerTransform.localScale = _startScaleVector * Mathf.Lerp(_startScale, _endScale, 0.2f);//t);
            //t += 1f * Time.fixedDeltaTime;
            //_playerTransform.Rotate(0, 0, 6f * Time.fixedDeltaTime);
        }
    }
}
