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

    public float cool;

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
        public int upHP, downHP, upEXP, timeLimit;
    }
    EventFrame[] frames = new EventFrame[6];

    void Start()
    {
        #region 구조체 배열 초기화(이벤트 값들)
        frames[0].upHP = 3;
        frames[0].downHP = 3;
        frames[0].upEXP = 3;
        frames[0].timeLimit = 3;

        frames[1].upHP = 3;
        frames[1].downHP = 3;
        frames[1].upEXP = 3;
        frames[1].timeLimit = 3;

        frames[2].upHP = 3;
        frames[2].downHP = 3;
        frames[2].upEXP = 3;
        frames[2].timeLimit = 3;

        frames[3].upHP = 3;
        frames[3].downHP = 3;
        frames[3].upEXP = 3;
        frames[3].timeLimit = 3;

        frames[4].upHP = 3;
        frames[4].downHP = 3;
        frames[4].upEXP = 3;
        frames[4].timeLimit = 3;

        frames[5].upHP = 3;
        frames[5].downHP = 3;
        frames[5].upEXP = 3;
        frames[5].timeLimit = 3;

        frames[6].upHP = 3;
        frames[6].downHP = 3;
        frames[6].upEXP = 3;
        frames[6].timeLimit = 3;
        #endregion

        //테스트
        StartCoroutine(CoolTime(cool));
        StartCoroutine(DelayTime());
    }

    IEnumerator CoolTime(float cool)
    {
        print("쿨타임 코루틴 실행");

        //이벤트 쿨타임 시각화
        gameObject.GetComponent<Image>().fillAmount = 0;

        while (cool > 0f)
        {
            cool -= Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        //Fail() 호출
        Fail();

        print("쿨타임 코루틴 완료");
    }

    IEnumerator DelayTime()
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

    void Scuccess()
    {
        //이벤트 제한시간 코루틴 중단
        StopCoroutine(CoolTime(cool));

        //GrowUp()함수 호출
        plant.GrowUp(frames[(int)eventType]);

        //이벤트 끄기, 이벤트 딜레이 코루틴 호출
        StartCoroutine(DelayTime());
    }

    void Fail()
    {
        //Wither()함수 호출
        plant.Wither(frames[(int)eventType]);

        //이벤트 끄기, 이벤트 딜레이 코루틴 호출
        StartCoroutine(DelayTime());
    }

    void EventAppear()
    {
        //이벤트 제한시간 코루틴 호출
        StartCoroutine(CoolTime(cool));

        //랜덤한 이벤트 호출(제한된)
        //식물 단계, 종류에 따라 호출될 이벤트 랜덤하게 설정
        eventType = EventType.rabbit;
    }
}
