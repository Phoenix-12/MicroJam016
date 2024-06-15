using UnityEngine;
using UnityEngine.UI;

public class BGSound : MonoBehaviour
{
    [SerializeField] private Text _text;
    private Slider _slider;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable() => MenuButtonsManager.StartedGame.AddListener(OffSlider);

    private void OnDisable() => MenuButtonsManager.StartedGame.RemoveListener(OffSlider);

    private void OffSlider()
    {
        gameObject.SetActive(false);
        _text.enabled = false;
    }

    private void Awake() => _slider = GetComponent<Slider>();

    public void SetSound() => _audioSource.volume = _slider.value;
}