using UnityEngine;
using System.Collections.Generic;
using MyProject.Utils;

public class AudioManagerController : MonoBehaviour
{
    public static AudioManagerController Instance;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip bgmClip;
    public List<AudioManager.SFXData> sfxList;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
        Managers.Audio.Init(bgmSource, sfxSource, bgmClip, sfxList);

        Managers.Audio.PlayBGM();
    }
}