using UnityEngine;
public static class Difficulty
{
    private static float secondsToMissileMaxDifficulty = 300f;
    private static float secondsToMissileSpawnerMaxDifficulty = 300f;

    public static float GetMissileDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMissileMaxDifficulty);
    }

    public static float GetMissileSpawnerDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMissileSpawnerMaxDifficulty);
    }
}