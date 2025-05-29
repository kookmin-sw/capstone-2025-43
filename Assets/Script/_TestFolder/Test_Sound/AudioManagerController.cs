using UnityEngine;
using System.Collections.Generic;
using MyProject.Utils;

public class AudioManagerController : MonoBehaviour
{
    public static AudioManagerController Instance;
    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM Setup")]
    public AudioClip defaultBgmClip;
    public List<AudioClip> bgmRotationList;
    private int currentBgmIndex = 0;

    [Header("SFX Setup")]
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
        Managers.Audio.Init(bgmSource, sfxSource, defaultBgmClip, sfxList);

        Managers.Audio.PlayBGM();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && bgmRotationList.Count > 0)
        {
            currentBgmIndex = (currentBgmIndex + 1) % bgmRotationList.Count;
            var nextBgm = bgmRotationList[currentBgmIndex];
            Managers.Audio.ChangeBGM(nextBgm);
            Debug.Log($" 변경된 BGM: {nextBgm.name}");
        }
    }
}