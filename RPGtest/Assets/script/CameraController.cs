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

    // Use this for initialization
    void Start () {
        targetPos = player.transform.position;
        inFpsPos = transform.position;
    }
	
	// Update is called once pedr frame
	void Update () {
        RotateCameraAngele();
    }

    private void RotateCameraAngele()
    {

        float angleX = Input.GetAxis("Mouse X") * rotetaspeed;
        float angleY = Input.GetAxis("Mouse Y") * rotetaspeed;
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        transform.position += player.transform.position - targetPos;
        targetPos = player.transform.position;

        if (Vector3.Distance(targetPos, transform.position + transform.forward * zoom) < 1f && fpsMode == false)
        {
            fpsMode = true;
            inFpsPos = transform.position;
            transform.position = targetPos;
        }
        else if (Vector3.Distance(targetPos, transform.position + transform.forward * zoom) >= 1f && fpsMode == true)
        {
            fpsMode = false;
            transform.position = inFpsPos;
        }

        Debug.Log(Vector3.Distance(targetPos, transform.position + transform.forward * zoom));

        if (fpsMode == false) 
        {
            transform.position += transform.forward * zoom;
            transform.LookAt(player.transform.position + new Vector3(0f, Vector3.Distance(targetPos, transform.position) / 3, 0f));
            transform.RotateAround(player.transform.position, Vector3.up, angleX * Time.deltaTime * rotetaspeed);
            transform.RotateAround(player.transform.position, transform.right, angleY * Time.deltaTime * rotetaspeed * -1);
        }
        else
        {
            transform.position += transform.forward * zoom;
            transform.Rotate(angleY * Time.deltaTime * rotetaspeed * -1, angleX * Time.deltaTime * rotetaspeed, 0f);
        }

        
        
    }
}
