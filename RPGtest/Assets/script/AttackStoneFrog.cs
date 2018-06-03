using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStoneFrog : MonoBehaviour {

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    public void enabledCollider(bool bo)
    {
        boxCollider.enabled = bo;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("あたり");
            other.GetComponent<PlayerMove>().TakeDamage(transform.root);
        }
    }
}
