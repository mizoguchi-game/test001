using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPStatusUI : MonoBehaviour {

    //敵のステータス
    private EnemyStatus enemyStatus;
    //HP表示用スライダー
    private Slider hpSlider;

	// Use this for initialization
	void Start () {
        //自身のルートに取り付けているステータス取得
        enemyStatus = transform.root.GetComponent<EnemyStatus>();
        //HP用Sliderを子要素から取得
        hpSlider = transform.Find("HPBar").GetComponent<Slider>();
        //スライダーの値0～1の間になるように比率を計算
        hpSlider.value = (float)enemyStatus.GetMaxHp() / (float)enemyStatus.GetMaxHp();
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Camera.main.transform.rotation;
	}

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }

    public void UpdateHPValue()
    {
        hpSlider.value = (float)enemyStatus.GetHp() / (float)enemyStatus.GetMaxHp();
    }
}
