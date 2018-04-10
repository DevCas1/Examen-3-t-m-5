using DG.Tweening;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource FXSource;
    public AudioSource MusicSource1;
    public AudioSource AmbientSource;
    public float FadeTime;
    public float MaxVolume = 1;

    public void FadeIn(int specificSource = 1) => MusicSource1.DOFade(MaxVolume, FadeTime);

    public void PlayMusic(AudioClip clip)
    {
        if (MusicSource1 == null || clip == null) return;

        MusicSource1.clip = clip;
        MusicSource1.DOFade(MaxVolume, FadeTime).OnStart(MusicSource1.Play);
    }

    public void PlayAudioFX(AudioClip clip)
    {
        if (FXSource.isPlaying) return;
        FXSource.PlayOneShot(clip);
    }

    public void StopAudioFX()
    {
        //FXSource.Stop();
    }

    public void PlayAmbient(AudioClip clip)
    {
        if (AmbientSource.isPlaying) return;
        AmbientSource.PlayOneShot(clip);
    }

    public void FadeAll(float fadeTime)
    {
        MusicSource1.DOFade(0, fadeTime <= float.Epsilon ? FadeTime : fadeTime);
    }
}