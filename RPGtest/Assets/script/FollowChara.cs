using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowChara : MonoBehaviour {

    private NavMeshAgent agent;

    //追いかけるキャラクター
    [SerializeField]
    private GameObject player;
    private Animator animator;
    //到着したとする距離
    [SerializeField]
    private float arrivedDistance = 1f;
    //追いかけ始める距離
    [SerializeField]
    private float followDistance = 1f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(player.transform.position);

        //到着していない時
        if (agent.remainingDistance < arrivedDistance)
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
        }
        //到着している時
        else if (agent.remainingDistance > followDistance)
        {
            agent.isStopped = false;
            animator.SetFloat("Speed",agent.desiredVelocity.magnitude);
        }
	}

    private void OnAnimatorIK()
    {
        var weighi = Vector3.Dot(transform.forward,player.transform.position - transform.position);

        if (weighi < 0)
        {
            weighi = 0;
        }

        animator.SetLookAtWeight(weighi, 0f, 1f, 0f, 0f);
        animator.SetLookAtPosition(player.transform.position + Vector3.up * 1.5f);
    }
}
