using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class MenuButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Canvas _menuCanvas;
    private Animator _animator;
    private AudioSource _audioSource;
    public static UnityEvent StartedGame = new UnityEvent();


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAnimationStartGame() 
    {
        StartedGame.Invoke();
        _startButton.SetActive(false);
        _audioSource.Play();
        _animator.Play(AnimationsConstants.STARTGAME);
    } 

    public void OffMenu() => _menuCanvas.enabled = false;
}