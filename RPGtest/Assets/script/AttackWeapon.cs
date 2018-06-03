using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour {

    private MyStatus myStatus;

    private void Start()
    {
        myStatus = transform.root.GetComponent<MyStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("呼ばれた");
        if (other.tag == "Enemy")
        {
            Debug.Log("敵にあたった");
            other.transform.root.GetComponent<Enemy>().TakeDamage(myStatus.GetAttackPower(),other.ClosestPointOnBounds(transform.position));
        }
    }
}
