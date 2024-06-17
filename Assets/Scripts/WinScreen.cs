using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Player _player;

    public void Show()
    {
        _winCanvas.enabled = true;
    }

    public void Restart()
    {
      //  if (_canvas.isActiveAndEnabled)
       // {
          //  if (Input.GetKeyDown(KeyCode.KeypadEnter))
          //  {
                _player.RestartLevel();
                _winCanvas.enabled=false;
                _input.enabled = true;
          //  }
       // }
    }

    private void Awake()
    {
        _winCanvas = GetComponent<Canvas>();
    }

}