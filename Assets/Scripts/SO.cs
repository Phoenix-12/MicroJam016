using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SO : ScriptableObject
{
    public List<Gem> gemList;
    public Transform playerTransform;
}