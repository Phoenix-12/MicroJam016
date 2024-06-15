using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    [Header("KissBar")]
    [SerializeField] private Slider _kis;

    [Header("HealthBar")]
    [SerializeField] private Slider _healthBar;

    [Header("Energy")]
    [SerializeField] private Slider _energyBar;
        
    private void SetMaxFill(int resource, Slider slider)
    {
        slider.maxValue = resource;
        slider.value = resource;
    }

    private void SetFill(int resource, Slider slider ) => slider.value = resource;


    public void SetKis(int resource) => SetFill(resource, _kis);
    public void SetMaxKis(int resource) => SetMaxFill(resource, _kis);


    public void SetHealthBar(int resource) => SetFill(resource, _healthBar);
    public void SetMaxHealthBar(int resource) => SetMaxFill(resource, _healthBar);


    public void SetEnergy(int resource) => SetFill(resource, _energyBar);
    public void SetMaxEnergy(int resource) => SetMaxFill(resource, _energyBar);

}