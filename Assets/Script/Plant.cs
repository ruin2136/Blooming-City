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

    //식물 구조체와 속성
    public struct PlantFrame
    {
        public int maxHP, maxEXP;
    }
    PlantFrame[] poppy = new PlantFrame[6];
    PlantFrame[] olive = new PlantFrame[6];
    PlantFrame[] plants = new PlantFrame[6];

    void Start()
    {
        #region POPPY 구조체 배열 초기화(식물 필요 값들)
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

        #region OLIVE 구조체 배열 초기화(식물 필요 값들)
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

        //시작 시 올리브인지 랜덤하게 판단

        OliveDecide();
    }

    //올리브인지 판단
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

    //성장 함수
    public void GrowUp(Event.EventFrame frame)
    {
        //매개변수 값을 hp, exp에 더함
        hp += frame.upHP;
        exp += frame.upEXP;

        //검사 후 성장 단계 변경-값 초기화, 스프라이트 변경, isComplete 판정
        //경험치 검사=성장
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

        //이벤트 딜레이 코루틴 호출
        StartCoroutine(eventControl.DelayTime());
    }

    //시듦 함수
    public void Wither(Event.EventFrame frame)
    {
        //매개변수 값을 hp에서 뺌
        hp -= frame.downHP;

        //검사 후 현 상태에 따라 성장 단계 변경
        //스프라이트 변경, isComplete 판정
        //단계에 따라 상시 이벤트 호출
        //체력 검사=시듦 판정
        if (hp <= plants[growGrade].maxHP)
        {
            if (growGrade >= 2)
            {
                growGrade = 5;
                plant.GetComponent<SpriteRenderer>().sprite= plantSprites[growGrade];
                isComplete = false;

                //제거 이벤트 호출 및 켜기
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

                //씨앗 이벤트 호출 및 켜기
                eventControl.EventAppear();
                eventControl.eventObj.SetActive(true);

                hp = plants[growGrade].maxHP;
                exp = plants[growGrade].maxEXP;
            }
        }
        else
        {
            eventControl.eventObj.SetActive(false);

            //이벤트 딜레이 코루틴 호출
            StartCoroutine(eventControl.DelayTime());
        }
    }
}
