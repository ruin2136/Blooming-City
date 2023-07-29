using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("PlayerMoveSetting")]
    public float incresePosInUpdate;

    [Header ("HoldingSetting")]
    public float minHoldingTime; // 홀딩 클릭시간
    private float nowholdedTime; // 홀딩 중인 시간

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
                Debug.LogWarning(spotNum + "는 올바르지 않은 spotNum값입니다.");
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {                     
        int spotNum = -999;   //비초기화 값

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
                    // 홀딩시간 측정
                    nowholdedTime += Time.deltaTime;

                    if(nowholdedTime > minHoldingTime)  //홀딩 시간 달성 시
                    {
                        Action(actionType);
                    }
                }
                else //Holding End
                {
                    nowholdedTime = 0;  // 홀딩시간 초기화
                }
                break;

            default:
                Debug.LogWarning("접촉한 오브젝트 Tag : "+ collision.tag + "도대체 누구랑 충돌하신건가요?");
                break;
        }
    }
}
