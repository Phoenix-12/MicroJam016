using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPosition
{
    public static Transform Transform;

    public static void Start()
    {
        
    }

    public static float SleepDistance = 20f;

    public static float GetDistance(Vector3 pos)
    {
        return Vector3.Distance(pos, Transform.position);
    }
}
