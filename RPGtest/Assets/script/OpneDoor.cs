﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpneDoor : MonoBehaviour {

    //ドアのエリアに入っているか。
    private bool isNear;
    //ドアのアニメーター
    private Animator animator;

	// Use this for initialization
	void Start () {
        isNear = false;
        animator = transform.parent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space") && isNear)
        {
            animator.SetBool("Open", !animator.GetBool("open"));
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (true)
        {

        }
    }
}
