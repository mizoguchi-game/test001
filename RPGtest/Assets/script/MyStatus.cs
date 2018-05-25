using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStatus : MonoBehaviour {
    
    [SerializeField]
    private int hp;

    [SerializeField]
    private int power;
    private WeapomStatus weapomStatus;

    private GameObject equip;

    public void SetEquip(GameObject weapon)
    {
        equip = weapon;
        weapomStatus = equip.GetComponent<WeapomStatus>();
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
}
