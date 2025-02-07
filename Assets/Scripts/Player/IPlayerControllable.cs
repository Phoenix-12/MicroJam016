using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


internal interface IPlayerControllable
{
    public event Action<bool> InputChanged;

    void Move(Vector2 direction);
    void Shoot();
    void Dodge();
    void Aim(Vector2 directionAim);
}