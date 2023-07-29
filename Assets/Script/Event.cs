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

    //�̺�Ʈ Ÿ�� enum
    public enum EventType
    { seed = 0, fertilizer = 1, water = 2, rabbit = 3, pruning = 4, remove = 5 }
    public EventType eventType;

    //�̺�Ʈ ����ü�� �Ӽ�
    public struct EventFrame
    {
        public int upHP, downHP, upEXP, timeLimit;
    }
    EventFrame[] frames = new EventFrame[6];

    void Start()
    {
        #region ����ü �迭 �ʱ�ȭ(�̺�Ʈ ����)
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

        //�׽�Ʈ
        StartCoroutine(CoolTime(cool));
        StartCoroutine(DelayTime());
    }

    IEnumerator CoolTime(float cool)
    {
        print("��Ÿ�� �ڷ�ƾ ����");

        //�̺�Ʈ ��Ÿ�� �ð�ȭ
        gameObject.GetComponent<Image>().fillAmount = 0;

        while (cool > 0f)
        {
            cool -= Time.deltaTime;
            gameObject.GetComponent<Image>().fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        //Fail() ȣ��
        Fail();

        print("��Ÿ�� �ڷ�ƾ �Ϸ�");
    }

    IEnumerator DelayTime()
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

    void Scuccess()
    {
        //�̺�Ʈ ���ѽð� �ڷ�ƾ �ߴ�
        StopCoroutine(CoolTime(cool));

        //GrowUp()�Լ� ȣ��
        plant.GrowUp(frames[(int)eventType]);

        //�̺�Ʈ ����, �̺�Ʈ ������ �ڷ�ƾ ȣ��
        StartCoroutine(DelayTime());
    }

    void Fail()
    {
        //Wither()�Լ� ȣ��
        plant.Wither(frames[(int)eventType]);

        //�̺�Ʈ ����, �̺�Ʈ ������ �ڷ�ƾ ȣ��
        StartCoroutine(DelayTime());
    }

    void EventAppear()
    {
        //�̺�Ʈ ���ѽð� �ڷ�ƾ ȣ��
        StartCoroutine(CoolTime(cool));

        //������ �̺�Ʈ ȣ��(���ѵ�)
        //�Ĺ� �ܰ�, ������ ���� ȣ��� �̺�Ʈ �����ϰ� ����
        eventType = EventType.rabbit;
    }
}
