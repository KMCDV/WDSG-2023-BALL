using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private string _playerToShow;
    
    private void Awake()
    {
        _healthText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        HealthSystem.HealthUpdated += UpdateHealthText;
    }
    
    private void OnDisable()
    {
        HealthSystem.HealthUpdated -= UpdateHealthText;
    }
    
    private void UpdateHealthText(object p_sender, HealthUpdatedEventArguments p_e)
    {
        if(p_e.PlayerID != _playerToShow)
            return;
        _healthText.text = $"Health: {p_e.Health}";
    }
}
