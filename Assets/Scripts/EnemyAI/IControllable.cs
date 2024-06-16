using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void LookAt(Vector3 pos);
    void MoveTo(Vector3 pos);
    void Pickup();
}
