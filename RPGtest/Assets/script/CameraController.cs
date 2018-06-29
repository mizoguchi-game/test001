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
    private float playerAngleY;//プレイヤーの現在の角度
    private float Angular_diff;//プレイヤーの回転角度差分
    private float minAngleXLimit = -80;
    private float maxAngleXLimit = 80;
    private float minAngleYLimit = -80;
    private float maxAngleYLimit = 80;

    // Use this for initialization
    void Start () {
        targetPos = player.transform.position;
        inFpsPos = transform.position;
        playerAngleY = player.transform.root.gameObject.transform.eulerAngles.y;
    }
	
	// Update is called once pedr frame
	void FixedUpdate() {
        RotateCameraAngele();
    }

    private void RotateCameraAngele()
    {
        

        float zoom = Input.GetAxis("Mouse ScrollWheel");
        float angleX = Input.GetAxis("Mouse X");
        float angleY = Input.GetAxis("Mouse Y");
        float cameraXAngle = 0f;
        float cameraYAngle = 0f;

        Angular_diff = (playerAngleY - player.transform.root.eulerAngles.y) * -1;
        playerAngleY = player.transform.root.gameObject.transform.eulerAngles.y;

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
            float rotateYminLimit = (playerAngleY + minAngleYLimit < 0) ? playerAngleY + minAngleYLimit + 360: playerAngleY + minAngleYLimit;
            float rotateYmaxLimit = (playerAngleY + minAngleYLimit > 360) ? playerAngleY + maxAngleYLimit - 360 : playerAngleY + maxAngleYLimit;
            Debug.Log("rotateYminLimit:" + rotateYminLimit);
            Debug.Log("rotateYmaxLimit:" + rotateYmaxLimit);

            // 現在の回転角度を0～360から-180～180に変換
            float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
            float rotateY = transform.eulerAngles.y;

            cameraXAngle = Mathf.Clamp(rotateX + angleY * fpsRotetaspeed * -1, minAngleYLimit, maxAngleYLimit);
            if (rotateYminLimit > rotateYmaxLimit)
            {
                Debug.Log("min大きい");
                cameraYAngle = Mathf.Clamp(rotateY + angleX * fpsRotetaspeed + Angular_diff, rotateYmaxLimit, rotateYminLimit);
            }
            else
            {
                Debug.Log("max大きい");
                cameraYAngle = Mathf.Clamp(rotateY + angleX * fpsRotetaspeed + Angular_diff, rotateYminLimit, rotateYmaxLimit);
            }
            Debug.Log("cameraYAngle:" + cameraYAngle);

            // 回転角度を-180～180から0～360に変換
            cameraXAngle = (cameraXAngle < 0) ? cameraXAngle + 360 : cameraXAngle;

            // 回転角度をオブジェクトに適用
            transform.rotation = Quaternion.Euler(/*cameraXAngle*/0,cameraYAngle, 0);            
        }
    }
}
