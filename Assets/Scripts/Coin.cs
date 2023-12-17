using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointsToAdd = 1;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COIN");
        if (other.transform.CompareTag("Player"))
        {
            PointsSystem pointsSystem = GameObject.Find("Points System").GetComponent<PointsSystem>();
            pointsSystem.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
