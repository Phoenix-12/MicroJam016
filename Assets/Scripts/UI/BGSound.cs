using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSound : MonoBehaviour
{
    [SerializeField] private Text _text;
    private Slider _slider;
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable() => MenuButtonsManager.StartedGame.AddListener(OffSlider);

    private void OnDisable() => MenuButtonsManager.StartedGame.RemoveListener(OffSlider);

    private void OffSlider()
    {
        gameObject.SetActive(false);
        _text.enabled = false;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        SetSound(); 
    }

    public void SetSound() 
    {
        for(int i= 0; i < _audioSources.Count; i++)
        {
            _audioSources[i].volume = _slider.value;
        }
    } 
}