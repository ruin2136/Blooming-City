using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Event eventUI;

    public int hp, exp;
    bool isComplete, isOlive;
    int growGrade, eventType, plantsType;
    Sprite[] plantSprites;

    //���� �Լ�
    public void GrowUp(Event.EventFrame frame)
    {
        //�Ű����� ���� hp, exp�� ����
        hp += frame.upHP;
        exp += frame.upEXP;

        //�˻� �� ���� �ܰ� ����, ��������Ʈ ����, isComplete ����
    }

    //�õ� �Լ�
    public void Wither(Event.EventFrame frame)
    {
        //�Ű����� ���� hp���� ��
        hp -= frame.downHP;

        //�˻� �� �� ���¿� ���� ���� �ܰ� ����
        //��������Ʈ ����, isComplete ����
        //�ܰ迡 ���� ��� �̺�Ʈ ȣ��
    }
}
