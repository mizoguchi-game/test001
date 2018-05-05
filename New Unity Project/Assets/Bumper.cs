using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public float Bounce = 10f;

    private void OnCollisionEnter(Collision other) {
        int randam = Random.Range(-1, 2);
        int randam2 = Random.Range(1, 5);
        if (other.gameObject.tag == "Ball")
        {
            other.rigidbody.AddForce(Bounce *-1, Bounce / 6, randam*randam2, ForceMode.Impulse);
        }
    }
}
