using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.Click,
            Resources.Load<AudioClip>("click"));
        audioClips.Add(AudioClipName.BallCollide,
            Resources.Load<AudioClip>("ball_collide"));
        audioClips.Add(AudioClipName.PaddleCollide,
           Resources.Load<AudioClip>("paddle_collide"));
        audioClips.Add(AudioClipName.BallSpawn,
           Resources.Load<AudioClip>("ball_spawn"));
        audioClips.Add(AudioClipName.BallLost,
           Resources.Load<AudioClip>("ball_lost"));
        audioClips.Add(AudioClipName.GameOver,
           Resources.Load<AudioClip>("gameover"));
        audioClips.Add(AudioClipName.Speedup,
           Resources.Load<AudioClip>("speedup"));
        audioClips.Add(AudioClipName.Freeze,
           Resources.Load<AudioClip>("freeze"));
    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
