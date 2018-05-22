using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceesMyAttak : MonoBehaviour {

    private PlayerMove playerMove;
    private BoxCollider boxCollider;
    private Animator animator;

	// Use this for initialization
	void Start () {
        playerMove = GetComponent<PlayerMove>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        animator = GetComponent<Animator>();
	}
	
	void AttackStart()
    {
        boxCollider.enabled = true;
        Debug.Log("AttackStart");
    }

    void AttackEnd()
    {
        boxCollider.enabled = false;
        Debug.Log("AttackEnd");
    }

    void StateEnd()
    {
        playerMove.SetState(PlayerMove.MyState.Normal);
        Debug.Log("StateEnd");
    }

    public void EndDamage()
    {
        playerMove.SetState(PlayerMove.MyState.Normal);
        Debug.Log("EndDmage");
    }
}
