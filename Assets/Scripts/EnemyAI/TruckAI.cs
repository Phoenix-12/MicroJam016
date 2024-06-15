using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAI : MonoBehaviour
{
    [SerializeField] private int GemCount = 0;
    internal void GiveGem()
    {
        GemCount++;
    }
}
