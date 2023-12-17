using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] private int currentPoints = 0;
    public TextMeshProUGUI currentPointsText;
    public void AddPoints(int value)
    {
        currentPoints += value;
        currentPointsText.text = $"{currentPoints}";
    }

    public void RemovePoints(int value)
    {
        currentPoints -= value;
        currentPointsText.text = $"Points: {currentPoints}";
    }
    
}
