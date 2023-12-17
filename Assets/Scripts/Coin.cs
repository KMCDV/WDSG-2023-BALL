using System;
using System.Reflection;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointsToAdd = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COIN");
        if (other.transform.CompareTag("Player"))
        {
            PointsSystem.OnPointsCollected(this, new PointCollectedEventArguments(pointsToAdd, transform.position));
            Destroy(gameObject);

        }
    }
}