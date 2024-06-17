using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public bool Collected = false;
    public void Pickup()
    {
        Collected = true;
        gameObject.SetActive(false);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
