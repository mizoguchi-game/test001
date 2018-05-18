using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        var randDestination = Random.insideUnitSphere * 8;
        startPoint = transform.position;
        destination = startPoint + new Vector3(randDestination.x, 0f, randDestination.y);
        velocity = Vector3.zero;
        arrived = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyController.isGrounded)
        {
            velocity = Vector3.zero;
            animator.SetFloat("speed",2.0f);
            direction = (destination - transform.position).normalized;
            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
            velocity = direction * walkSpeed;
            Debug.Log(destination);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);

        if(Vector3.Distance(transform.position,destination)< 0.5f)
        {
            arrived = true;
            animator.SetFloat("speed", 0.0f);
        }
	}
}
