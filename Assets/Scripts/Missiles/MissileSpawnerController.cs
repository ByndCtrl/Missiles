using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MissileSpawnerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private int amountOfSpawners = 3;
    [SerializeField] private MissileSpawner missileSpawnerPrefab = null;
    [SerializeField] private float radius = 100f, xScale = 10f, yScale = 10f, zScale = 0f;   

    private List<MissileSpawner> missileSpawners = null;

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

    private Transform player = null;

    private void Awake()
    {
        missileSpawners = new List<MissileSpawner>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        InitMissileSpawners();
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

    private void InitMissileSpawners()
    {
        for (int i = 0; i < amountOfSpawners; i++)
        {
            CreateSpawner(i);
        }
    }

    private void CreateSpawner(int index)
    {
        Transform spawnerPosition = new GameObject("Missile Spawner - Position").transform;
        spawnerPosition.SetParent(player, false);
        spawnerPosition.localRotation = Quaternion.Euler(0f, 0f, index * 360f / amountOfSpawners);

        MissileSpawner spawner = Instantiate<MissileSpawner>(missileSpawnerPrefab);
        spawner.name = "Missile Spawner";
        spawner.transform.SetParent(spawnerPosition, false);
        spawner.transform.localPosition = new Vector3(0f, radius, 0f);
        spawner.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        spawner.transform.localScale = new Vector3(xScale, yScale, zScale);
        //spawner.gameObject.SetActive(false);
        missileSpawners.Add(spawner);
    }
}
