using System;
using System.Collections;
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
    /// Max resources values
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

    [Header("Regeneration")]
    /// <summary>
    /// Resource regeneration values
    /// </summary>
    public float HealthRegeneration = 0f;
    public float ShieldRegeneration = 1f;
    public float EnergyRegeneration = 1f;

    private bool shouldRestoreShield = true;
    private bool shouldRestoreEnergy = true;

    private Coroutine pauseShieldRestorationCoroutine = null;
    [SerializeField] private float regenerationDelay = 3f;

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
        Regenerate();
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
        if (pauseShieldRestorationCoroutine != null)
        {
            StopCoroutine(pauseShieldRestorationCoroutine);
        }

        pauseShieldRestorationCoroutine = StartCoroutine(PauseShieldRestoration());

        if (Shield > 0)
        {
            Shield -= amount;
            ShieldChange();
        }

        if (Shield <= 0)
        {
            Shield = 0;
            ShieldChange();
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

    private IEnumerator PauseShieldRestoration()
    {
        shouldRestoreShield = false;
        yield return new WaitForSeconds(regenerationDelay);
        shouldRestoreShield = true;
    }

    public void Regenerate()
    {
        if (Shield < MaxShield)
        {
            if (shouldRestoreShield)
            {
                AddShield(ShieldRegeneration * Time.deltaTime);
            }
        }

        if (Energy < MaxEnergy)
        {
            if (shouldRestoreEnergy)
            {
                AddEnergy(EnergyRegeneration * Time.deltaTime);
            }
        }      
    }

    #region Add | Substract methods
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
    #endregion

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