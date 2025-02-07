using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private static GemList instance;

    private GemList()
    { 
    
    }

    public static GemList getInstance()
    {
        if (instance == null)
            instance = new GemList();
        return instance;
    }




    private static List<Gem> _gems = new List<Gem>();

    public void Initialize()
    {
        _gems.Clear();
    }

    public void AddGem(Gem gem)
    {
        _gems.Add(gem);
    }

    public Gem GetNearestGem(Vector3 position)
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
