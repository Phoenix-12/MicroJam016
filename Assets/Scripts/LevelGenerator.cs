using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _all;

    [SerializeField] private GameObject _meteorPref;
    [SerializeField] private GameObject _truckPref;
    [SerializeField] private GameObject _piratePref;
    [SerializeField] private GameObject _oxygenPref;

    [SerializeField] private GameObject _generator;

    [SerializeField] private GameObject _player;

    [Header("TRUCK")]
    [SerializeField] private int _maxTruckCount = 4;
    [SerializeField] private float _minTruckRadius = 10f;
    [SerializeField] private float _maxTruckRadius = 40f;

    [Header("METEOR")]
    [SerializeField] private int _maxMeteorCount = 6;
    [SerializeField] private float _minMeteorRadius;
    [SerializeField] private float _maxMeteorRadius;
    private int _oxyChankCount = 6;

    private List<GameObject> trucks = new List<GameObject>();

    private List<GameObject> _allGamoObjects = new List<GameObject>();

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //_all.SetActive(false);
        GemList.getInstance().Initialize();
        gameObject.SetActive(false);
        //Instantiate(_generator, Vector3.zero, Quaternion.identity);
        //Destroy(gameObject);
    }

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        if (_generator == null)
            _generator = GameObject.Find("GENERATOR");
        PlayerPosition.Transform = _player.transform;
        GemList.getInstance().Initialize();
        StartLevel();
    }

    private void StartLevel()
    {
        var navi = _player.GetComponent<Navigator>();
        
    
        var min = 0f;
        var max = 2 * Mathf.PI / _maxTruckCount;
        var step = 2 * Mathf.PI / _maxTruckCount;
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
            var truck = Instantiate(_truckPref,  truckPos, Quaternion.identity, transform.transform);
            _allGamoObjects.Add(truck);
            foreach (var obj in truck.GetComponent<TruckAI>().Miners)
            {
                obj.transform.parent = transform.transform;
            }
            trucks.Add(truck);
            for(int j = 0; j < _maxMeteorCount; ++j)
            {
                var rotM = UnityEngine.Random.Range(minM, maxM);
                var distM = UnityEngine.Random.Range(_minMeteorRadius, _maxMeteorRadius);
                minM += stepM;
                maxM += stepM;
                Vector3 meteorPos = new Vector3(Mathf.Cos(rotM), Mathf.Sin(rotM), 0) * distM;
                var met = Instantiate(_meteorPref, truckPos + meteorPos, Quaternion.identity, transform.transform);
                var startScale = met.transform.localScale;
                met.transform.localScale = startScale * UnityEngine.Random.Range(0.5f, startScale.x);
                _allGamoObjects.Add(met);
                //met.GetComponent<Meteora>();
            }
        }
        navi.Targets = new List<GameObject>();
        navi.Targets = trucks;

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
                Instantiate(_oxygenPref, oxySpawnPos, Quaternion.identity, transform.transform);
            }
        }
    }
}
