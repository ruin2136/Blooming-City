using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartText : MonoBehaviour
{
    bool nowInCoroutine = false;
    // Update is called once per frame
    void Update()
    {
        if(!nowInCoroutine)
            StartCoroutine(Blink(gameObject));
    }

    IEnumerator Blink(GameObject gameObject)
    {
        #region 초기 변수선언
        nowInCoroutine = true;
        float currentTime = 0;
        Text _text = gameObject.GetComponent<Text>();
        UnityEngine.Color c = _text.color;
        #endregion

        while (currentTime < 0.5f)    //fadeInTime 초 만큼 반복
        {
            if (currentTime >= 0.5f)
            {
                currentTime = 0.5f;
            }
            currentTime += Time.deltaTime;

            c.a = Mathf.Lerp(0, 1, currentTime / 0.5f);  //투명도 조절 후 대입  

            gameObject.GetComponent<Text>().color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        currentTime = 0;

        while (currentTime < 0.5f)    //fadeInTime 초 만큼 반복
        {
            if (currentTime >= 0.5f)
            {
                currentTime = 0.5f;
            }
            currentTime += Time.deltaTime;

            c.a = Mathf.Lerp(1, 0, currentTime / 0.5f);  //투명도 조절 후 대입  

            gameObject.GetComponent<Text>().color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        nowInCoroutine = false;
    }
}
