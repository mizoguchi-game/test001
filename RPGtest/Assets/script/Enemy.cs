using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private SetPositon setPositon;
    private CharacterController enemyController;
    private Animator animator;

    //目的地
    private Vector3 destination;
    //歩くスピード    
    private float walkSpeed = 1.0f;
    //速度
    private Vector3 velocity;
    //移動方向
    private Vector3 direction;
    //到着フラグ
    private bool arrived;
    //開始位置
    private Vector3 startPoint;
    //待ち時間
    private float waitTime = 5f;
    //経過時間
    private float elapsedTime;
    //敵の状態
    private EnemyState state;
    //追いかけるキャラクター
    private Transform playerTranform;


    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };

    [SerializeField]
    private float freezeTime = 50f;


	// Use this for initialization
	void Start () {
        setPositon = GetComponent<SetPositon>();
        setPositon.CreateRandomPosition();
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState("wait");
	}

    // Update is called once per frame
    void Update() {
        Debug.Log(state);
        //見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if(state == EnemyState.Chase)
            {
                setPositon.setDestination(playerTranform.position);
            }
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("speed", 2.0f);
                direction = (setPositon.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPositon.GetDestination().x, transform.position.y, setPositon.GetDestination().z));
                velocity = direction * walkSpeed;
            }

            if (state == EnemyState.Walk) { 
                //目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position,setPositon.GetDestination()) < 0.7f)
                {
                    SetState("wait");
                    animator.SetFloat("speed", 0.0f);
                }
            }
            else if (state == EnemyState.Chase)
            {
                if (Vector3.Distance(transform.position, setPositon.GetDestination()) < 1.5f)
                {
                    SetState("attack");
                }
            }
            //目的地に到着していた場合一定時間待機
        }else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //待ち時間を超えたら次の目的を設定
            if(elapsedTime > waitTime)
            {
                SetState("walk");
            }
        }else if (state == EnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                SetState("walk");
                Debug.Log("InfreezeTime");
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);
    }

    public void SetState(string mode,Transform obj = null)
    {
        if (mode == "walk")
        {
            arrived = false;
            elapsedTime = 0f;
            state = EnemyState.Walk;
            setPositon.CreateRandomPosition();
        }else if (mode == "chase")
        {
            state = EnemyState.Chase;
            arrived = false;
            playerTranform = obj;
        }else if(mode == "wait")
        {
            elapsedTime = 0f;
            state = EnemyState.Wait;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("speed", 0f);
        }else if(mode == "attack")
        {
            state = EnemyState.Attack;
            velocity = Vector3.zero;
            animator.SetFloat("speed", 0f);
            animator.SetBool("Attack", true);
            Debug.Log("InAttack");
        }
        else if (mode == "freeze")
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            state = EnemyState.Freeze;
            animator.SetFloat("speed", 0f);
            animator.SetBool("Attack", false);
            Debug.Log("Infreeze");
        }
    }

    public EnemyState GetState()
    {
        return state;
    }
}
