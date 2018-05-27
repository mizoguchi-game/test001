using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquip : MonoBehaviour {

    [SerializeField]
    private GameObject[] weapons;
    private int equipment;
    private ProceesMyAttak proceesMyAttack;
    private PlayerMove playerMove;

    [SerializeField]
    private Transform equip;
    private MyStatus myStatus;

    private void Start()
    {
        myStatus = GetComponent<MyStatus>();

        playerMove = GetComponentInParent<PlayerMove>();
        proceesMyAttack = transform.root.GetComponent<ProceesMyAttak>();

        //初期装備指定
        equipment = -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1") && playerMove.GetState() == PlayerMove.MyState.Normal)
        {
            InstantiateWepon();
            Debug.Log("切り替え呼ばれた");
        }
    }

    //ヒエラルキー内に
    private void Change()
    {
        equipment++;
        if (equipment >= weapons.Length)
        {
            equipment = 0;
        }

        for (var i = 0;i < weapons.Length; i++)
        {
            if(i == equipment)
            {
                weapons[i].SetActive(true);
                proceesMyAttack.SetCollider(weapons[i].GetComponent<Collider>());
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    void InstantiateWepon()
    {
        equipment++;
        if (equipment >= weapons.Length)
        {
            equipment = 0;
        }

        //今装備している武器を削除
        if (equip.childCount != 0)
        {
            Destroy(equip.GetChild(0).gameObject);
        }

        //新しく装備する武装をインスタンス化
        var weapon = GameObject.Instantiate(weapons[equipment]) as GameObject;
        proceesMyAttack.SetCollider(weapon.GetComponent<Collider>());

        if (equipment == 0)
        {
            weapon.transform.SetParent(equip);
            weapon.transform.localPosition = new Vector3(0f,0f,0f);
            weapon.transform.localEulerAngles = new Vector3(38.929f, -81.94601f, -84.91901f);
            weapon.transform.localScale = new Vector3(18.64241f, 30.14519f, 8.053901f);
        }
        else if (equipment == 1)
        {
            weapon.transform.SetParent(equip);
            weapon.transform.localPosition = new Vector3(0f, 0f, 0f);
            weapon.transform.localEulerAngles = new Vector3(44.816f, -88.95901f, -88.523f);
            weapon.transform.localScale = new Vector3(24.68945f, 24.68945f, 24.68945f);
        }
        else if (equipment == 2)
        {
            weapon.transform.SetParent(equip);
            weapon.transform.localPosition = new Vector3(0f, 0f, 0f);
            weapon.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            weapon.transform.localScale = new Vector3(0.2847402f, 0.2847401f, 0.2847401f);
        }

        myStatus.SetEquip(weapon);
    }
}