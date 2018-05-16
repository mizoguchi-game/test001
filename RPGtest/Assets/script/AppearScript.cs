using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearScript : MonoBehaviour {
    
    //出現させる敵の数を入れておく
    [SerializeField] GameObject[] enemys;
    //次に敵が出現するまでの時間
    [SerializeField] float appearNextTime;
    //この場所から出現する敵の数
    [SerializeField] int maxNumOfEnemys;
    //今何体出現させたか
    private int numberOfEnemys;
    //待ち時間計測
    private float elapsedTime;

    private void Start()
    {
        numberOfEnemys = 0;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (numberOfEnemys >= maxNumOfEnemys)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > appearNextTime)
        {
            elapsedTime = 0f;
            AppearEnemy();
        }
    }

    void AppearEnemy()
    {
        var randomValue = Random.Range(0, enemys.Length);
        var randomRotationY = Random.value * 360f;

        GameObject.Instantiate(enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));
        numberOfEnemys++;
        elapsedTime = 0f;
    }


}
