using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


internal interface IPlayerControllable
{
    void Move(Vector2 direction);
    void Shoot();
    void Dodge();
}