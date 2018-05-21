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

	public enum MyState
    {
        Normal,
        Damage,
        Attack
    }

    private MyState state;
    
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        if (state == MyState.Normal) {
            if (ground)
            {

                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

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
            animator.SetTrigger("Attack");
        }
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
}
