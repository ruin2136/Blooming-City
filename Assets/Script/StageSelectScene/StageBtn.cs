using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{

    private void Update()
    {
         
    }
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
}
