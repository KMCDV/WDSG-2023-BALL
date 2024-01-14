using UnityEngine;

public class Healer : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Movement>(out Movement player))
            return;
        HealthSystem.OnPlayerHealed(this, new PlayerHealedEventArguments(healAmount, transform.position, player.playerID));
        Destroy(gameObject);
    }
}
