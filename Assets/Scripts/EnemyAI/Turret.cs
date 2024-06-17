using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;

    [SerializeField] private float _maxReloadTime = 1.0f;
    [SerializeField] private float _reloadTimer = 0.0f;

    private Transform _playerTransform = PlayerPosition.Transform;



    private void FixedUpdate()
    {
        transform.up = _playerTransform.position - transform.position + SpaceUtils.Utils.Utils.GetRandomDir() * 2f;
        if (_reloadTimer < 0.0f)
        {
            
            if (PlayerPosition.GetDistance(transform.position) < 10f)
            {
                Shoot();
            }
            _reloadTimer = _maxReloadTime;
        }
        _reloadTimer -= Time.fixedDeltaTime;
    }

    private void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, _bulletSpawn.rotation);
        var component = bullet.GetComponent<TruckBullet>();
        component.Boomtime = PlayerPosition.GetDistance(transform.position) / component.Speed;
    }
}
