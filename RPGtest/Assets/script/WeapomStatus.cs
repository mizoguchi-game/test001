using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapomStatus : MonoBehaviour {

    [SerializeField]
    private int attackPower;
    [SerializeField]
    private int ShotPower;
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private float weaponRange;
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private Vector3 rot;
    [SerializeField]
    private Vector3 scale;

    public enum WeaponType
    {
        Sword,
        Staff,
        Gun
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetShotPower()
    {
        return ShotPower;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public float GetWeaponRange()
    {
        return weaponRange;
    }

    public Vector3 GetPos()
    {
        return pos;
    }

    public Vector3 GetRot()
    {
        return rot;
    }

    public Vector3 GetScale()
    {
        return scale;
    }
}
