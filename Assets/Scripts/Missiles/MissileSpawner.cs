using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private float stateSwitchDelay = 3f;

    [SerializeField]
    private enum MissileSpawnState
    {
        Rapidfire,
        Wave,
        COUNT
    }

    [Header("Rapidfire")]
    [SerializeField] private int missileSpawnAmount = 16;
    [SerializeField] private float initialSpawnDelay = 1f;
    [SerializeField] private float rapidfireDelayCountermeasure = 0.25f;

    [Header("Wave")]
    [SerializeField] private int missilesPerWave = 4;
    [SerializeField] private float waveDelay = 5f;
    [SerializeField] private float waveDelayCounter = 0f;
    [SerializeField] private int waveTotal = 5;
    [SerializeField] private int waveIndex = 0;

    MissileSpawnState currentSpawnState = MissileSpawnState.Rapidfire;

    private void Start()
    {
        NextSpawnState();
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
        Debug.Log(currentSpawnState.ToString());
        // Initial spawn delay is required for the object pool to create instances before the spawner activates, prevents null reference exception.
        yield return new WaitForSeconds(initialSpawnDelay);
        // Scaling delay that delays missiles in-between spawns.
        float rapidfireDelay = rapidfireDelayCountermeasure / missileSpawnAmount;
        for (int missileSpawnIndex = 0; missileSpawnIndex < missileSpawnAmount; missileSpawnIndex++)
        {
            // Gets missile instance from object pool and positions it on the spawner gameobject.
            GameObject missileInstance = MissilePool.Instance.GetFromPool();
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
        Debug.Log(currentSpawnState.ToString());
        yield return new WaitForSeconds(initialSpawnDelay);
        for (int i = 0; i < waveTotal; i++)
        {
            waveIndex++;
            Debug.Log(waveDelayCounter);
            for (int j = 0; j < missilesPerWave; j++)
            {
                GameObject missileInstance = MissilePool.Instance.GetFromPool();
                missileInstance.transform.position = RandomPointInBox(transform.position, transform.localScale);
            }
            yield return new WaitForSeconds(waveDelay);
        }

        if (waveIndex == waveTotal)
        {
            yield return new WaitForSeconds(stateSwitchDelay);
            waveIndex = 0;
            NextSpawnState();
        }
    }

    public static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3
            (
        (UnityEngine.Random.value - 0.5f) * size.x,
        (UnityEngine.Random.value - 0.5f) * size.y,
        (UnityEngine.Random.value - 0.5f) * size.z
            );
    }
}
