using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementPreset", menuName = "Ball/Player Movement Preset")]
public class PlayerMovementPreset : ScriptableObject
{
    public float Acceleration = 10f;
    public float MaxSpeed = 5f;
    public float JumpForce = 45000f;
    public float DashForce = 135000f;
    public int DashAmount = 2;
    public float DashCooldown = 1f;
    public float Mass = 25f;
    public int MaxJumps = 2;
    
    public void ApplyMassToRigidbody(GameObject gameObject)
    {
        if(gameObject == null)
            return;
        if(gameObject.TryGetComponent(out Rigidbody rigidbody) == false)
            return;
        
        rigidbody.mass = Mass;
    }
}
