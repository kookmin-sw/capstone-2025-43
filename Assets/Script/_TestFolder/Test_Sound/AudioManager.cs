using UnityEngine;
using System.Collections.Generic;
using MyProject.Utils;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip bgmClip;     // BGM

    [System.Serializable]
    public struct SFXData
    {
        public SFXType type;
        public List<AudioClip> clip;
    }

    public List<SFXData> sfxList;
    private Dictionary<SFXType, List<AudioClip>> sfxDict;

        void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        sfxDict = new Dictionary<SFXType, List<AudioClip>>();
        foreach (var sfx in sfxList)
        {
            sfxDict[sfx.type] = sfx.clip;
        }
    }


    // Battle Scene Start
    public void PlayBGM()
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    // Battle Scene End
    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    // Effect
    public void PlayEffect(SFXType type)
    {
        if (sfxDict.TryGetValue(type, out var clipList) && clipList.Count > 0)
        {
            var index = Random.Range(0, clipList.Count);
            sfxSource.PlayOneShot(clipList[index]);
        }
        else
        {
            Debug.LogWarning("Effect sound not found: {type}");
        }
    }

    // Test Start BGM
        void Start()
    {
        AudioManager.Instance.PlayBGM();
    }
}

// Effect Example
// AudioManager.Instance.PlayEffect(SFXType.Attack);