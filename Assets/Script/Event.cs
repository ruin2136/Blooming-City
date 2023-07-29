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

    //�̺�Ʈ ���ѽð�
    public float coolTime;

    public Sprite[] eventSprites;
    float eventDelay;
    public int maxDelay, minDelay;

    //�̺�Ʈ Ÿ�� enum
    public enum EventType
    { seed = 0, fertilizer = 1, water = 2, rabbit = 3, pruning = 4, remove = 5 }
    public EventType eventType;

    //�̺�Ʈ ����ü�� �Ӽ�
    public struct EventFrame
    {
        public int upHP, downHP, upEXP;
    }
    EventFrame[] frames = new EventFrame[6];

    void Start()
    {
        #region �̺�Ʈ ����ü �迭 �ʱ�ȭ(�̺�Ʈ ����)
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

        //�׽�Ʈ
        StartCoroutine(CoolTime(coolTime));
        StartCoroutine(DelayTime());
    }

    public IEnumerator CoolTime(float cool)
    {
        print("��Ÿ�� �ڷ�ƾ ����");

        //�̺�Ʈ ��Ÿ�� �ð�ȭ
        eventObj.GetComponent<Image>().fillAmount = 0;

        while (cool > 0f)
        {
            cool -= Time.deltaTime;
            eventObj.GetComponent<Image>().fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        //Fail() ȣ��
        Fail();

        print("��Ÿ�� �ڷ�ƾ �Ϸ�");
    }

    public IEnumerator DelayTime()
    {
        eventDelay = UnityEngine.Random.Range(minDelay, maxDelay);

        print("�̺�Ʈ ������ �ڷ�ƾ ����" + eventDelay);

        while (eventDelay > 0)
        {
            eventDelay -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //�ڷ�ƾ �Ϸ� �� EventAppear()ȣ��
        EventAppear();

        print("�̺�Ʈ ������ �ڷ�ƾ �Ϸ�" + eventDelay);
    }

    void Success()
    {
        //�̺�Ʈ ���ѽð� �ڷ�ƾ �ߴ�
        StopCoroutine(CoolTime(coolTime));

        //GrowUp()�Լ� ȣ��
        plant.GrowUp(frames[(int)eventType]);

        //�̺�Ʈ ������Ʈ ����
        eventObj.SetActive(false);
    }

    void Fail()
    {
        //Wither()�Լ� ȣ��
        plant.Wither(frames[(int)eventType]);

        //�̺�Ʈ ������Ʈ ����
        eventObj.SetActive(false);
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
                eventType = EventType.seed;
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                break;

            case 1:
            case 2:
            case 3:
                //2~4�ܰ�� 1~3�� ���� ȣ��
                //�ڷ�ƾ ȣ��
                eventType = (EventType)UnityEngine.Random.Range(1, 3);
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                StartCoroutine(CoolTime(coolTime));
                break;

            case 4:
                //4�ܰ�(�ø���)�� 1~4�� ����
                //�ڷ�ƾ ȣ��
                eventType = (EventType)UnityEngine.Random.Range(1, 4);
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                StartCoroutine(CoolTime(coolTime));
                break;

            case 5:
                eventType = EventType.remove;
                eventObj.GetComponent<Image>().sprite = eventSprites[(int)eventType];
                //5�ܰ�(�õ�)�̸� ���� �̺�Ʈ ȣ��
                break;

            default:
                Debug.Log("Event Error!");
                break;
        }
    }
}
