using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _meteorPref;
    [SerializeField] private GameObject _truckPref;
    [SerializeField] private GameObject _piratePref;
    [SerializeField] private GameObject _oxygenPref;

    [Header("TRUCK")]
    [SerializeField] private int _maxTruckCount = 4;
    [SerializeField] private float _minTruckRadius = 10f;
    [SerializeField] private float _maxTruckRadius = 40f;

    [Header("METEOR")]
    [SerializeField] private int _maxMeteorCount = 6;
    [SerializeField] private float _minMeteorRadius;
    [SerializeField] private float _maxMeteorRadius;
    private int _oxyChankCount = 5;

    private void Awake()
    {
        var min = 0f;
        var max = 2 * Mathf.PI * _maxTruckCount;
        var step = 2 * Mathf.PI * _maxTruckCount;
        var minM = 0f;
        var maxM = 2 * Mathf.PI / _maxMeteorCount;
        var stepM = 2 * Mathf.PI / _maxMeteorCount;
        for (int i = 0; i < _maxTruckCount; i++)
        {
            var rot = UnityEngine.Random.Range(min, max);
            var dist = UnityEngine.Random.Range(_minTruckRadius, _maxTruckRadius);
            min += step;
            max += step;
            Vector3 truckPos = new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0) * dist;
            Instantiate(_truckPref,  truckPos, Quaternion.identity);
            for(int j = 0; j < _maxMeteorCount; ++j)
            {
                var rotM = UnityEngine.Random.Range(minM, maxM);
                var distM = UnityEngine.Random.Range(_minMeteorRadius, _maxMeteorRadius);
                minM += stepM;
                maxM += stepM;
                Vector3 meteorPos = new Vector3(Mathf.Cos(rotM), Mathf.Sin(rotM), 0) * distM;
                var met = Instantiate(_meteorPref, truckPos + meteorPos, Quaternion.identity);
                //met.GetComponent<Meteora>();
            }
        }

        var radius = (_maxMeteorRadius + _maxTruckRadius);
        var oxyStep = 2f * radius / _oxyChankCount;

        var startVecOxy = new Vector3(-radius, -radius, 0);
        for(int i = 0; i < _oxyChankCount; ++i)
        {
            for (int j = 0; j < _oxyChankCount; ++j)
            {
                var newStart = startVecOxy + new Vector3(oxyStep * i, oxyStep * j, 0);
                var oxySpawnPos = newStart 
                    + new Vector3(UnityEngine.Random.Range(0, oxyStep), UnityEngine.Random.Range(0, oxyStep), 0);
                Instantiate(_oxygenPref, oxySpawnPos, Quaternion.identity);
            }
        }
    }
}
