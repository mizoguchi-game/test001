using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchCharacter : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //Enamyキャラの状態を取得
            Enemy.EnemyState state = GetComponentInParent<Enemy>().GetState();
            //Enemy キャラが追いかけている状態でなければ追いかける設定に変更
            if (state != Enemy.EnemyState.Chase &&
                state != Enemy.EnemyState.Attack &&
                state != Enemy.EnemyState.Freeze &&
                state != Enemy.EnemyState.Dead &&
                state != Enemy.EnemyState.Damage &&
                !Physics.Linecast(transform.position + Vector3.up,other.gameObject.transform.position + Vector3.up,LayerMask.GetMask("Field")))
            {
                //Debug.Log("プレイヤー発見");
                GetComponentInParent<Enemy>().SetState("chase", other.transform);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("見失う");
            GetComponentInParent<Enemy>().SetState("wait");
        }
    }
}
