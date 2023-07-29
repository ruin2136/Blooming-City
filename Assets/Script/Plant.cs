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

    //성장 함수
    public void GrowUp(Event.EventFrame frame)
    {
        //매개변수 값을 hp, exp에 더함
        hp += frame.upHP;
        exp += frame.upEXP;

        //검사 후 성장 단계 변경, 스프라이트 변경, isComplete 판정
    }

    //시듦 함수
    public void Wither(Event.EventFrame frame)
    {
        //매개변수 값을 hp에서 뺌
        hp -= frame.downHP;

        //검사 후 현 상태에 따라 성장 단계 변경
        //스프라이트 변경, isComplete 판정
        //단계에 따라 상시 이벤트 호출
    }
}
