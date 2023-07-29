using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton ¼±¾ð
    public static GameManager instance = null;

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

    public bool[] isClearStage;
    public bool isCutSceneShown = true;

    public int GetNumOfClearStage()
    {
        int count = 0;
        for (int i = 0; i < isClearStage.Length; i++)
        {
            if(isClearStage[i])
                count++;
        }

        return count;
    }
}
