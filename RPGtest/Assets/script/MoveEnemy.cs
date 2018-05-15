using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private CharacterController enemyController;
    private Vector3 destination;
    //歩行速度
    private float walkspeed = 1.0f;
    private Vector3 velocity;
    private Vector3 direction;
    private bool arrived;
    private Vector3 startPossition;

    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        //ランダムで円内に点をおきその地点を取得
        var randDestination = Random.insideUnitCircle * 8;
        //移動先をランダム値から生成
        destination = startPossition + new Vector3(randDestination.x,0,randDestination.y);
        //移動速度を0クリア
        velocity = Vector3.zero;
        //到着フラグをfalseで初期化
        arrived = false;
        //現在位置をオブジェクトの位置から取得
        startPossition = transform.position;
    }

    void Update()
    {
        if (!arrived)
        {
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                direction = (destination - transform.position).normalized;
                transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                velocity = direction * walkspeed;
                Debug.Log(destination);
            }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            enemyController.Move(velocity * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position,destination) < 0.5f)
        {
            arrived = true;
        }
    }
}

