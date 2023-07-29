using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public Event eventControl;
    public GameObject plant;

    public int hp, exp;
    bool isComplete=false, isOlive;
    public int growGrade = 0;
    int eventType, plantsType;
    
    public Sprite[] poppySprites;
    public Sprite[] oliveSprites;
    Sprite[] plantSprites;

    //�Ĺ� ����ü�� �Ӽ�
    public struct PlantFrame
    {
        public int maxHP, maxEXP;
    }
    PlantFrame[] poppy = new PlantFrame[6];
    PlantFrame[] olive = new PlantFrame[6];
    PlantFrame[] plants = new PlantFrame[6];

    void Start()
    {
        #region POPPY ����ü �迭 �ʱ�ȭ(�Ĺ� �ʿ� ����)
        poppy[0].maxHP = -1;
        poppy[0].maxEXP = 1;

        poppy[1].maxHP = 2;
        poppy[1].maxEXP = 2;

        poppy[2].maxHP = 2;
        poppy[2].maxEXP = 2;

        poppy[3].maxHP = 3;
        poppy[3].maxEXP = 0;

        poppy[4].maxHP = -1;
        poppy[4].maxEXP = -1;

        poppy[5].maxHP = -1;
        poppy[5].maxEXP = 0;
        #endregion

        #region OLIVE ����ü �迭 �ʱ�ȭ(�Ĺ� �ʿ� ����)
        olive[0].maxHP = -1;
        olive[0].maxEXP = 1;

        olive[1].maxHP = 2;
        olive[1].maxEXP = 2;

        olive[2].maxHP = 2;
        olive[2].maxEXP = 2;

        olive[3].maxHP = 2;
        olive[3].maxEXP = 2;

        olive[4].maxHP = 3;
        olive[4].maxEXP = 0;

        olive[5].maxHP = -1;
        olive[5].maxEXP = 0;
        #endregion

        //���� �� �ø������� �����ϰ� �Ǵ�

        OliveDecide();
    }

    //�ø������� �Ǵ�
    void OliveDecide()
    {
        isOlive = Random.value > 0.5f;

        if (isOlive)
        {
            plants = olive;
            plantSprites = oliveSprites;
        }
        else
        {
            plants = poppy;
            plantSprites = poppySprites;
        }
    }

    //���� �Լ�
    public void GrowUp(Event.EventFrame frame)
    {
        //�Ű����� ���� hp, exp�� ����
        hp += frame.upHP;
        exp += frame.upEXP;

        //�˻� �� ���� �ܰ� ����-�� �ʱ�ȭ, ��������Ʈ ����, isComplete ����
        //����ġ �˻�=����
        if (exp >= plants[growGrade].maxEXP && plants[growGrade].maxEXP!=0)
        {
            growGrade += 1;
            plant.GetComponent<SpriteRenderer>().sprite = plantSprites[growGrade];

            if(growGrade==3||growGrade==4)
                isComplete = true;

            hp=plants[growGrade].maxHP;
            exp=0;
        }
        eventControl.eventObj.SetActive(false);

        //�̺�Ʈ ������ �ڷ�ƾ ȣ��
        StartCoroutine(eventControl.DelayTime());
    }

    //�õ� �Լ�
    public void Wither(Event.EventFrame frame)
    {
        //�Ű����� ���� hp���� ��
        hp -= frame.downHP;

        //�˻� �� �� ���¿� ���� ���� �ܰ� ����
        //��������Ʈ ����, isComplete ����
        //�ܰ迡 ���� ��� �̺�Ʈ ȣ��
        //ü�� �˻�=�õ� ����
        if (hp <= plants[growGrade].maxHP)
        {
            if (growGrade >= 2)
            {
                growGrade = 5;
                plant.GetComponent<SpriteRenderer>().sprite= plantSprites[growGrade];
                isComplete = false;

                //���� �̺�Ʈ ȣ�� �� �ѱ�
                eventControl.EventAppear();
                eventControl.eventObj.SetActive(true);

                hp = plants[growGrade].maxHP;
                exp = plants[growGrade].maxEXP;
            }
            else
            {
                growGrade = 0;
                plant.GetComponent<SpriteRenderer>().sprite = plantSprites[growGrade];
                isComplete = false;

                //���� �̺�Ʈ ȣ�� �� �ѱ�
                eventControl.EventAppear();
                eventControl.eventObj.SetActive(true);

                hp = plants[growGrade].maxHP;
                exp = plants[growGrade].maxEXP;
            }
        }
        else
        {
            eventControl.eventObj.SetActive(false);

            //�̺�Ʈ ������ �ڷ�ƾ ȣ��
            StartCoroutine(eventControl.DelayTime());
        }
    }
}
