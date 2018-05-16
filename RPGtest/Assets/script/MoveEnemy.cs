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
    private EnamyState state;
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
        //経過時間をリセット
        elapsedtime = 0f;
        SetState("wait");
    }

    void Update()
    {
        if (state == EnamyState.Walk || state == EnamyState.Chase)
        {
            if (state == EnamyState.Chase)
            {
                setPosition.SetDestination(PlayerTransform.position);
            }

            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                direction = (destination - transform.position).normalized;
                transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                velocity = direction * walkspeed;
            }
            //目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 0.5f)
            {
                SetState("wait");
            } 
        }
        else if(state == EnamyState.Wait)
        {
            elapsedtime += Time.deltaTime;

            if (elapsedtime > waitTime)
            {
                SetState("walk");
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);
    }

    public void SetState(string mode,Transform obj = null)
    {
        if(mode == "walk")
        {

            elapsedtime = 0f;
            state = EnamyState.Walk;
            setPosition.CreateRandomPosition();
        }
        else if(mode == "Chase")
        {
            state = EnamyState.Chase;
 
            PlayerTransform = obj;
        }else if (mode == "wait")
        {
            elapsedtime = 0f;
            state = EnamyState.Wait;
 
            velocity = Vector3.zero;
        }
    }

    public EnamyState GetState()
    {
        return state;
    }
}

