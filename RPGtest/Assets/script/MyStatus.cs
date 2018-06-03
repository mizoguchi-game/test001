using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStatus : MonoBehaviour {

    [SerializeField]
    private int hp;

    [SerializeField]
    private int power;
    private WeapomStatus weapomStatus;

    [SerializeField]
    private int numOfBulletsForGun;

    private GameObject equip;

    public void SetEquip(GameObject weapon)
    {
        equip = weapon;
        weapomStatus = equip.GetComponent<WeapomStatus>();
    }

    public WeapomStatus GetWeapomStatus()
    {
        return weapomStatus;
    }

    public int GetAttackPower()
    {
        return power + weapomStatus.GetAttackPower();
    }

    void SetHp (int hp)
    {
        this.hp = hp;
    }

    public int GetHp()
    {
        return hp;
    }

    public GameObject GetEquip()
    {
        return equip;
    }

    public int GetShotPower()
    {
        return GetWeapomStatus().GetShotPower();
    }

    //弾の数を設定
    public int SetNumberOfBullet(int num)
    {
        numOfBulletsForGun = num;
        return num;
    }

    //弾の数を取得
    public int GetNumberOfBullet()
    {
        return numOfBulletsForGun;
    }
}
