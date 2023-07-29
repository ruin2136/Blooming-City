using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public Plant plant;
    public GameObject eventObj;

    //이벤트 제한시간
    public float coolTime;

    public Sprite[] eventSprites;
    float eventDelay;
    public int maxDelay, minDelay;

    //이벤트 타입 enum
    public enum EventType
    { seed = 0, fertilizer = 1, water = 2, rabbit = 3, pruning = 4, remove = 5 }
    public EventType eventType;

    //이벤트 구조체와 속성
    public struct EventFrame
    {
        public int upHP, downHP, upEXP;
    }
    EventFrame[] frames = new EventFrame[6];

    void Start()
    {
        #region 이벤트 구조체 배열 초기화(이벤트 값들)
        frames[0].upHP = 3;
        frames[0].downHP = 3;
        frames[0].upEXP = 3;

        frames[1].upHP = 3;
        frames[1].downHP = 3;
        frames[1].upEXP = 3;

        frames[2].upHP = 3;
        frames[2].downHP = 3;
        frames[2].upEXP = 3;

        frames[3].upHP = 3;
        frames[3].downHP = 3;
        frames[3].upEXP = 3;

        frames[4].upHP = 3;
        frames[4].downHP = 3;
        frames[4].upEXP = 3;

        frames[5].upHP = 3;
        frames[5].downHP = 3;
        frames[5].upEXP = 3;
        #endregion

        //테스트
        StartCoroutine(CoolTime(coolTime));
        StartCoroutine(DelayTime());
    }

    public IEnumerator CoolTime(float cool)
    {
        print("쿨타임 코루틴 실행");

        //이벤트 쿨타임 시각화
        eventObj.GetComponent<Image>().fillAmount = 0;

        while (cool > 0f)
        {
            cool -= Time.deltaTime;
            eventObj.GetComponent<Image>().fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        //Fail() 호출
        Fail();

        print("쿨타임 코루틴 완료");
    }

    public IEnumerator DelayTime()
    {
        eventDelay = UnityEngine.Random.Range(minDelay, maxDelay);

        print("이벤트 딜레이 코루틴 실행" + eventDelay);

        while (eventDelay > 0)
        {
            eventDelay -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //코루틴 완료 시 EventAppear()호출
        EventAppear();

        print("이벤트 딜레이 코루틴 완료" + eventDelay);
    }

    void Success()
    {
        //이벤트 제한시간 코루틴 중단
        StopCoroutine(CoolTime(coolTime));

        //GrowUp()함수 호출
        plant.GrowUp(frames[(int)eventType]);

        //이벤트 오브젝트 끄기
        eventObj.SetActive(false);
    }

    void Fail()
    {
        //Wither()함수 호출
        plant.Wither(frames[(int)eventType]);

        //이벤트 오브젝트 끄기
        eventObj.SetActive(false);
    }

    public void EventAppear()
    {
        //이벤트 오브젝트 활성화
        eventObj.SetActive(true);

        //이벤트 타입 변경
        //식물 단계에 따라 호출될 이벤트 랜덤하게 설정
        switch (plant.growGrade)
        {
            case 0:
                //1단계(토양)이면 씨앗 이벤트 호출
                eventType = EventType.seed;
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                break;

            case 1:
            case 2:
            case 3:
                //2~4단계면 1~3중 랜덤 호출
                //코루틴 호출
                eventType = (EventType)UnityEngine.Random.Range(1, 3);
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                StartCoroutine(CoolTime(coolTime));
                break;

            case 4:
                //4단계(올리브)면 1~4중 랜덤
                //코루틴 호출
                eventType = (EventType)UnityEngine.Random.Range(1, 4);
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                StartCoroutine(CoolTime(coolTime));
                break;

            case 5:
                eventType = EventType.remove;
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                //5단계(시듦)이면 제거 이벤트 호출
                break;

            default:
                Debug.Log("Event Error!");
                break;
        }
    }
}
