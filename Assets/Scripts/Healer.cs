using UnityEngine;

public class Healer : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
        if (healthSystem != null && healthSystem.CanBeHealed())
        {
            healthSystem.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
