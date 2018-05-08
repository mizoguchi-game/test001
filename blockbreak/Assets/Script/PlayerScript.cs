using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float Speed = 15.0f;
	
	// Update is called once per frame
	void Update () {
        var velox = Speed * Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody>().velocity = new Vector3(velox, 0f, 0f);

    }
}
