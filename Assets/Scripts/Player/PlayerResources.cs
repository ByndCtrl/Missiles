using System;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    /// <summary>
    /// Current resources values
    /// </summary> 
    public float Health { get; private set; } = 100f;
    public float Shield { get; private set; } = 200f;
    public float Energy { get; private set; } = 50f;

    /// <summary>
    /// Max Resources values
    /// </summary>
    public float MaxHealth { get; private set; } = 100f;
    public float MaxShield { get; private set; } = 200f;
    public float MaxEnergy { get; private set; } = 50f;

    /// <summary>
    /// Events to update PlayerResourcesUI.cs
    /// Avoiding UI changes in Update()
    /// </summary>
    public event Action HealthChange;
    public event Action ShieldChange;
    public event Action EnergyChange;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InitResources();
    }

    private void Update()
    {
        DebugStatsUI();
    }

    public void InitResources()
    {
        Health = MaxHealth;
        Shield = MaxShield;
        Energy = MaxEnergy;

        Debug.Log("[PlayerResources] Health: " + Health);
        Debug.Log("[PlayerResources] Shield: " + Shield);
        Debug.Log("[PlayerResources] Energy: " + Energy);
    }

    public void TakeDamage(float amount)
    {
        if (Shield > 0)
        {
            Shield -= amount;
            ShieldChange();
        }

        if (Shield <= 0)
        {
            Shield = 0;
            Health -= amount;
            HealthChange();
        }

        if (Health <= 0)
        {
            Health = 0;
            HealthChange();
            Debug.Log("Player dead.");
        }
    }

    public void AddHealth(float healthAmount)
    {
        Health += healthAmount;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        HealthChange();
    }

    public void AddShield(float shieldAmount)
    {
        Shield += shieldAmount;

        if (Shield > MaxShield)
        {
            Shield = MaxShield;
        }

        ShieldChange();
    }

    public void AddEnergy(float energyAmount)
    {
        Energy += energyAmount;

        if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
        }

        EnergyChange();
    }

    public void SubtractHealth(float healthAmount)
    {
        Health -= healthAmount;

        if (Health < 0)
        {
            Health = 0;
        }

        HealthChange();
    }

    public void SubtractShield(float shieldAmount)
    {
        Shield -= shieldAmount;

        if (Shield < 0)
        {
            Shield = 0;
        }

        ShieldChange();
    }

    public void SubtractEnergy(float energyAmount)
    {
        Energy -= energyAmount;

        if (Energy < 0)
        {
            Energy = 0;
        }

        EnergyChange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            
        }
    }

    /* USED FOR DEBUGGING ONLY */
    private void DebugStatsUI()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AddHealth(25);
            AddShield(25);
            AddEnergy(5);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            TakeDamage(5);
        }
    }
}