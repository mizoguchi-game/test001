using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStoneFrog : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("あたり");
            other.GetComponent<PlayerMove>().TakeDamage(transform.root);
        }
    }
}
