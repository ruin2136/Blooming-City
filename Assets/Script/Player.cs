using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("PlayerMoveSetting")]
    public float incresePosInUpdate;

    [Header ("HoldingSetting")]
    public float minHoldingTime; // Ȧ�� Ŭ���ð�
    private float nowholdedTime; // Ȧ�� ���� �ð�

    Transform transform;
    ActionType actionType;

    public enum ActionType
    {
        Fertilizing = 0,
        Sowing = 1,
        Wartering = 2,
        AwayingRabbit = 3,
        Pruning = 4,
        Removing = 5
    }

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        #region UpDown
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + new Vector3(0, incresePosInUpdate, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + new Vector3(0, -incresePosInUpdate, 0);
        }
        #endregion

        #region LeftRight
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-incresePosInUpdate, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(incresePosInUpdate, 0, 0);
        }
        #endregion
    }

    private void Action(ActionType actionType)
    {

    }
    
    private void DecideActionType(int spotNum)
    {
        switch (spotNum)
        {
            case 0:
                actionType = ActionType.Fertilizing;
                break;
            case 1:
                actionType = ActionType.Sowing;
                break;
            case 2:
                actionType= ActionType.Wartering;
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
        int spotNum = -999;   //���ʱ�ȭ ��

        switch (collision.tag)
        {
            case "Spot":
                if (Input.GetKeyDown(KeyCode.L))    //Init actionType 
                {
                    spotNum = collision.gameObject.GetComponent<Spot>().spotNumber;
                    DecideActionType(spotNum);
                }
                break;

            case "Plant":
                if (Input.GetKeyDown(KeyCode.K))    //Holding Start
                {
                    // Ȧ���ð� ����
                    nowholdedTime += Time.deltaTime;

                    if(nowholdedTime > minHoldingTime)  //Ȧ�� �ð� �޼� ��
                    {
                        Action(actionType);
                    }
                }
                else //Holding End
                {
                    nowholdedTime = 0;  // Ȧ���ð� �ʱ�ȭ
                }
                break;

            default:
                Debug.LogWarning("������ ������Ʈ Tag : "+ collision.tag + "����ü ������ �浹�ϽŰǰ���?");
                break;
        }
    }
}
