using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] plants;
    public bool stageClear=false;
    public int stageNumber;

    public GameObject clearPopUp;

    // Update is called once per frame
    void Update()
    {
        if(!stageClear)
        {
            int count = 0;
            foreach (var plant in plants)
            {
                if(plant.GetComponent<Plant>().isComplete)
                    count++;
            }

            if(count == plants.Length)
                stageClear=true;

            //for(int i = 0; i < plants.Length; i++)
            //{
            //    if (plants[i].GetComponent<Plant>().isComplete)
            //        stageClear = true;
            //    else
            //        stageClear = false;
            //}
        }

        Debug.Log(stageClear);

        CheckClear();

    }

    void CheckClear()
    {
        if (stageClear)
        {
            GameManager.instance.isClearStage[stageNumber] = true;
            //스테이지 클리어!

            clearPopUp.SetActive(true);
        }
    }

    public void OnClickBtn()
    {
        SceneManager.LoadScene("MainScene");
    }
}
