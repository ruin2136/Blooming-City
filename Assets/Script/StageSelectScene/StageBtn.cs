using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public GameObject tapToStartBtn;
    public GameObject[] selectBtnArr;

    #region FadeInSetting
    [Header("FadeInSetting")]
    public float fadeInTime;
    [Range(0f, 1f)]
    public float fadeInStartAlpha;
    [Range(0f, 1f)]
    public float fadeInEndAlpha;
    #endregion

    #region FadeOutSetting
    [Header("FadeOutSetting")]
    public float fadeOutTime;
    [Range(0f, 1f)]
    public float fadeOutStartAlpha;
    [Range(0f, 1f)]
    public float fadeOutEndAlpha;
    #endregion

    #region SelectStageBtns
    public void SelcetStageBtn1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void SelcetStageBtn2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void SelcetStageBtn3()
    {
        SceneManager.LoadScene("Stage3");
    }
    #endregion

    public void TapToStartBtn()
    {
        for (int i = 0; i < selectBtnArr.Length; i++)
        {
            StartCoroutine(FadeIn(selectBtnArr[i]));
        }

        Destroy(tapToStartBtn);
    }

    public IEnumerator FadeIn(GameObject Btn)
    {
        #region 초기 변수선언
        float currentTime = 0;
        UnityEngine.Color c = Btn.GetComponent<Image>().color;
        #endregion

        while (currentTime < fadeInTime)    //fadeInTime 초 만큼 반복
        {
            if (currentTime >= fadeInTime)
            {
                currentTime = fadeInTime;
            }
            currentTime += Time.deltaTime;

            c.a = Mathf.Lerp(fadeInStartAlpha, fadeInEndAlpha, currentTime / fadeInTime);  //투명도 조절 후 대입  
            
            Btn.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
