using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private CharacterController characterController;
    private Vector3 velocity;

    public float Walk = 2f;
    public float Run = 4f;
    public float thrust = -2;
    public float rotateSpeed = 2;

    private float speed = 0f;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && v > 0.1)
        {
            speed = v * Run;
        }
        else
        {
            speed = v * Walk;
        }

        if (characterController.isGrounded)
        {
            velocity = new Vector3(v,0f, 0f);
        }

        /*characterの回転*/
        transform.Rotate(0f, h * rotateSpeed, 0f);

        /*キャラクターの移動*/
        /*velocity = transform.TransformDirection(velocity);
        characterController.Move(velocity * speed * Time.deltaTime);*/
	}
}
