using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    private MissileSpawnerController missileSpawnerController = null;
    [SerializeField] private float stateSwitchDelay = 3f;
    [SerializeField] private float initialSpawnDelay = 1f;

    [SerializeField]
    private enum MissileSpawnState
    {
        Rapidfire,
        Wave,
        Stats,
        COUNT
    }

    [Header("Rapidfire")]
    private float spawnAmountRapidfire = 4;
    private float rapidfireDelay = 0.25f;

    [Header("Wave")]
    private float spawnAmountPerWave = 2;
    private float waveDelay = 3f;
    private float waveAmount = 5;
    private int waveIndex = 0;

    MissileSpawnState currentSpawnState = MissileSpawnState.Rapidfire;

    private void OnEnable()
    {
        NextSpawnState();
    }

    private void Awake()
    {
        missileSpawnerController = GameObject.FindObjectOfType<MissileSpawnerController>();    
    }

    private void NextSpawnState()
    {
        // Pick a random spawn state.
        MissileSpawnState nextSpawnState = (MissileSpawnState)Random.Range(0, (int)MissileSpawnState.COUNT);

        // Get the name of the chosen state and append "State" to the end.
        string nextSpawnStateString = nextSpawnState.ToString() + "State";

        // Get the name of the current state and append "State" to the end.
        string lastSpawnStateString = currentSpawnState.ToString() + "State";

        // Update current state.
        currentSpawnState = nextSpawnState;

        // Stop last state, start new state
        StopCoroutine(lastSpawnStateString);
        StartCoroutine(nextSpawnStateString);
    }

    IEnumerator RapidfireState()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        for (int missileSpawnIndex = 0; missileSpawnIndex < spawnAmountRapidfire; missileSpawnIndex++)
        {
            // Gets missile instance from object pool and positions it on the spawner gameobject.
            GameObject missileInstance = MissilePool.Instance.GetMissileFromPool();
            missileInstance.transform.position = RandomPointInBox(transform.position, transform.localScale);
            // Short delay in-between missile spawns.
            yield return new WaitForSeconds(rapidfireDelay);
        }

        // Switch to another state after {n} seconds.       
        yield return new WaitForSeconds(stateSwitchDelay);
        NextSpawnState();
    }

    IEnumerator WaveState()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        for (int i = 0; i < waveAmount; i++)
        {
            waveIndex++;
            for (int j = 0; j < spawnAmountPerWave; j++)
            {
                // Gets missile instance from object pool and positions it on the spawner gameobject.
                GameObject missileInstance = MissilePool.Instance.GetMissileFromPool();
                missileInstance.transform.position = RandomPointInBox(transform.position, transform.localScale);
            }

            // Delay in-between missile waves.
            yield return new WaitForSeconds(waveDelay);
        }

        if (waveIndex == waveAmount)
        {
            // Reset wave counter for next iteration.  
            waveIndex = 0;

            // Switch to another state after {n} seconds. 
            yield return new WaitForSeconds(stateSwitchDelay); 
            NextSpawnState();
        }
    }

    IEnumerator StatsState()
    {
        spawnAmountRapidfire = missileSpawnerController.RapidfireSpawnAmount();
        rapidfireDelay = missileSpawnerController.RapidfireSpawnAmount();

        spawnAmountPerWave = missileSpawnerController.SpawnAmountPerWave();
        waveDelay = missileSpawnerController.WaveDelay();
        waveAmount = missileSpawnerController.WaveAmount();

        yield return null;
        NextSpawnState();
    }

    public string SpawnerCurrentState()
    {
        return currentSpawnState.ToString();
    }

    private Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3
            (
        (UnityEngine.Random.value - 0.5f) * size.x,
        (UnityEngine.Random.value - 0.5f) * size.y,
        (UnityEngine.Random.value - 0.5f) * size.z
            );
    }
}
