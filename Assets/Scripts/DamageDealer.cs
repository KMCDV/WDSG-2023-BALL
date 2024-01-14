using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.TryGetComponent<Movement>(out Movement player))
            return;
        
        HealthSystem.OnPlayerDamaged(this, new PlayerDamagedEventArguments(damage, transform.position, player.playerID));
        PointsSystem.OnPointsLost(this, new PointLostEventArguments(1, transform.position));
    }
}
