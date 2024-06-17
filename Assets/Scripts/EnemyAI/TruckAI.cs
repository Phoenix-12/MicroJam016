using SpaceUtils.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent), typeof(Enemy))]
public class TruckAI : MonoBehaviour
{
    //[SerializeField] private GemList _gemList;

    [Header("Miner Settings")]
    [SerializeField] public List<GameObject> Miners;
    [SerializeField] private GameObject _minerPrefab;
    [SerializeField] private int _minMinersCount;
    [SerializeField] private int _maxMinersCount;

    [Header("Roaming Settings")]
    [SerializeField] private float _roamingDistanceMin;
    [SerializeField] private float _roamingDistanceMax;
    [SerializeField] private float _roamingTimerMax = 4f;
    [SerializeField] private float _roamingTime = 0;

    [SerializeField] private int GemCount = 0;
    [SerializeField] private TruckState _state;

    [SerializeField] private float _turningSpeed = 5f;
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private IControllable _controllable;

    private Vector3 _posToRoam;


    internal void GiveGem()
    {
        GemCount++;
        _enemy.Gem++;
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        var truckComponent = GetComponent<TruckAI>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _state = TruckState.Idle;
        _controllable = GetComponent<IControllable>();

        for(int i = 0; i < UnityEngine.Random.Range(_minMinersCount, _maxMinersCount); ++i)
        {
            var obj = Instantiate(_minerPrefab, transform.position +
                Utils.GetRandomDir() * UnityEngine.Random.Range(5f, 10f),
                Quaternion.identity);
            var miner = obj.GetComponent<MinerAI>(); 
            miner.enabled = true;
            //miner.GemList = GemList;
            miner.Truck = truckComponent;
            Miners.Add(obj);
        }
    }

    private void Update()
    {
        if (PlayerPosition.GetDistance(transform.position) > PlayerPosition.SleepDistance)
            return;
        switch (_state)
        {
            default:
            case TruckState.Idle:
                _state = TruckState.Roaming;
                break;

            case TruckState.Roaming:
                if (IsSeeEnemy())
                    _state = TruckState.AttackEnemy;
                else
                {
                    _roamingTime -= Time.deltaTime;
                    if (_roamingTime < 0)
                    {
                        _posToRoam = GetSearchPosition();
                        GoToPosition(_posToRoam);
                        _roamingTime = _roamingTimerMax;
                    }
                    RotateToVec(_posToRoam);
                }
                break;
            case TruckState.AttackEnemy:
                break;
        }
    }

    private bool IsSeeEnemy()
    {
        return false;
    }

    private void GoToPosition(Vector3 pos)
    {
        _agent.SetDestination(pos);
        RotateToVec(pos);
    }
    private void RotateToVec(Vector3 pos) => transform.up = Vector2.MoveTowards(transform.up, pos - transform.position, Time.deltaTime * _turningSpeed);

    private Vector3 GetSearchPosition()
    {
        return transform.position +
            Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }
}
