using UnityEngine;
public static class Difficulty
{
    private static float secondsToMaxDifficulty = 300f;

    public static float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}