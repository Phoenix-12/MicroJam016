using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemList : MonoBehaviour
{
    [SerializeField] private List<Gem> _gems;

    internal Gem GetNearestGem(Vector3 position)
    {
        Gem nearest = null;
        float min_dist = 10000f;
        foreach (var gem in _gems)
        {
            if (gem.Collected == false)
            {
                //Debug.Log(gem.Collected);
                var dist = Vector3.Distance(position, gem.transform.position);
                if (dist < min_dist)
                {
                    nearest = gem;
                    min_dist = dist;
                }
            }
        }
        
        return nearest;
    }
}
