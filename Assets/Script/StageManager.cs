using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] plants;
    public bool stageClear=false;

    // Update is called once per frame
    void Update()
    {
        if(!stageClear)
        {
            for(int i = 0; i < plants.Length; i++)
            {
                if (plants[i].GetComponent<Plant>().isComplete)
                    stageClear = true;
                else
                    stageClear = false;
            }
        }

        if(stageClear)
        {
            //스테이지 클리어!
        }
    }
}
