using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
    }
}
