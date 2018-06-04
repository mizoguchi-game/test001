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
    private float arrivedDistance = 0.5f;
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
		
	}
}
