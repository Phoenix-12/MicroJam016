using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public void Show()
    {
        _canvas.enabled = true;
        GetComponent<Canvas>().enabled = true;
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

    }
}