using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public bool Collected = false;
    public void Pickup()
    {
        Debug.Log("Gem Pickuped!");
        Collected = true;
        gameObject.SetActive(false);
    }
}
