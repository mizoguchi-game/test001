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

    // Use this for initialization
    void Start () {
        targetPos = player.transform.position;
        inFpsPos = transform.position;
    }
	
	// Update is called once pedr frame
	void FixedUpdate() {
        RotateCameraAngele();
    }

    private void RotateCameraAngele()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        float angleX = Input.GetAxis("Mouse X") * rotetaspeed + playerMoveAngle * Vector3.Distance(targetPos, transform.position + transform.forward * zoom)*3.15f*2*2;//円の中心回転速度と外周の回転速度同期の方法を調べる
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
            transform.position = inFpsPos;
        }

        Debug.Log(Vector3.Distance(targetPos, transform.position + transform.forward * zoom));

        if (fpsMode == false) 
        {
            transform.position += transform.forward * zoom;
            transform.LookAt(player.transform.position + new Vector3(0f, Vector3.Distance(targetPos, transform.position) / 3, 0f));
            transform.RotateAround(player.transform.position, Vector3.up, angleX * Time.deltaTime);
            transform.RotateAround(player.transform.position, transform.right, angleY * Time.deltaTime * -1);
        }
        else
        {
            //transform.position += transform.forward * zoom;
            transform.Rotate(0f, angleX * Time.deltaTime, 0f);
            transform.Rotate(angleY * Time.deltaTime*-1, 0f, 0f);
        }  
    }

    public void SetPlayerMove(float angle)
    {
        playerMoveAngle = angle;
    }
}
