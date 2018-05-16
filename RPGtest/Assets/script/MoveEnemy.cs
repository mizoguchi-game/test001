using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private CharacterController enemyController;
    private Vector3 destination;
    //歩行速度
    private float walkspeed = 1.0f;
    private Vector3 velocity;
    private Vector3 direction;
    private bool arrived;
    private Vector3 startPosition;

    private SetPosition setPosition;
    //ウェイトタイムの指定
    private float waitTime = 5f;
    //経過時間
    private float elapsedtime;

    //Enemyの状態を列挙
    public enum EnamyState
    {
        Walk,
        Wait,
        Chase
    };

    //Enemyの状態
    private EnamyState State;
    //
    private Transform PlayerTransform;


    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        setPosition = GetComponent<SetPosition>();
        //移動先を取得
        setPosition.CreateRandomPosition();
        //移動速度を0クリア
        velocity = Vector3.zero;
        //到着フラグをfalseで初期化
        arrived = false;
        //現在位置をオブジェクトの位置から取得
        startPosition = transform.position;
        //経過時間をリセット
        elapsedtime = 0f;
        SetState("wait");
    }

    void Update()
    {
        if (!arrived)
        {
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                direction = (destination - transform.position).normalized;
                transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                velocity = direction * walkspeed;
                Debug.Log(destination);
            }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            enemyController.Move(velocity * Time.deltaTime);

            //目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position,destination) < 0.5f)
            {
                arrived = true;
            }
        }
        else
        {
            elapsedtime += Time.deltaTime;

            if (elapsedtime > waitTime)
            {
                setPosition.CreateRandomPosition();
                destination = setPosition.GetDestination();
                arrived = false;
                elapsedtime = 0;
            }
            Debug.Log(elapsedtime);
        }
    }

    public void SetState(string mode,Transform obj = null)
    {
        if(mode == "walk")
        {
            arrived = false;
            elapsedtime = 0f;
            State = EnamyState.Walk;
            setPosition.CreateRandomPosition();
        }
        else if(mode == "Chase")
        {
            State = EnamyState.Chase;
            arrived = false;
            PlayerTransform = obj;
        }else if (mode == "wait")
        {
            elapsedtime = 0f;
            State = EnamyState.Wait;
            arrived = true;
            velocity = Vector3.zero;
        }
    }

    public EnamyState GetState()
    {
        return State;
    }
}

