using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public AudioSource sfxSource;
    public AudioSource sfxLoopSource;
    public AudioSource musicSource;

    private float lowPitchRange = 0.90f;
    private float highPitchRange = 1.10f;

    public void PlaySingle(AudioClip audioClip)
    {
        sfxSource.clip = audioClip;
        sfxSource.Play();
    }

    public void PlayRandom(params AudioClip[] audioClips)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxSource.clip = audioClips[randomIndex];
        sfxSource.pitch = randomPitch;
        sfxSource.Play();
    }

    public void PlayRandomLoop(params AudioClip[] audioClips)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxLoopSource.pitch = randomPitch;
        sfxLoopSource.clip = audioClips[randomIndex];
        sfxLoopSource.loop = true;
        sfxLoopSource.Play();
    }
}
