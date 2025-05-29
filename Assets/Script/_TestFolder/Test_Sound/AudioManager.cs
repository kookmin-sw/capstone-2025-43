using UnityEngine;
using System.Collections.Generic;
using MyProject.Utils;

public class AudioManager
{
    AudioSource bgmSource;
    AudioSource sfxSource;

    AudioClip bgmClip;

    public enum AudioChannel { BGM, SFX }

    [System.Serializable]
    public struct SFXData
    {
        public SFXType type;
        public List<AudioClip> clip;
    }

    Dictionary<SFXType, List<AudioClip>> sfxDict = new();

    public void Init(AudioSource bgmSource, AudioSource sfxSource, AudioClip bgmClip, List<SFXData> sfxList)
    {
        this.bgmSource = bgmSource;
        this.sfxSource = sfxSource;
        this.bgmClip = bgmClip;

        sfxDict.Clear();
        foreach (var sfx in sfxList)
        {
            sfxDict[sfx.type] = sfx.clip;
        }
    }

    public void PlayBGM()
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    public void PlayEffect(SFXType type)
    {
        if (sfxDict.TryGetValue(type, out var clipList) && clipList.Count > 0)
        {
            var index = Random.Range(0, clipList.Count);
            sfxSource.PlayOneShot(clipList[index]);
        }
        else
        {
            Debug.LogWarning($"Effect sound not found: {type}");
        }
    }
}
