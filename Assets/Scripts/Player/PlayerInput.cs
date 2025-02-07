using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private IPlayerControllable _controllable;
    private GameInput _gameInput;
    private Vector2 _directionMove;
    private Vector2 _directionAim;
    private bool _isMobileInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _controllable = GetComponent<IPlayerControllable>();
        _controllable.InputChanged += (a) => _isMobileInput = a;
        /*_controllable = GetComponent<IControllable>();
        if (_controllable != null)
            Debug.Log("No Controller Component");*/
    }

    private void EnableTouch()
    {
        _gameInput.Gameplay.Shoot.performed -= ShootOnPerformed;
        //_gameInput.Gameplay.Dodge.performed -= DodgeOnPerformed;
    }

    private void EnableKeyboard()
    {
        _gameInput.Gameplay.Shoot.performed += ShootOnPerformed;
        //_gameInput.Gameplay.Dodge.performed += DodgeOnPerformed;
    }

    private void OnEnable()
    {
        _gameInput.Enable();
        if (_isMobileInput)
            EnableTouch();
        else
            EnableKeyboard();
    }



    private void OnDisable()
    {
        _gameInput.Disable();
    }


    void Update()
    {
        ReadMovement();
    }

    private void ShootOnPerformed(InputAction.CallbackContext context)
    {
        _controllable.Shoot();
    }
    
    private void DodgeOnPerformed(InputAction.CallbackContext context)
    {
        _controllable.Dodge();
    }

    private void ReadMovement()
    {
        if (_isMobileInput)
        {
            var moveDirection = _gameInput.Touch.Movement.ReadValue<Vector2>();
            var aimDirection = _gameInput.Touch.Aim.ReadValue<Vector2>();
            if (aimDirection != Vector2.zero)
            {
                _directionAim = new Vector2(aimDirection.x, aimDirection.y);
                _controllable.Aim(_directionAim);
                _controllable.Shoot();
            }
            _directionMove = new Vector2(moveDirection.x, moveDirection.y);
            _controllable.Move(_directionMove);
        }
        else
        {
            if (_gameInput.Gameplay.Shoot.IsPressed())
            {
                //_controllable.Shoot();
            }
            //Debug.Log(_gameInput.Gameplay.Aim.ReadValue<Vector2>());
            var moveDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            //var aimDirection = _gameInput.Gameplay.Aim.ReadValue<Vector2>();
            //var turnDirection = _gameInput.Gameplay.Turn.ReadValue<float>();
            _directionMove = new Vector2(moveDirection.x, moveDirection.y);
            //_directionAim = new Vector2(aimDirection.x, aimDirection.y);
            //_controllable.Turn(turnDirection);
            //Debug.Log(_direction);
            //_controllable.Aim(_directionAim);
            _controllable.Move(_directionMove);
        }
    }
}


