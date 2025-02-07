using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<int> HealthChanged;

    public int Health;
    public int Gem;

    [SerializeField] private GameObject _minigemPrefab;
    [SerializeField] private GameObject _oxygenPrefab;
    [SerializeField] private Transform _gemHolderPosition;

    private List<GameObject> _gems = new List<GameObject>();
    private int _gemRendered;
    [SerializeField] private GameObject _boomEffect;

    public void Hit()
    {
        Health -= 1;
        if (Health <= 0)
        {
            Destroy(Instantiate(_boomEffect, transform.position, Quaternion.identity), 0.3f);
            Health = 0;
            if (Gem > 0)
            {
                foreach (GameObject gem in _gems)
                {
                    gem.transform.parent = null;
                }
            }
            if(UnityEngine.Random.Range(0,100) < 40)
            {
                var oxy = Instantiate(_oxygenPrefab, transform.position, Quaternion.identity);
            }

            gameObject.SetActive(false);
        }
        HealthChanged?.Invoke(Health);
    }

    private void FixedUpdate()
    {
        if (Gem > 0) {
            for (int i = 0; i < Gem - _gemRendered; i++)
            {
                var gem = Instantiate(_minigemPrefab, _gemHolderPosition, false);
                gem.GetComponent<Minigem>().Target = _gemHolderPosition;
                _gems.Add(gem);
                _gemRendered++;
            }
        }
        if (Gem < _gemRendered)
        {
            Destroy(_gems.Last());
            _gems.Remove(_gems.Last());
            _gemRendered--;
        }
        
    }
}
