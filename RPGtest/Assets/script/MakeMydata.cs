using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMydata : MonoBehaviour {

    private MyData myData;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            myData = ScriptableObject.CreateInstance<MyData>();
            myData.myName = "Name";
            myData.hp = Random.Range(10, 50);
            myData.attackPower = 10f;
            Debug.Log(myData.myName + " : " + myData.hp + " : " + myData.attackPower);
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log(myData.myName + " : " + myData.hp + " : " + myData.attackPower);
        }
	}
}
