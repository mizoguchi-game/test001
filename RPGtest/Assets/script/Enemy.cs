using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private SetPositon setPositon;
    private CharacterController enemyController;
    private Animator animator;
    private EnemyStatus enemyStatus;
    [SerializeField]
    private BoxCollider leftHandCollider;
    [SerializeField]
    private BoxCollider rightHandCollider;

    //エージェント
    private NavMeshAgent agent;
    //回転スピード
    [SerializeField]
    private float rotateSpeed = 45f;

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
        Freeze,
        Damage,
        Dead
    };

    [SerializeField]
    private float freezeTime = 3f;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

        setPositon = GetComponent<SetPositon>();
        setPositon.CreateRandomPosition();
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState("wait");
        enemyStatus = GetComponent<EnemyStatus>();
	}

    // Update is called once per frame
    /*
  void Update() {
      if (state == EnemyState.Dead)
      {
          return;
      }
      //Debug.Log(state);
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
              //Debug.Log("InfreezeTime");
              //Debug.Log("freezeTime:"+freezeTime);
          }
      }
      velocity.y += Physics.gravity.y * Time.deltaTime;
      enemyController.Move(velocity * Time.deltaTime);
  }
  */
    //agent用アップデート
    void Update()
    {
        if (state == EnemyState.Dead)
        {
            return;
        }
        //Debug.Log(state);
        //見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase)
            {
                setPositon.setDestination(playerTranform.position);
                agent.SetDestination(setPositon.GetDestination());
            }

            //エージェントの潜在的な速さを設定
            animator.SetFloat("Speed",agent.desiredVelocity.magnitude);

            if (state == EnemyState.Walk)
            {
                //目的地に到着したかどうかの判定
                if (agent.remainingDistance < 0.7f)
                {
                    SetState("wait");
                    animator.SetFloat("speed", 0.0f);
                }
            }
            else if (state == EnemyState.Chase)
            {
                //攻撃する距離の場合攻撃
                if (agent.remainingDistance < 2f)
                {
                    SetState("attack");
                }
            }
            //目的地に到着していた場合一定時間待機
        }
        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //待ち時間を超えたら次の目的を設定
            if (elapsedTime > waitTime)
            {
                SetState("walk");
            }
        }
        //攻撃後のフリーズ状態
        else if (state == EnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                SetState("walk");
                //Debug.Log("InfreezeTime");
                //Debug.Log("freezeTime:"+freezeTime);
            }
        }
        else if(state == EnemyState.Attack)
        {
            //プレイヤーの方向を取得
            var playerDirection = new Vector3(playerTranform.position.x,transform.position.y,playerTranform.position.z) - transform.position;
            //敵の向きをプレイヤーの方向に少しづつ変える
            var dir = Vector3.RotateTowards(transform.forward, playerDirection, rotateSpeed * Time.deltaTime, 0f);
            //算出した方向の角度を敵の角度に設定
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    //ダメージを受けた時に当たった位置だけを受け取りる
    public void TakeDamage(int damage,Vector3 hitPoint)
    {
        if(state != EnemyState.Dead)
        {
            leftHandCollider.enabled = false;
            rightHandCollider.enabled = false;
            SetState("Damage");
            enemyStatus.SetHp(enemyStatus.GetHp() - damage);
        }
    }

    //ダメージを受けた時に受けた場所のtransfromを受けた位置を受け取る
    public void TakeDamage(int damage,Transform tra,Vector3 hitPoint)
    {
        if (state != EnemyState.Dead)
        {
            leftHandCollider.enabled = false;
            rightHandCollider.enabled = false;
            SetState("Damage");
            enemyStatus.SetHp(enemyStatus.GetHp() - damage);
        }
    }

    //キャラクターコントローラー用
    /*
    public void SetState(string mode,Transform obj = null)
    {
        //死んでいたいた場合状態変更を行わない。
        if (state == EnemyState.Dead)
        {
            return;
        }
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
            //Debug.Log("InAttack");
        }
        else if (mode == "freeze")
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            state = EnemyState.Freeze;
            animator.SetFloat("speed", 0f);
            animator.SetBool("Attack", false);
        }else if(mode == "Damage")
        {
            state = EnemyState.Damage;
            velocity = Vector3.zero;
            animator.SetTrigger("Damage");
        }else if(mode == "Dead")
        {
            state = EnemyState.Dead;
            velocity = Vector3.zero;
        }
    }
    */

    //ナビメッシュ用
    public void SetState(string mode, Transform obj = null)
    {
        //死んでいたいた場合状態変更を行わない。
        if (state == EnemyState.Dead)
        {
            return;
        }
        if (mode == "walk")
        {
            arrived = false;
            elapsedTime = 0f;
            state = EnemyState.Walk;
            setPositon.CreateRandomPosition();
            agent.SetDestination(setPositon.GetDestination());
            agent.isStopped = false;
        }
        else if (mode == "chase")
        {
            state = EnemyState.Chase;
            arrived = false;
            playerTranform = obj;
            setPositon.setDestination(playerTranform.position);
            agent.SetDestination(setPositon.GetDestination());
            agent.isStopped = false;

        }
        else if (mode == "wait")
        {
            elapsedTime = 0f;
            state = EnemyState.Wait;
            arrived = true;
            animator.SetFloat("speed", 0f);
            agent.isStopped = true;
        }
        else if (mode == "attack")
        {
            state = EnemyState.Attack;
            animator.SetFloat("speed", 0f);
            agent.isStopped = true;
            animator.SetBool("Attack", true);
        }
        else if (mode == "freeze")
        {
            elapsedTime = 0f;
            state = EnemyState.Freeze;
            animator.SetFloat("speed", 0f);
            animator.SetBool("Attack", false);
            agent.isStopped = true;
        }
        else if (mode == "Damage")
        {
            state = EnemyState.Damage;
            animator.SetTrigger("Damage");
            agent.isStopped = true;
        }
        else if (mode == "Dead")
        {
            state = EnemyState.Dead;
            enemyController.enabled = false;
            agent.isStopped = true;
        }
    }


    public void Dead()
    {
        SetState("Dead");
        animator.ResetTrigger("Damage");
        animator.SetTrigger("Dead");
        Destroy(this.gameObject, 3f);
    }

    public EnemyState GetState()
    {
        return state;
    }

}
