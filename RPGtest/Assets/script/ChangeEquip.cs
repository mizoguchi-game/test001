using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquip : MonoBehaviour {

    [SerializeField]
    private GameObject[] weapons;
    private int equipment;
    private ProceesMyAttak proceesMyAttack;
    private PlayerMove playerMove;

    private void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        proceesMyAttack = transform.root.GetComponent<ProceesMyAttak>();

        //初期装備指定
        equipment = -1;
        weapons[equipment].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown("1") && playerMove.GetState() == PlayerMove.MyState.Normal)
        {
            InstantiateWepon();
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
        if (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        //新しく装備する武装をインスタンス化
        var weapon = GameObject.Instantiate(weapons[equipment]) as GameObject;
        proceesMyAttack.SetCollider(weapon.GetComponent<Collider>());
    }
}