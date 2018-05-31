using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody rb;

    public float Walk = 2f;
    public float Run = 4f;
    public float thrust = -2;
    public float rotateSpeed = 2;

    private Vector3 velocity;
    private float speed = 0f;
    private Animator animator;

    bool ground;

    private Shot shot;

    //IK用フィールド宣言
    //spineボーンのX軸の変更
    [SerializeField]
    private float xAsis = 0f;
    //spineボーンのX軸の変更限度値
    [SerializeField]
    private float xAxisLimit = 60f;
    //spineボーンのZ軸の変更値
    [SerializeField]
    private float zAxis = 0f;
    //spineボーンのZ軸の変更限度値
    [SerializeField]
    private float zAxisLimit = 30f;
    //銃を構えた時のSpineボーンのZ軸の角度
    [SerializeField]
    private float initZ = 1.193215f;
    //銃を構えた時のSpineボーンのX軸の角度
    [SerializeField]
    private float initX = 359.2921f;
    //回転する角度のスピード
    [SerializeField]
    private float armsrotateSpeed = 45f;
    //手ブレを使用するかどうか
    [SerializeField]
    private bool isHandBlur;








    //キャラクターのHPなどのステータスを格納
    private MyStatus myStatus;

	public enum MyState
    {
        Normal,
        Damage,
        Attack,
        WaitShot
    }

    //現在のキャラクター状態を格納
    private MyState state;
    
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        shot = GetComponent<Shot>();
        myStatus = transform.GetComponent<MyStatus>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {

        if (state == MyState.Normal) {
            if (ground)
            {

                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                //walk or Run　切り替え
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && v > 0.1)
                {
                    speed = v * Run;
                }
                else
                {
                    speed = v * Walk;
                }

                transform.Rotate(0f, h * rotateSpeed, 0f);
                animator.SetFloat("Walking", Mathf.Abs(speed));

                velocity = new Vector3(0, 0, speed);
                velocity = transform.TransformDirection(velocity);
                rb.velocity = velocity;

                rb.angularVelocity = new Vector3(0f, 0f, 0f);
            }
            //ジャンプ処理
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Junping", true);
                rb.AddForce(new Vector3(0, thrust, 0));
                ground = false;
            }
            else
            {
                animator.SetBool("Junping", false);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                SetState(MyState.Attack);
            }
            else if (Input.GetButton("Fire2") && myStatus.GetWeapomStatus() != null && myStatus.GetWeapomStatus().GetWeaponType() == "Gun")
            {
                SetState(MyState.WaitShot);
                animator.SetBool("WaitShot", true);
            }
        }
        else if(state == MyState.WaitShot)
        {
            //マウスの右ボタンを離したらnormal状態へ遷移
            if (!Input.GetButton("Fire2"))
            {
                animator.SetBool("WaitShot", false); ;
                shot.SetDisableLight();
                shot.SetDisableRaserPointer();
                SetState(MyState.Normal);
            }
            //構えた状態
            else
            {
                shot.SetAbleRaserPointer();
                if (Input.GetButtonDown("Fire1"))
                {
                    shot.SetAbleLight();
                    shot.JudgeShot();
                }
            }
        }
	}

    public void SetState(MyState myState)
    {
        if(myState == MyState.Normal)
        {
            animator.SetBool("Attack", false);
            state = MyState.Normal;
        }else if(myState == MyState.Attack)
        {
            velocity = Vector3.zero;
            state = MyState.Attack;
            animator.SetBool("Attack",true);
        }else if (myState == MyState.WaitShot)
        {
            state = MyState.WaitShot;
        }
    }

    public MyState GetState()
    {
        return state;
    }

    private void OnCollisionStay(Collision col)
    {
        ground = true;
    }

    public void TakeDamage(Transform enemyTransform)
    {
        state = MyState.Damage;
        velocity = Vector3.zero;
        animator.SetTrigger("Damage");
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
    }
}
