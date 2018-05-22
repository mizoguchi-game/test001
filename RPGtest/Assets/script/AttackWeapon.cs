using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("呼ばれた");
        if (other.tag == "Enemy")
        {
            Debug.Log("敵にあたった");
            other.GetComponent<Enemy>().SetState("Damage");
        }
    }
}
