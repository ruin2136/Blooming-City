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
    public GameObject[] FadeOutObjectArr;
    public GameObject tapToStartText; 

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

    private void Start()
    {
        tapToStartBtn.GetComponent<Button>().interactable = false;

        if (GameManager.instance.isCutSceneShown)
        {

        }
        else //컷씬 및 이미지 페이드 연출
        {
            //영상출력부분



            StartCoroutine(CutSceneFadeIn());   //배경 이미지 페이드인(컷씬전용 하드코딩함수)

            tapToStartBtn.GetComponent<Button>().interactable = true;
            tapToStartText.SetActive(true);

            GameManager.instance.isCutSceneShown = false;
        }
    }

    public void TapToStartBtn()
    {
        for (int i = 0; i < selectBtnArr.Length; i++)
        {
            StartCoroutine(FadeIn(selectBtnArr[i]));
            StartCoroutine(FadeOut(FadeOutObjectArr[i]));
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

    public IEnumerator FadeOut(GameObject FadeOutObject)
    {
        #region 초기 변수선언
        float currentTime = 0;
        UnityEngine.Color c = FadeOutObject.GetComponent<Image>().color;
        #endregion

        while (currentTime < fadeOutTime)    //fadeInTime 초 만큼 반복
        {
            if (currentTime >= fadeOutTime)
            {
                currentTime = fadeOutTime;
            }
            currentTime += Time.deltaTime;

            c.a = Mathf.Lerp(fadeOutStartAlpha, fadeOutEndAlpha, currentTime / fadeOutTime);  //투명도 조절 후 대입  

            FadeOutObject.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator CutSceneFadeIn()
    {
        yield return new WaitForSeconds(22f);
        for (int i = 0; i < FadeOutObjectArr.Length; i++)
        {
            StartCoroutine(FadeIn(FadeOutObjectArr[i]));
        }
    }
}
