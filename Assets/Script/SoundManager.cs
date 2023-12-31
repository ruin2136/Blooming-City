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
    [Header("Extra SFX")]
    public AudioClip[] EatingSoundByRabbit;

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

        SceneManager.sceneLoaded += OnSceneLoaded;

        InitBGMClip();
        if (!GameManager.instance.isCutSceneShown)
            Invoke("PlayBGM", 22f);
        else
            PlayBGM();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitBGMClip();
        if (!GameManager.instance.isCutSceneShown)
            Invoke("PlayBGM", 22f);
        else
            PlayBGM();
    }

    void InitBGMClip()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainScene":
                if (GameManager.instance.GetNumOfClearStage() == 0)
                {
                    audioSource.clip = BGMDic["SelectBGM_0"];
                }
                if (GameManager.instance.GetNumOfClearStage() == 1)
                    audioSource.clip = BGMDic["SelectBGM_1"];
                if (GameManager.instance.GetNumOfClearStage() == 2)
                    audioSource.clip = BGMDic["SelectBGM_2"];
                if (GameManager.instance.GetNumOfClearStage() == 3)
                    audioSource.clip = BGMDic["SelectBGM_3"];
                break;
            case "Stage1":
                audioSource.clip = BGMDic["StageBGM_0"];
                break;
            case "Stage2":
                audioSource.clip = BGMDic["StageBGM_1"];
                break;
            case "Stage3":
                audioSource.clip = BGMDic["StageBGM_2"];
                break;
            default:
                break;
        }
    }

    void PlayBGM()
    {
        audioSource.Play();
    }

    public AudioClip GetSFX(string clipName)
    {
        int i = UnityEngine.Random.Range(0, 3);
        AudioClip returnClip;

        switch (clipName)   //AudioClip 초기화
        {
            #region GrowUpSound
            case "Planting":
                returnClip = plantingSFXArr[i];
                break;
            case "Fertilizer":
                returnClip = fertilizerSFXArr[i];
                break;
            case "Watering":
                returnClip = WarteringSFXArr[i];
                break;
            case "AwayRabbit":
                returnClip = awayRabbitSFXArr[i];
                break;
            case "Pruning":
                returnClip = pruningSFXArr[i];
                break;
            case "Shovel":
                returnClip = shovelSFXArr[i];
                break;
            #endregion
            #region ExtraSound
            case "EatingSoundByRabbit":
                returnClip = EatingSoundByRabbit[i];
                break;
            #endregion
            default:
                returnClip = EatingSoundByRabbit[i];
                Debug.LogWarning("도대체 무슨 사운드를 호출하신건가요? 멍청아!");
                break;
        }

        return returnClip;
    }
}
