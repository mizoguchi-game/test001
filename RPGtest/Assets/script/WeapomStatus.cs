using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapomStatus : MonoBehaviour {

    [SerializeField]
    private int attackPower;
    [SerializeField]
    private int ShotPower;
    [SerializeField]
    private string weaponType;
    [SerializeField]
    private float weaponRange;

    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetShotPower()
    {
        return ShotPower;
    }

    public string GetWeaponType()
    {
        return weaponType;
    }

    public float GetWeaponRange()
    {
        return weaponRange;
    }
}
