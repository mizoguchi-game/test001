using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float tpsRotetaspeed = 5f;
    [SerializeField] private float fpsRotetaspeed = 0.1f;
    private Vector3 targetPos = new Vector3();
    private Vector3 inFpsPos = new Vector3();
    private bool fpsMode = false;
    private Vector3 tpsPos = new Vector3();
    private float playerMoveAngle = 0f;
    private float playerAngleY;
    private float Angular_diff;
    private float minAngleXLimit = -80;
    private float maxAngleXLimit = 80;
    private float minAngleYLimit = -80;
    private float maxAngleYLimit = 80;

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
        float angleX = Input.GetAxis("Mouse X");
        float angleY = Input.GetAxis("Mouse Y");

        transform.position += player.transform.position - targetPos;
        targetPos = player.transform.position;

        if (Vector3.Distance(targetPos, transform.position + transform.forward * zoom) < 1f && fpsMode == false)
        {
            fpsMode = true;
            inFpsPos = transform.position;
            transform.position = targetPos;
            transform.LookAt(player.transform.root.position + player.transform.root.forward * 10f);
        }
        else if (zoom < 0 && fpsMode == true)
        {
            fpsMode = false;
            transform.position = transform.position + transform.forward * -1.1f;
        }

        if (fpsMode == false) 
        {
            transform.position += transform.forward * zoom;//ズーム設定
            transform.LookAt(player.transform.position + new Vector3(0f, Vector3.Distance(targetPos, transform.position) / 3, 0f));//キャラクターの方向にカメラを向ける
            transform.RotateAround(player.transform.position, Vector3.up, angleX * tpsRotetaspeed * Time.deltaTime + Angular_diff);
            transform.RotateAround(player.transform.position, transform.right, angleY * tpsRotetaspeed * Time.deltaTime * -1);
        }
        else
        {
            /*上下角-+180度 左右角80度*/


            //transform.Rotate(Vector3.up,angleX * Time.deltaTime + Angular_diff);
            //transform.Rotate(new Vector3(1f,0f,0f),angleY * Time.deltaTime*-1);
            //transform.Rotate(new Vector3(0, 0, 1),transform.eulerAngles.z * -1);

            
            // 現在の回転角度を0～360から-180～180に変換
            float rotateY = (transform.eulerAngles.y > 180) ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;
            float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
            Debug.Log("rotateY:"+rotateY);

            // 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする
            float Y = Mathf.Clamp(rotateY + angleX * fpsRotetaspeed, minAngleXLimit, maxAngleXLimit);
            float X = Mathf.Clamp(rotateX + angleY * fpsRotetaspeed * -1, minAngleYLimit, maxAngleYLimit);
            Debug.Log("Y1:" + Y);
            // 回転角度を-180～180から0～360に変換
            Y = (Y < 0) ? Y + 360 : Y;
            X = (X < 0) ? X + 360 : X;
            Debug.Log("Y2:" + Y);
            // 回転角度をオブジェクトに適用
            transform.rotation = Quaternion.Euler(X, Y + Angular_diff, 0);
            

            /*
            // 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする
            float Y = Mathf.Clamp(transform.eulerAngles.y + angleX * fpsRotetaspeed, minAngleXLimit, maxAngleXLimit);
            //float X = Mathf.Clamp(transform.eulerAngles.x + angleY * fpsRotetaspeed * -1, minAngleYLimit + player.transform.root.eulerAngles.x, maxAngleYLimit + player.transform.root.eulerAngles.x);
            // 回転角度をオブジェクトに適用
            transform.rotation = Quaternion.Euler(0, Y + Angular_diff, 0);
            */
        }
    }
}
