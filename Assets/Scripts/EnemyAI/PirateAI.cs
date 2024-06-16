using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent))]
public class PirateAI : MonoBehaviour, IPirate
{
    public bool IsSeeMiner()
    {
        throw new System.NotImplementedException();
    }

    public bool IsSeePlayer()
    {
        throw new System.NotImplementedException();
    }

    public bool IsSeeTruck()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
