using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public Plant plant;
    public GameObject eventObj, eventIcon, eventGauge;
    public bool isEvent;

    //오디오 소스
    public AudioSource eventAudio;

    //이벤트 제한시간
    public float coolTime;

    public Sprite[] eventSprites;
    
    //이벤트 딜레이
    public float eventDelay;
    
    public int maxDelay, minDelay;

    //이벤트 타입 enum
    public enum EventType
    { Sowing = 0, Fertilizing = 1, Watering = 2, AwayingRabbit = 3, Pruning = 4, Removing = 5 }
    public EventType eventType;

    //이벤트 구조체와 속성
    public struct EventFrame
    {
        public int upHP, downHP, upEXP;
    }
    public EventFrame[] frames = new EventFrame[6];

    IEnumerator coroutine;

    void Start()
    {
        eventAudio = GetComponent<AudioSource>();

        //코루틴 할당
        coroutine = TimeLimit(coolTime);

        #region 이벤트 구조체 배열 초기화(이벤트 값들)
        frames[0].upHP = 1;
        frames[0].downHP = 1;
        frames[0].upEXP = 1;

        frames[1].upHP = 1;
        frames[1].downHP = 1;
        frames[1].upEXP = 1;

        frames[2].upHP = 1;
        frames[2].downHP = 1;
        frames[2].upEXP = 1;

        frames[3].upHP = 1;
        frames[3].downHP = 2;
        frames[3].upEXP = 0;

        frames[4].upHP = 1;
        frames[4].downHP = 1;
        frames[4].upEXP = 0;

        frames[5].upHP = 0;
        frames[5].downHP = 0;
        frames[5].upEXP = 0;
        #endregion

        plant.OliveDecide();
        plant.Wither(frames[(int)eventType]);
    }

    public IEnumerator TimeLimit(float cool)
    {
        float tmp = 0f;

        isEvent = true;

        print("시간제한 코루틴 실행");
        Debug.Log(cool);

        eventGauge.SetActive(true);
        //이벤트 쿨타임 시각화
        eventGauge.GetComponent<Image>().fillAmount = 0;

        while (tmp <= cool)
        {
            tmp += Time.deltaTime;
            eventGauge.GetComponent<Image>().fillAmount = (tmp / cool);
            yield return new WaitForFixedUpdate();
        }

        isEvent = false;
        //Fail() 호출
        Fail();
        eventGauge.SetActive(false);

        print("시간제한 코루틴 완료");

    }

    public IEnumerator DelayTime()
    {
        eventDelay = UnityEngine.Random.Range(minDelay, maxDelay);

        while (eventDelay > 0)
        {
            eventDelay -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //코루틴 완료 시 EventAppear()호출
        EventAppear();

        print("이벤트 딜레이 코루틴 완료" + eventDelay);
    }

    public void Success()
    {
        //만약 제거해야 되면 이렇게
        //아니면 그대로
        if((int)eventType==5)
        {
            plant.ResetPlant();

            eventAudio.clip= SoundManager.instance.GetSFX("Shovel");
            eventAudio.Play();
        }
        else
        {
            //이벤트 제한시간 코루틴 중단
            StopCoroutine(coroutine);            
            coroutine = TimeLimit(coolTime);

            switch((int)eventType)
            {
                case 0:
                    eventAudio.clip = SoundManager.instance.GetSFX("Planting");
                    eventAudio.Play();
                    break;
                case 1:
                    eventAudio.clip = SoundManager.instance.GetSFX("Fertilizer");
                    eventAudio.Play();
                    break;
                case 2:
                    eventAudio.clip = SoundManager.instance.GetSFX("Watering");
                    eventAudio.Play();
                    break;
                case 3:
                    eventAudio.clip = SoundManager.instance.GetSFX("AwayRabbit");
                    eventAudio.Play();
                    break;
                case 4:
                    eventAudio.clip = SoundManager.instance.GetSFX("Pruning");
                    eventAudio.Play();
                    break;
                default:
                    Debug.LogWarning("잘못된 사운드 할당입니다.");
                    break;
            }

            //GrowUp()함수 호출
            plant.GrowUp(frames[(int)eventType]);
        }
    }

    public void Fail()
    {
        //Wither()함수 호출
        plant.Wither(frames[(int)eventType]);
        coroutine = TimeLimit(coolTime);
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
                eventType = EventType.Sowing;
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];

                isEvent=true;
                break;

            case 1:
            case 2:
            case 3:
                //2~4단계면 1~3중 랜덤 호출
                //코루틴 호출
                eventType = (EventType)UnityEngine.Random.Range(1, 4);
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];

                coroutine = TimeLimit(coolTime);
                StartCoroutine(coroutine);
                break;

            case 4:
                //4단계(올리브)면 1~4중 랜덤
                //코루틴 호출
                eventType = (EventType)UnityEngine.Random.Range(1, 5);
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];

                coroutine = TimeLimit(coolTime);
                StartCoroutine(coroutine);
                break;

            case 5:
                eventType = EventType.Removing;
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                //5단계(시듦)이면 제거 이벤트 호출

                isEvent = true;
                break;

            default:
                Debug.Log("Event Error!");
                break;
        }
    }
}
