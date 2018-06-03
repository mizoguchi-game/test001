using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessAttack : MonoBehaviour {

    private Enemy enemy;
    private AttackStoneFrog boxCollider;
    private Animator animator;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        boxCollider = GetComponentInChildren<AttackStoneFrog>();
        animator = GetComponent<Animator>();
    }

    void AttackStart()
    {
        boxCollider.enabledCollider(true);
        Debug.Log("AttackStart");
    }

    void AttackEnd()
    {
        boxCollider.enabledCollider(false);
        Debug.Log("AttackEnd");
    }

    void StateEnd()
    {
        enemy.SetState("freeze");
        //Debug.Log("StateEnd");
    }
}
