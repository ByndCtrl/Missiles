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
    [SerializeField] private Vector2 missileSpawnRapidfireMinMax = new Vector2(1, 16);
    [SerializeField] private float missileSpawnAmountRapidfire = 4;
    [SerializeField] private float initialSpawnDelay = 1f;
    [SerializeField] private float rapidfireDelayCountermeasure = 0.25f;

    [Header("Wave")]
    [SerializeField] private Vector2 missileSpawnWaveMinMax = new Vector2(1, 4);
    [SerializeField] private float missileSpawnAmountWave = 2;
    [SerializeField] private float waveDelay = 3f;
    [SerializeField] private int waveTotal = 5;
    [SerializeField] private int waveIndex = 0;

    MissileSpawnState currentSpawnState = MissileSpawnState.Rapidfire;

    private void OnEnable()
    {
        NextSpawnState();
    }
    private void FixedUpdate()
    {
        Debug.Log("Current number of missiles per rapidfire: " + MissileSpawnAmountRapidfire() + ".");
        Debug.Log("Current number of missiles per wave: " + MissileSpawnAmountWave() + ".");
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
        yield return new WaitForSeconds(initialSpawnDelay);

        // Delays missiles in-between spawns.
        float rapidfireDelay = rapidfireDelayCountermeasure / missileSpawnAmountRapidfire;

        for (int missileSpawnIndex = 0; missileSpawnIndex < missileSpawnAmountRapidfire; missileSpawnIndex++)
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
            for (int j = 0; j < missileSpawnAmountWave; j++)
            {
                // Gets missile instance from object pool and positions it on the spawner gameobject.
                GameObject missileInstance = MissilePool.Instance.GetFromPool();
                missileInstance.transform.position = RandomPointInBox(transform.position, transform.localScale);
            }

            // Delay in-between missile waves.
            yield return new WaitForSeconds(waveDelay);
        }

        if (waveIndex == waveTotal)
        {
            // Reset wave counter for next iteration.  
            waveIndex = 0;

            // Switch to another state after {n} seconds. 
            yield return new WaitForSeconds(stateSwitchDelay); 
            NextSpawnState();
        }
    }
    private float MissileSpawnAmountRapidfire()
    {
        missileSpawnAmountRapidfire = Mathf.RoundToInt(Mathf.Lerp(missileSpawnRapidfireMinMax.x, missileSpawnRapidfireMinMax.y, Difficulty.GetDifficultyPercent()));
        return missileSpawnAmountRapidfire;
    }
    private float MissileSpawnAmountWave()
    {
        missileSpawnAmountWave = Mathf.RoundToInt(Mathf.Lerp(missileSpawnWaveMinMax.x, missileSpawnWaveMinMax.y, Difficulty.GetDifficultyPercent()));
        return missileSpawnAmountWave;
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
