using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    #region Singleton 선언
    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    #endregion

    Dictionary<string, AudioClip> BGMDic;
    AudioSource audioSource;

    [Header("BGM AudioSources")]
    public AudioClip[] selcetBGMClipArr;
    public AudioClip[] stageBGMClipArr;

    [Header("GrowUp SFX AudioSources")]
    public AudioClip[] plantingSFXArr;
    public AudioClip[] fertilizerSFXArr;
    public AudioClip[] WarteringSFXArr;
    public AudioClip[] awayRabbitSFXArr;
    public AudioClip[] pruningSFXArr;
    public AudioClip[] shovelSFXArr;

    void Start()
    {
        #region 사운드 출력 준비
        BGMDic = new Dictionary<string, AudioClip>()
        {
            {"SelectBGM_0", selcetBGMClipArr[0]}, {"SelectBGM_1", selcetBGMClipArr[1]}, {"SelectBGM_2", selcetBGMClipArr[2]},
            {"SelectBGM_3", selcetBGMClipArr[3]},

            {"StageBGM_0",  stageBGMClipArr[0]}, {"StageBGM_1",  stageBGMClipArr[1]}, {"StageBGM_2",  stageBGMClipArr[2]}
        };
        audioSource = GetComponent<AudioSource>();
        #endregion

        PlayBGM();
    }

    void InitBGMClip()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainScene":
            default:
                break;
        }
    }

    void PlayBGM()
    {
        audioSource.clip = BGMDic["SelectBGM_3"];
        audioSource.Play();
    }
}
