using System;
using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    public static event EventHandler<PointCollectedEventArguments> PointCollected;
    public static void OnPointsCollected(object sender, PointCollectedEventArguments eventArguments) => PointCollected?.Invoke(sender, eventArguments);

    public static event EventHandler<PointLostEventArguments> PointLost;
    public static void OnPointsLost(object sender, PointLostEventArguments eventArguments) => PointLost?.Invoke(sender, eventArguments);
    

    [SerializeField] private int currentPoints = 0;
    public TextMeshProUGUI currentPointsText;
    
    private void OnEnable()
    {
        PointCollected += AddPoints;
        PointLost += RemovePoints;
    }
    
    private void OnDisable()
    {
        PointCollected -= AddPoints;
        PointLost -= RemovePoints;
    }
    
    
    public void AddPoints(object sender, PointCollectedEventArguments arguments)
    {
        currentPoints += arguments.Points;
        currentPointsText.text = $"{currentPoints}";
    }

    public void RemovePoints(object sender, PointLostEventArguments p_eventArgs)
    {
        currentPoints -= p_eventArgs.Points;
        currentPointsText.text = $"Points: {currentPoints}";
    }
}

public class PointCollectedEventArguments : EventArgs
{
    public int Points { get; }
    public Vector3 Position { get; }
    
    public PointCollectedEventArguments(int points, Vector3 p_position)
    {
        Position = p_position;
        Points = points;
    }
}

public class PointLostEventArguments : EventArgs
{
    public int Points { get; }
    public Vector3 Position { get; }
    
    public PointLostEventArguments(int points, Vector3 p_position)
    {
        Position = p_position;
        Points = points;
    }
}