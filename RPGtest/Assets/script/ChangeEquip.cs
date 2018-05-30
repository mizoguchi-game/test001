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
        Debug.Log("押された際の子数：" + equip.childCount);
        equipment++;
        if (equipment >= weapons.Length)
        {
            equipment = 0;
        }

        //今装備している武器を削除
        if (equip.childCount != 0)
        {
            Destroy(equip.GetChild(0).gameObject);
            Debug.Log("削除後子数：" + equip.childCount);
            Debug.Log("削除後子名：" + equip.GetChild(0));
        }

        if (weapons.Length-1 !=  equipment) { 
            //新しく装備する武装をインスタンス化
            var weapon = GameObject.Instantiate(weapons[equipment]) as GameObject;
            proceesMyAttack.SetCollider(weapon.GetComponent<Collider>());

            var weaponStateus = weapon.GetComponent<WeapomStatus>();

            weapon.transform.SetParent(equip);
            weapon.transform.localPosition = weaponStateus.GetPos();
            weapon.transform.localEulerAngles = weaponStateus.GetRot();
            weapon.transform.localScale = weaponStateus.GetScale();

            myStatus.SetEquip(weapon);
            GetComponent<Shot>().SetComponent();
        }
    }
}