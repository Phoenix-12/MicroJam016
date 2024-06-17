using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public static class DataHolder
{
    private static GameObject prefabName;

    public static GameObject Prefab
    {
        get
        {
            return prefabName;
        }
        set
        {
            prefabName = value;
        }
    }
}*/

public class GemList
{
    private static List<Gem> _gems = new List<Gem>();

    public static void Start()
    {
        _gems.Clear();
    }

    public static void AddGem(Gem gem)
    {
        _gems.Add(gem);
    }

    public static Gem GetNearestGem(Vector3 position)
    {
        //Debug.Log(_gems.Count);
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
