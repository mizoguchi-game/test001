using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBlockScript : MonoBehaviour {

    public int hp = 2;

    private void OnCollisionEnter(Collision collision)
    {
        hp--;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
