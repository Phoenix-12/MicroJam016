using SpaceUtils.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent))]
public class TruckAI : MonoBehaviour
{
    [SerializeField] private int GemCount = 0;
    [SerializeField] private TruckState _state;

    [SerializeField] private float _turningSpeed = 5f;

    [Header("Roaming Settings")]
    [SerializeField] private float roamingDistanceMin;
    [SerializeField] private float roamingDistanceMax;
    [SerializeField] private float _roamingTimerMax = 4f;
    [SerializeField] private float _roamingTime = 0;

    private NavMeshAgent _agent;
    private IControllable _controllable;

    private Vector3 _posToRoam;

    internal void GiveGem()
    {
        GemCount++;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _state = TruckState.Idle;
        _controllable = GetComponent<IControllable>();
    }

    private void Update()
    {
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
            Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
