using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MenuButtonsManager : MonoBehaviour
{
    public static UnityEvent StartedGame = new UnityEvent();

    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private PlayerInput _playerInput;

    private Animator _animator;
    private AudioSource _audioSourceMenu;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSourceMenu = GetComponent<AudioSource>();
    }

    public void PlayAnimationStartGame() 
    {
        StartedGame.Invoke();
        _startButton.SetActive(false);
        _audioSourceMenu.Play();
        _animator.Play(AnimationsConstants.STARTGAME);
        _cameraManager.SetNewScale(7);
        _playerInput.enabled = true;
    } 

    public void OffMenu() => _menuCanvas.enabled = false;
}