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

    //����� �ҽ�
    public AudioSource eventAudio;

    //�̺�Ʈ ���ѽð�
    public float coolTime;

    public Sprite[] eventSprites;
    
    //�̺�Ʈ ������
    public float eventDelay;
    
    public int maxDelay, minDelay;

    //�̺�Ʈ Ÿ�� enum
    public enum EventType
    { Sowing = 0, Fertilizing = 1, Watering = 2, AwayingRabbit = 3, Pruning = 4, Removing = 5 }
    public EventType eventType;

    //�̺�Ʈ ����ü�� �Ӽ�
    public struct EventFrame
    {
        public int upHP, downHP, upEXP;
    }
    public EventFrame[] frames = new EventFrame[6];

    IEnumerator coroutine;

    void Start()
    {
        eventAudio = GetComponent<AudioSource>();

        //�ڷ�ƾ �Ҵ�
        coroutine = TimeLimit(coolTime);

        #region �̺�Ʈ ����ü �迭 �ʱ�ȭ(�̺�Ʈ ����)
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

        print("�ð����� �ڷ�ƾ ����");
        Debug.Log(cool);

        eventGauge.SetActive(true);
        //�̺�Ʈ ��Ÿ�� �ð�ȭ
        eventGauge.GetComponent<Image>().fillAmount = 0;

        while (tmp <= cool)
        {
            tmp += Time.deltaTime;
            eventGauge.GetComponent<Image>().fillAmount = (tmp / cool);
            yield return new WaitForFixedUpdate();
        }

        isEvent = false;
        //Fail() ȣ��
        Fail();
        eventGauge.SetActive(false);

        print("�ð����� �ڷ�ƾ �Ϸ�");

    }

    public IEnumerator DelayTime()
    {
        eventDelay = UnityEngine.Random.Range(minDelay, maxDelay);

        while (eventDelay > 0)
        {
            eventDelay -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //�ڷ�ƾ �Ϸ� �� EventAppear()ȣ��
        EventAppear();

        print("�̺�Ʈ ������ �ڷ�ƾ �Ϸ�" + eventDelay);
    }

    public void Success()
    {
        //���� �����ؾ� �Ǹ� �̷���
        //�ƴϸ� �״��
        if((int)eventType==5)
        {
            plant.ResetPlant();

            eventAudio.clip= SoundManager.instance.GetSFX("Shovel");
            eventAudio.Play();
        }
        else
        {
            //�̺�Ʈ ���ѽð� �ڷ�ƾ �ߴ�
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
                    Debug.LogWarning("�߸��� ���� �Ҵ��Դϴ�.");
                    break;
            }

            //GrowUp()�Լ� ȣ��
            plant.GrowUp(frames[(int)eventType]);
        }
    }

    public void Fail()
    {
        //Wither()�Լ� ȣ��
        plant.Wither(frames[(int)eventType]);
        coroutine = TimeLimit(coolTime);
    }

    public void EventAppear()
    {
        //�̺�Ʈ ������Ʈ Ȱ��ȭ
        eventObj.SetActive(true);

        //�̺�Ʈ Ÿ�� ����
        //�Ĺ� �ܰ迡 ���� ȣ��� �̺�Ʈ �����ϰ� ����
        switch (plant.growGrade)
        {
            case 0:
                //1�ܰ�(���)�̸� ���� �̺�Ʈ ȣ��
                eventType = EventType.Sowing;
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];

                isEvent=true;
                break;

            case 1:
            case 2:
            case 3:
                //2~4�ܰ�� 1~3�� ���� ȣ��
                //�ڷ�ƾ ȣ��
                eventType = (EventType)UnityEngine.Random.Range(1, 4);
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];

                coroutine = TimeLimit(coolTime);
                StartCoroutine(coroutine);
                break;

            case 4:
                //4�ܰ�(�ø���)�� 1~4�� ����
                //�ڷ�ƾ ȣ��
                eventType = (EventType)UnityEngine.Random.Range(1, 5);
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];

                coroutine = TimeLimit(coolTime);
                StartCoroutine(coroutine);
                break;

            case 5:
                eventType = EventType.Removing;
                eventIcon.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                //5�ܰ�(�õ�)�̸� ���� �̺�Ʈ ȣ��

                isEvent = true;
                break;

            default:
                Debug.Log("Event Error!");
                break;
        }
    }
}
