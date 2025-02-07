using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ReactiveEnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;  
        private int _health;
        private int _healthMax;

        [Header("HealthBar")]
        [SerializeField] private Slider _healthBar;

        private void Awake()
        {
            _enemy.HealthChanged += UpdateSlider;
            _healthMax = _enemy.Health;
            UpdateSlider(_healthMax);
        }

        private void OnDestroy()
        {
            _enemy.HealthChanged -= UpdateSlider;
        }

        private void UpdateSlider(int health)
        {
            _healthBar.maxValue = _healthMax;
            _healthBar.value = health;
        }
    }
}