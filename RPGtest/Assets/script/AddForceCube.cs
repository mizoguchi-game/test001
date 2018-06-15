using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceCube : MonoBehaviour {

    //blockレイヤーを持つゲームオブジェクトと接触したら力を加える
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            col.rigidbody.AddForce(col.contacts[0].normal * 100f);
        }
    }
}
