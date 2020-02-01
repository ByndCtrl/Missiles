using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResourcesUI : MonoBehaviour
{
    // Assigned through inspector
    // Resource text references
    [Header("Resources - Texts")]
    [Tooltip("Assign texts through inspector.")]
    [SerializeField] private TextMeshProUGUI healthText = null;
    [SerializeField] private TextMeshProUGUI shieldText = null;
    [SerializeField] private TextMeshProUGUI energyText = null;

    // Assigned through inspector
    // Resource bar references
    [Header("Resources - Bars")]
    [Tooltip("Assign images through inspector.")]
    [SerializeField] private Image healthBar = null;
    [SerializeField] private Image shieldBar = null;
    [SerializeField] private Image energyBar = null;

    [Header("Smoothing")]
    [Tooltip("Lerp speed for resource bars.")]
    [SerializeField] private float smoothValue = 0;

    private PlayerResources playerResources = null;

    private void Awake()
    {
        // Gets reference to PlayerResources.cs
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void Start()
    {
        InitStatsUI();
    }

    void InitStatsUI()
    {
        healthText.text = "Health: " + playerResources.Health;
        shieldText.text = "Shield: " + playerResources.Shield;
        energyText.text = "Energy: " + playerResources.Energy;

        healthBar.fillAmount = 1;
        shieldBar.fillAmount = 1;
        energyBar.fillAmount = 1;

        // Subscribe to UI events, avoid Update() for text
        playerResources.HealthChange += OnHealthChange;
        playerResources.ShieldChange += OnShieldChange;
        playerResources.EnergyChange += OnEnergyChange;
    }

    private void Update()
    {
        UpdateResourceBars();
    }

    void OnHealthChange()
    {
        healthText.text = "Health: " + System.Math.Round(playerResources.Health) + " | " + playerResources.MaxHealth;
    }

    void OnShieldChange()
    {
        shieldText.text = "Shield: " + System.Math.Round(playerResources.Shield) + " | " + playerResources.MaxShield;
    }

    void OnEnergyChange()
    {
        energyText.text = "Energy: " + System.Math.Round(playerResources.Energy) + " | " + playerResources.MaxEnergy;
    }

    private void UpdateResourceBars()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerResources.Health / playerResources.MaxHealth, Time.deltaTime * smoothValue);
        shieldBar.fillAmount = Mathf.Lerp(shieldBar.fillAmount, playerResources.Shield / playerResources.MaxShield, Time.deltaTime * smoothValue);
        energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, playerResources.Energy / playerResources.MaxEnergy, Time.deltaTime * smoothValue);
    }

    private void OnDestroy()
    {
        /// <summary>
        /// Unsubcribe from events, prevent memory leaks
        /// </summary>
        playerResources.HealthChange -= OnHealthChange;
        playerResources.ShieldChange -= OnShieldChange;
        playerResources.EnergyChange -= OnEnergyChange;
    }
}