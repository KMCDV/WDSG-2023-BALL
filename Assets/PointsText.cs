using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Awake()
    {
        pointsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        PointsSystem.PointsUpdated += UpdatePointsText;
    }

    private void UpdatePointsText(object p_sender, PointsUpdatedEventArguments p_e)
    {
        pointsText.text = $"Points: {p_e.Points}";
    }
}
