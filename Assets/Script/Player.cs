using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("PlayerMoveSetting")]
    public float incresePosInUpdate;

    [Header ("HoldingSetting")]
    public float minHoldingTime; // Ȧ�� Ŭ���ð�
    bool isHolding = true;
    float tmpTime;


    Transform transform;
    Rigidbody2D rigid;
    public ActionType actionType;

    public enum ActionType
    {
        Sowing = 0,
        Fertilizing = 1,
        Watering = 2,
        AwayingRabbit = 3,
        Pruning = 4,
        Removing = 5
    }

    private void Start()
    {
        tmpTime = minHoldingTime;
        transform = GetComponent<Transform>();
        rigid = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyUp(KeyCode.K))
        {
            isHolding = true;
            tmpTime = minHoldingTime;
        }

    }

    private void Move()
    {
        #region UpDown
        if (Input.GetKey(KeyCode.W))
        {
            rigid.velocity = new Vector3(0, incresePosInUpdate, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = new Vector3(0, -incresePosInUpdate, 0);
        }
        #endregion

        #region LeftRight
        else if (Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            rigid.velocity = new Vector3(-incresePosInUpdate, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            rigid.velocity = new Vector3(incresePosInUpdate, 0, 0);
        }
        else
        {
            //Ű ���� �� �ӵ� ����
            rigid.velocity = Vector3.zero;
        }
        #endregion
    }

    private void Action(Event eventObj)
    {
        if((int)eventObj.eventType==(int)actionType && eventObj.isEvent)
        {
            eventObj.Success();
        }
        
    }
    
    private void DecideActionType(int spotNum)
    {
        switch (spotNum)
        {
            case 0:
                actionType = ActionType.Sowing;
                break;
            case 1:
                actionType = ActionType.Fertilizing;
                break;
            case 2:
                actionType= ActionType.Watering;
                break;
            case 3:
                actionType = ActionType.AwayingRabbit;
                break;
            case 4:
                actionType = ActionType.Pruning;
                break;
            case 5:
                actionType = ActionType.Removing;
                break;
            default:
                Debug.LogWarning(spotNum + "�� �ùٸ��� ���� spotNum���Դϴ�.");
                break;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Plant":
                if (Input.GetKey(KeyCode.K)&&isHolding)    //Holding Start
                {
                    Debug.Log(tmpTime);
                    // Ȧ���ð� ����
                    tmpTime -= Time.deltaTime;

                    if ((int)collision.transform.GetComponent<Plant>().eventControl.eventType == (int)actionType && tmpTime <= 0 && isHolding)  //Ȧ�� �ð� �޼� ��
                    {
                        Debug.Log("�׼�");
                        isHolding = false;
                        Action(collision.transform.GetComponent<Plant>().eventControl);
                        //Ȧ���ð� �ʱ�ȭ
                    }
                }
                break;

            default:
                Debug.Log("On Trigger �ٸ� �±�");
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {                     
        int spotNum = -999;   //���ʱ�ȭ ��

        switch (collision.gameObject.tag)
        {
            case "Spot":
                if (Input.GetKey(KeyCode.L))    //Init actionType 
                {
                    #region Spot ���� �޾ƿ���
                    Debug.Log(collision.gameObject.name);
                    spotNum = collision.gameObject.GetComponent<Spot>().spotNumber;
                    DecideActionType(spotNum);
                    #endregion
                }
                break;

            default:
                Debug.LogWarning("������ ������Ʈ Tag : "+ collision.gameObject.tag + "����ü ������ �浹�ϽŰǰ���?");
                break;
        }
    }
}
