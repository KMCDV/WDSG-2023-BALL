using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public static event EventHandler<PlayerHealedEventArguments> PlayerHealed;
    public static void OnPlayerHealed(object sender, PlayerHealedEventArguments eventArguments) => PlayerHealed?.Invoke(sender, eventArguments);
    
    public static event EventHandler<PlayerDamagedEventArguments> PlayerDamaged;
    public static void OnPlayerDamaged(object sender, PlayerDamagedEventArguments eventArguments) => PlayerDamaged?.Invoke(sender, eventArguments);
    
    public static event EventHandler<HealthUpdatedEventArguments> HealthUpdated;
    public static void OnHealthUpdated(object sender, HealthUpdatedEventArguments eventArguments) => HealthUpdated?.Invoke(sender, eventArguments);
    
    
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 3;
    
    private void Start() => currentHealth = maxHealth;

    private void OnEnable()
    {
        PlayerHealed += Heal;
        PlayerDamaged += TakeDamage;
    }
    
    private void OnDisable()
    {
        PlayerHealed -= Heal;
        PlayerDamaged -= TakeDamage;
    }

    public bool CanBeHealed()
    {
        return currentHealth < maxHealth;
    }

    public void TakeDamage(object p_sender, PlayerDamagedEventArguments p_playerDamagedEventArguments)
    {
        currentHealth -= p_playerDamagedEventArguments.Damage;
        RefreshUI(p_playerDamagedEventArguments.PlayerID);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Heal(object p_sender, PlayerHealedEventArguments p_playerHealedEventArguments)
    {
        if(CanBeHealed() == false)
            return;
        currentHealth += p_playerHealedEventArguments.AmountToHeal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        RefreshUI(p_playerHealedEventArguments.PlayerID);
    }

    private void RefreshUI(string playerID) => HealthUpdated?.Invoke(this, new HealthUpdatedEventArguments(currentHealth, transform.position, playerID));
}

public class HealthUpdatedEventArguments : EventArgs
{
    public string PlayerID { get; }
    public int Health { get; }
    public Vector3 Position { get; }
    
    public HealthUpdatedEventArguments(int health, Vector3 p_position, string p_playerID)
    {
        PlayerID = p_playerID;
        Position = p_position;
        Health = health;
    }
}

public class PlayerDamagedEventArguments : EventArgs
{
    public string PlayerID { get; }
    public int Damage { get; }
    public Vector3 Position { get; }
    
    public PlayerDamagedEventArguments(int damage, Vector3 p_position, string p_playerID)
    {
        PlayerID = p_playerID;
        Position = p_position;
        Damage = damage;
    }
}

public class PlayerHealedEventArguments : EventArgs
{
    public string PlayerID { get; }
    public int AmountToHeal { get; }
    public Vector3 Position { get; }
    
    public PlayerHealedEventArguments(int p_amountToHeal, Vector3 p_position, string p_playerID)
    {
        PlayerID = p_playerID;
        Position = p_position;
        AmountToHeal = p_amountToHeal;
    }
}