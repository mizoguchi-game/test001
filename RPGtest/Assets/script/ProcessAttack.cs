using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessAttack : MonoBehaviour {

    private Enemy enemy;
    private BoxCollider boxCollider;
    private Animator animator;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    void AttackStart()
    {
        boxCollider.enabled = true;
        //Debug.Log("AttackStart");
    }

    void AttackEnd()
    {
        boxCollider.enabled = false;
        //Debug.Log("AttackEnd");
    }

    void StateEnd()
    {
        enemy.SetState("freeze");
        //Debug.Log("StateEnd");
    }
}
