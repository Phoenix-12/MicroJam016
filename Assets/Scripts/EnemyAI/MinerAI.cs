//using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SpaceUtils.Utils;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent))]
public class MinerAI : MonoBehaviour, IMiner
{
    

    [SerializeField] private bool _haveGem = false;
    [SerializeField] private MinerState _state;
    
    [SerializeField] private GemList _gemList;
    [SerializeField] private TruckAI _truck;

    [SerializeField] private float _seeDist = 10000f;
    [SerializeField] private float _pickupDist;
    [SerializeField] private float _giveDist = 10f;
    [SerializeField] private float roamingDistanceMin;
    [SerializeField] private float roamingDistanceMax;

    [SerializeField] private float _searchTimerMax = 4f;
    
    private float _searchTime = 0;
    private NavMeshAgent _agent;
    private Gem _currentGem;
    private Transform _truckTransform;
    private IControllable _controllable;

    private Vector3 _posToRoam;
    

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _truckTransform = _truck.transform;
        _state = MinerState.Idle;
        _controllable = GetComponent<IControllable>();
    }

    private void Update()
    {
        switch (_state)
        {
            default:
            case MinerState.Idle:
                _state = MinerState.SearchGem;
                break;

            case MinerState.SearchGem:
                if (IsSeeGem())
                    _state = MinerState.SeeGem;
                else
                {
                    _searchTime -= Time.deltaTime;
                    if (_searchTime < 0)
                    {
                        _posToRoam = GetSearchPosition();
                        GoToPosition(_posToRoam);
                        _searchTime = _searchTimerMax;
                    }
                    RotateToVec(_posToRoam);
                }
                break;

            case MinerState.SeeGem:
                if (IsSeeGem())
                {
                    _state = MinerState.GoToGem;
                }
                else _state = MinerState.SearchGem;
                break;
            
            case MinerState.GoToGem:
                if (IsSeeGem())
                {
                    _currentGem = _gemList.GetNearestGem(transform.position);
                    if (Vector3.Distance(_currentGem.transform.position, transform.position) < _pickupDist)
                    {
                        _state = MinerState.Pickup;
                    }
                    else
                    {
                        GoToPosition(_currentGem.transform.position);
                    }
                }
                else
                    _state = MinerState.SearchGem;
                break;

            case MinerState.ReturnGem:
                if (Vector3.Distance(_truckTransform.position, transform.position) < _giveDist)
                {
                    _truck.GiveGem();
                    _haveGem = false;
                    _state = MinerState.SearchGem;
                }
                else
                {
                    GoToTruck();
                }
                break;

            case MinerState.Pickup:
                _currentGem.Pickup();
                _haveGem = true;
                _state = MinerState.ReturnGem;
                break;
        }
    }
    private void GoToPosition(Vector3 pos)
    {
        _agent.SetDestination(pos);
        RotateToVec(pos);
    }
    private void RotateToVec(Vector3 pos) => transform.up = Vector2.MoveTowards(transform.up, pos - transform.position, Time.deltaTime* 10f);
    private void GoToTruck()
    {
        GoToPosition(_truckTransform.position);
    }

    public bool IsSeeGem()
    {
        var nearestGem = _gemList.GetNearestGem(transform.position);
        if (nearestGem != null)
        {
            var distToNearestGem = Vector3.Distance(nearestGem.transform.position, transform.position);
            if (distToNearestGem < _seeDist)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetSearchPosition()
    {
        return _truck.transform.position + 
            Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
