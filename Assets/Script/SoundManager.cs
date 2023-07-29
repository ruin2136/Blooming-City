using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton ¼±¾ð
    public static SoundManager instance = null;
    Dictionary<string, AudioSource> BGMDic;

    [Header ("BGM AudioSources")]
    public AudioSource[] selcetBGMSourceArr;
    public AudioSource[] stageBGMSourceArr;
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

    // Start is called before the first frame update
    void Start()
    {
        BGMDic = new Dictionary<string, AudioSource>()
        {
            {"SelectBGM_0", selcetBGMSourceArr[0]}, {"SelectBGM_1", selcetBGMSourceArr[1]}, {"SelectBGM_2", selcetBGMSourceArr[2]},
            {"StageBGM_0",  stageBGMSourceArr[0]}, {"StageBGM_1",  stageBGMSourceArr[1]}, {"StageBGM_2",  stageBGMSourceArr[2]}
        };
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
