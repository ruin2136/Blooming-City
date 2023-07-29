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
    Rigidbody2D rigid;
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
        rigid = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Debug.Log(actionType);
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
            rigid.velocity = new Vector3(-incresePosInUpdate, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector3(incresePosInUpdate, 0, 0);
        }
        else
        {
            //Ű ���� �� �ӵ� ����
            rigid.velocity = Vector3.zero;
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

            case "Plant":
                if (Input.GetKey(KeyCode.K))    //Holding Start
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
                Debug.LogWarning("������ ������Ʈ Tag : "+ collision.gameObject.tag + "����ü ������ �浹�ϽŰǰ���?");
                break;
        }
    }
}
