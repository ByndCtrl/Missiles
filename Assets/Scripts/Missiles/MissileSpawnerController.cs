using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MissileSpawnerController : MonoBehaviour
{
    [Header("General")]
    /* Add options for controlling the number of active spawners and their positions around the player */

    [Header("Rapidfire")]
    [SerializeField] private Vector2 spawnAmountRapidfireMinMax = new Vector2(1f, 16f);
    private float spawnAmountRapidfire = 1f;

    [SerializeField] private Vector2 rapidfireDelayMinMax = new Vector2(1f, 0.16f);
    private float rapidfireDelay = 1f;

    [Header("Wave")]
    [SerializeField] private Vector2 spawnAmountPerWaveMinMax = new Vector2(1f, 4f);
    private float spawnAmountPerWave = 1f;

    [SerializeField] private Vector2 waveAmountMinMax = new Vector2(1f, 4f);
    private float waveAmount = 1f;

    [SerializeField] private Vector2 waveDelayMinMax = new Vector2(4f, 1f);
    private float waveDelay = 4f;

    private void Awake()
    {

    }
    private void FixedUpdate()
    {
        Debug.Log("Current number of missiles per rapidfire: " + spawnAmountRapidfire + ".");
        Debug.Log("Current number of missiles per wave: " + spawnAmountPerWave + ".");
    }
    public float RapidfireSpawnAmount()
    {
        spawnAmountRapidfire = Mathf.RoundToInt(Mathf.Lerp(spawnAmountRapidfireMinMax.x, spawnAmountRapidfireMinMax.y, Difficulty.GetMissileSpawnerDifficultyPercent()));
        return spawnAmountRapidfire;
    }

    public float RapidfireDelay()
    {
        rapidfireDelay = Mathf.Lerp(rapidfireDelayMinMax.x, rapidfireDelayMinMax.y, Difficulty.GetMissileSpawnerDifficultyPercent());
        return rapidfireDelay;
    }

    public float SpawnAmountPerWave()
    {
        spawnAmountPerWave = Mathf.RoundToInt(Mathf.Lerp(spawnAmountPerWaveMinMax.x, spawnAmountPerWaveMinMax.y, Difficulty.GetMissileSpawnerDifficultyPercent()));
        return spawnAmountPerWave;
    }

    public float WaveAmount()
    {
        waveAmount = Mathf.RoundToInt(Mathf.Lerp(waveAmountMinMax.x, waveAmountMinMax.y, Difficulty.GetMissileSpawnerDifficultyPercent()));
        return waveAmount;
    }

    public float WaveDelay()
    {
        rapidfireDelay = Mathf.Lerp(waveDelayMinMax.x, waveDelayMinMax.y, Difficulty.GetMissileSpawnerDifficultyPercent());
        return waveDelay;
    }
}
