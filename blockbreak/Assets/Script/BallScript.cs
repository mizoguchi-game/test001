﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public float Speed = 15.0f;

    private Vector3 vector3 = new Vector3(0, 0, 0);
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Jump") && transform.position == vector3)
        {
            GetComponent<Rigidbody>().AddForce((transform.forward + transform.right) * Speed, ForceMode.VelocityChange);
        }
	}

    private void OnCollisionEnter(Collision other)
    {

        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * Speed;

        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < 5)
        {
            GetComponent<Rigidbody>().velocity =new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y * 5, GetComponent<Rigidbody>().velocity.z);
        }

        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) < 5)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x *5 , GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }
    }
}
