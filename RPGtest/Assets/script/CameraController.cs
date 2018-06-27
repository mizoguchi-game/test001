using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float rotetaspeed = 5f;
    private Vector3 targetPos = new Vector3();
    private Vector3 inFpsPos = new Vector3();
    private bool fpsMode = false;
    private Vector3 tpsPos = new Vector3();
    private float playerMoveAngle = 0f;
    private float playerAngleY;
    private float Angular_diff;

    // Use this for initialization
    void Start () {
        targetPos = player.transform.position;
        inFpsPos = transform.position;
        playerAngleY = player.transform.root.eulerAngles.y;
    }
	
	// Update is called once pedr frame
	void FixedUpdate() {
        RotateCameraAngele();
    }

    private void RotateCameraAngele()
    {
        Angular_diff = (playerAngleY - player.transform.root.eulerAngles.y)*-1;
        playerAngleY = player.transform.root.eulerAngles.y;

        float zoom = Input.GetAxis("Mouse ScrollWheel");
        float angleX = Input.GetAxis("Mouse X") * rotetaspeed;
        float angleY = Input.GetAxis("Mouse Y") * rotetaspeed;

        transform.position += player.transform.position - targetPos;
        targetPos = player.transform.position;

        if (Vector3.Distance(targetPos, transform.position + transform.forward * zoom) < 1f && fpsMode == false)
        {
            fpsMode = true;
            inFpsPos = transform.position;
            transform.position = targetPos;
            transform.Rotate(transform.eulerAngles * -1);
        }
        else if (zoom < 0 && fpsMode == true)
        {
            fpsMode = false;
            transform.position = transform.position + transform.forward * -1.1f;
        }

        if (fpsMode == false) 
        {
            transform.position += transform.forward * zoom;
            transform.LookAt(player.transform.position + new Vector3(0f, Vector3.Distance(targetPos, transform.position) / 3, 0f));
            transform.RotateAround(player.transform.position, Vector3.up, angleX * Time.deltaTime + Angular_diff);
            transform.RotateAround(player.transform.position, transform.right, angleY * Time.deltaTime * -1);
        }
        else
        {
            //transform.position += transform.forward * zoom;
            transform.Rotate(Vector3.up,angleX * Time.deltaTime + Angular_diff);
            transform.Rotate(transform.right,angleY * Time.deltaTime*-1);
            transform.Rotate(new Vector3(0, 0, 1),transform.eulerAngles.z * -1);

            /*上下角-+180度 左右角80度*/
        }
    }
}
