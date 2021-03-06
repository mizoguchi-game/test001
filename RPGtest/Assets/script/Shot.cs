﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {

    //レイの長さ
    private float rayRange;
    [SerializeField]
    private Transform equip;

    private AudioSource audioSource;

    //レーザーポインター
    private LineRenderer raserPointer;
    //発砲の光
    private Light shotLigth;
    //発砲の光の持続時間
    [SerializeField]
    private float lightLimit;
    //発砲の光が出てからの経過時間
    private float ligthElapsedTime;
    //主人公の行動処理script
    private PlayerMove move;
    //敵にレーザーポインタが当たった時のポイント
    [SerializeField]
    public MeshRenderer shotPoint;
    //主人公のステータススクリプト
    private MyStatus myStatus;
    //レーザーポインタが何かに当たっているかどうか
    private bool hitFlag = false;
    //レイを飛ばした先で一番近い位置
    private Vector3 nearPoint;
    //レイがヒットした一番近い位置と銃口の距離
    private float distance = float.MaxValue;
    //銃の発射工
    [SerializeField]
    private Transform muzzle;
    //弾の数を表示しているUI
    [SerializeField]
    private Text bulletText;
    //発射音
    [SerializeField]
    private AudioClip ShotSE;
    //弾が無い時の音
    [SerializeField]
    private AudioClip noBulletSE;
    [SerializeField] private GameObject bulletHolePrehub;//弾痕のプレハブ

	// Use this for initialization
	void Start () {
        myStatus = GetComponent<MyStatus>();
        move = GetComponent<PlayerMove>();
        rayRange = 1000f;
        audioSource = gameObject.GetComponent<AudioSource>();
        Debug.Log(equip.childCount);
        bulletText.text = myStatus.GetNumberOfBullet().ToString();


    }
	
    public void SetAbleRaserPointer()
    {
        raserPointer.enabled = true;
    }

    public void SetDisableRaserPointer()
    {
        raserPointer.enabled = false;
    }

    public void SetAbleLight()
    {
        shotLigth.enabled = true;
        ligthElapsedTime = 0f;
    }

    public void SetDisableLight()
    {
        shotLigth.enabled = false;
    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetComponent(Transform muzzleTransform)
    {
        raserPointer = equip.GetComponentInChildren<LineRenderer>();
        shotLigth = equip.GetComponentInChildren<Light>();
        muzzle = muzzleTransform.Find("Muzzle");
        /*muzzle = equip.GetChild(0).Find("Muzzle");*/
        /*
        Debug.Log("equip:"+ equip);
        Debug.Log("raserPointer:" + raserPointer);
        Debug.Log("muzzle:"+muzzle);
        */
    }
    public void SetComponent()
    {
        raserPointer = equip.GetComponentInChildren<LineRenderer>();
        shotLigth = equip.GetComponentInChildren<Light>();
        muzzle = equip.GetChild(0).Find("Muzzle");
        /*
        var tmp1 = equip.GetChild(0);
        var tmp2 = equip.childCount;
        Debug.Log("equip:" + equip);
        Debug.Log("raserPointer:" + raserPointer);
        Debug.Log("muzzle:" + muzzle);
        Debug.Log("tmp1子名:" + tmp1);
        Debug.Log("tmp2子の数:" + tmp2);
        */

    }

    private void Update()
    {
        if (move.GetState() == PlayerMove.MyState.WaitShot)
        {
            ligthElapsedTime += Time.deltaTime;

            raserPointer.SetPosition(0, muzzle.position);

            Ray ray = new Ray(muzzle.position, muzzle.forward);

            RaycastHit hit;
            hitFlag = false;
            distance = float.MaxValue;

            //Fieldレイヤーとの接触
            if (Physics.Raycast(ray,out hit,rayRange,LayerMask.GetMask("Filed")))
            {
                shotPoint.enabled = false;
                hitFlag = true;
                nearPoint = hit.point;
                distance = Vector3.Distance(muzzle.position, hit.point);
            }
            //Enemyレイヤーとの接触
            if (Physics.Raycast(ray,out hit,myStatus.GetWeapomStatus().GetWeaponRange(),LayerMask.GetMask("EnemyHit")))
            {
                if (hit.collider.transform.root.GetComponent<Enemy>().GetState() != Enemy.EnemyState.Dead)
                {
                    shotPoint.enabled = true;
                    hitFlag = true;

                    //Fieldレイヤーとの接触よりEnemyレイヤーとの接触が近い場合
                    if (Vector3.Distance(muzzle.position,hit.point) < distance)
                    {
                        nearPoint = hit.point;
                        shotPoint.transform.position = hit.point;
                        //Fieldレイヤーのほうが近い場合shotpointを無効か
                    }
                    else
                    {
                        shotPoint.enabled = false;
                    }
                }
            }
            if (Physics.Raycast(ray,out hit,myStatus.GetWeapomStatus().GetWeaponRange(),LayerMask.GetMask("Block")))
            {
                shotPoint.enabled = true;
                hitFlag = true;

                //Fieldレイヤーとの接触よりEnemyレイヤーとの接触が近い場合
                if (Vector3.Distance(muzzle.position, hit.point) < distance)
                {
                    nearPoint = hit.point;
                    shotPoint.transform.position = hit.point;
                    //Fieldレイヤーのほうが近い場合shotpointを無効か
                }
                else
                {
                    shotPoint.enabled = false;
                }
            }
            //何らかに接触していたら接触した一番近い位置をレザーポイントの
            if (hitFlag)
            {
                raserPointer.SetPosition(1, nearPoint);
                //接触していなければShotPointを無効化し、レーザーポインタはMUzzleからRayRangeの長さ分いったところを到達点にする
            }
            else
            {
                shotPoint.enabled = false;
                raserPointer.SetPosition(1, ray.origin + ray.direction * rayRange);
            }

            if (ligthElapsedTime >= lightLimit)
            {
                SetDisableLight();
            }
            //銃を構えた状態でなければレーザーポインタ、銃の光、shotPointを無効化
        }
        else
        {
            if(raserPointer != null)
            {
                raserPointer.enabled = false;
                shotLigth.enabled = false;
            }
            shotPoint.enabled = false;
        }
    }

    public void JudgeShot()
    {
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hitPointField;
        RaycastHit hitPointEnemy;
        RaycastHit hitPointBlock;

        distance = rayRange;

        //弾がなければ弾がない時の効果音を鳴らしreturnする
        if (myStatus.GetNumberOfBullet() <= 0)
        {
            PlaySE(noBulletSE);
            return;
        }

        SetAbleLight();
        PlaySE(ShotSE);
        bulletText.text = myStatus.SetNumberOfBullet(myStatus.GetNumberOfBullet() - 1).ToString();
        //フィールド
        if (Physics.Raycast(ray, out hitPointField, myStatus.GetWeapomStatus().GetWeaponRange(),LayerMask.GetMask("Field")))
        {
            distance = Vector3.Distance(muzzle.position, hitPointField.point);
        }
        //エネミー
        if(Physics.Raycast(ray,out hitPointEnemy,myStatus.GetWeapomStatus().GetWeaponRange(),LayerMask.GetMask("EnemyHit")))
        {
            if (distance > Vector3.Distance(muzzle.position,hitPointEnemy.point))
            {
                var enemyObj = hitPointEnemy.collider.gameObject.transform.root;

                if (enemyObj.GetComponent<Enemy>().GetState() != Enemy.EnemyState.Dead)
                {
                    var enemt = enemyObj.GetComponent<Enemy>();
                    enemt.TakeDamage(myStatus.GetShotPower(),hitPointEnemy.collider.transform,hitPointEnemy.point);
                }
            }
        }
        if (Physics.Raycast(ray,out hitPointBlock,myStatus.GetWeapomStatus().GetWeaponRange(),LayerMask.GetMask("Block")))
        {
            distance = Vector3.Distance(muzzle.position,hitPointBlock.point);
            hitPointBlock.rigidbody.AddForce(muzzle.forward * 500f);
        }
        if (hitPointEnemy.transform != null)
        {
            var bulletHoleInstance = Instantiate(bulletHolePrehub,hitPointEnemy.point - muzzle.forward * 0.001f,Quaternion.FromToRotation(Vector3.up,hitPointEnemy.normal)) as GameObject;
            bulletHoleInstance.transform.SetParent(hitPointEnemy.collider.transform);
            Destroy(bulletHoleInstance, 30.0f);
        }
        if(hitPointField.transform != null)
        {
            var bulletHoleInstance = Instantiate(bulletHolePrehub, hitPointField.point - muzzle.forward * 0.001f, Quaternion.FromToRotation(Vector3.up, hitPointField.normal)) as GameObject;
            bulletHoleInstance.transform.SetParent(hitPointField.collider.transform);
            Destroy(bulletHoleInstance, 30.0f);
        }
    }

}
