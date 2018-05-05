using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.tag == "Ball"){
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
