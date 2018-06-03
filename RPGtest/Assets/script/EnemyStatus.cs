using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

    [SerializeField]
    private int maxHp = 3;

    [SerializeField]
    private int hp;

    [SerializeField]
    private int attackPower = 1;
    private Enemy enemy;
    //敵のHPバー処理script
    private HPStatusUI hpStatusUI;
    
    // Use this for initialization
	void Start () {
        enemy = GetComponent<Enemy>();
        hp = maxHp;
        hpStatusUI = GetComponentInChildren<HPStatusUI>();
	}
	
	public void SetHp(int hp)
    {
        this.hp = hp;

        //HP表示用のUIアップデート
        hpStatusUI.UpdateHPValue();

        if(hp <= 0)
        {
            hpStatusUI.SetDisable();
            enemy.Dead();
        }
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }
}
