//using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SpaceUtils.Utils;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent), typeof(Enemy))]
public class MinerAI : MonoBehaviour, IMiner
{
    //[SerializeField] public GemList GemList;
    [SerializeField] public TruckAI Truck;

    [SerializeField] private bool _haveGem = false;
    [SerializeField] private MinerState _state;
    

    [SerializeField] private float _seeDist = 10000f;
    [SerializeField] private float _pickupDist;
    [SerializeField] private float _giveDist = 10f;
    [SerializeField] private float _roamingDistanceMin;
    [SerializeField] private float _roamingDistanceMax;

    [SerializeField] private float _searchTimerMax = 4f;
    
    private float _searchTime = 0;
    private NavMeshAgent _agent;
    private Gem _currentGem;
    private Transform _truckTransform;
    private IControllable _controllable;

    private Vector3 _posToRoam;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _truckTransform = Truck.transform;
        _state = MinerState.Idle;
        _controllable = GetComponent<IControllable>();
    }

    private void Update()
    {
        if (PlayerPosition.GetDistance(transform.position) > PlayerPosition.SleepDistance)
            return;
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
                    GoToRoamPosition();
                    
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
                    _currentGem = GemList.getInstance().GetNearestGem(transform.position);
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
                if (Truck.isActiveAndEnabled)
                {
                    if (Vector3.Distance(_truckTransform.position, transform.position) < _giveDist)
                    {
                        Truck.GiveGem();
                        _enemy.Gem--;
                        _haveGem = false;
                        _state = MinerState.SearchGem;
                    }
                    else
                    {
                        GoToTruck();
                    }
                    break;
                }
                else
                {
                    _state = MinerState.Roaming;
                }
                break;

            case MinerState.Roaming:
                GoToRoamPosition();
                break;
                

            case MinerState.Pickup:
                _currentGem.Pickup();
                _haveGem = true;
                _enemy.Gem++;
                _state = MinerState.ReturnGem;
                break;
        }
    }

    private void GoToRoamPosition()
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
        //Debug.Log(transform.position);
        var nearestGem = GemList.getInstance().GetNearestGem(transform.position);
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
        return Truck.transform.position + 
            Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }
}
