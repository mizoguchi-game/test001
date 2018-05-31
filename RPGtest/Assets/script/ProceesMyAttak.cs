﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceesMyAttak : MonoBehaviour {

    private PlayerMove playerMove;
    private Collider weaponCollider;
    private Animator animator;

    [SerializeField]
    private Transform equip;

	// Use this for initialization
	void Start () {
        playerMove = GetComponent<PlayerMove>();
        weaponCollider = equip.GetComponentInChildren<Collider>();
        animator = GetComponent<Animator>();
	}
	
	void AttackStart()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
        
        Debug.Log("AttackStart");
    }

    void AttackEnd()
    {
        if (weaponCollider != null) { weaponCollider.enabled = false; }
        
        Debug.Log("AttackEnd");
    }

    void StateEnd()
    {
        playerMove.SetState(PlayerMove.MyState.Normal);
        Debug.Log("StateEnd");
    }

    void EndDamage()
    {
        playerMove.SetState(PlayerMove.MyState.Normal);
        Debug.Log("EndDmage");
    }

    public void SetCollider(Collider col)
    {
        weaponCollider = col;
    }

    public void ReadyShot()
    {
        playerMove.SetReadyShot();
        Debug.Log("AIManimation終了");
    }
}
