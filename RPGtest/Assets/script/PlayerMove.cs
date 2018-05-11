using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody rb;

    public float speed = 2f;
    public float thrust = 2;

    private Animator animator;

    bool ground;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ground)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                animator.SetBool("Running", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector3(-speed, 0, 0);
                transform.rotation = Quaternion.Euler(0, 270, 0);
                animator.SetBool("Running", true);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector3(0, 0, speed);
                transform.rotation = Quaternion.Euler(0,0,0);
                animator.SetBool("Running",true);
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector3(0, 0, -speed);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("Running",true);
            }
            else
            {
                animator.SetBool("Running",false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jumping",true);
            rb.AddForce(new Vector3(0,thrust,0));
            ground = false;
        }
        else
        {
            animator.SetBool("jumping", false);
        }
	}

    private void OnCollisionStay(Collision col)
    {
        ground = true;
    }
}
