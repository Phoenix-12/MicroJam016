using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cam;
    private static float t = 0.0f;
    private float start;
    private bool _isSet = false;
    private float _fovIndex;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }


    public void SetNewScale(float fovIndex)
    {
        _isSet = true;
        _fovIndex = fovIndex;
        start = _cam.m_Lens.OrthographicSize;
        
        //_cam.m_Lens.OrthographicSize = fovIndex;
    }

    private void FixedUpdate() 
    {   
        if (_isSet)
        {
            _cam.m_Lens.OrthographicSize = Mathf.Lerp(start, _fovIndex, t);
            t += 0.5f * Time.fixedDeltaTime;
        }
    }


}
