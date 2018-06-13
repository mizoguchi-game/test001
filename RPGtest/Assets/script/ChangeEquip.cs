using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEquip : MonoBehaviour {

    [SerializeField]
    private GameObject[] weapons;
    //private int equipment;
    private ProceesMyAttak proceesMyAttack;
    private PlayerMove playerMove;
    //characterのステータススクリプト
    private MyStatus myStatus;
    //武器の親のTransform
    [SerializeField]
    private Transform equip;
    //ゲーム画面の装備スロットの親
    [SerializeField]
    private Transform equipPanel;
    //インスタンス化した武器の参照
    private GameObject weapon;
    //現在装備している武器のスロット番号
    private int equipSlotNum = -1;

    private void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        proceesMyAttack = transform.root.GetComponent<ProceesMyAttak>();
        myStatus = GetComponent<MyStatus>();
    }

    private void Update()
    {

        if (Time.timeScale == 0)
        {
            return;
        }
        if (playerMove.GetState() == PlayerMove.MyState.Normal)
        {
            if (Input.GetKeyDown("1"))
            {
                InstantiateWepon(0);
            }
            else if (Input.GetKeyDown("2"))
            {
                InstantiateWepon(1);
            }
            else if (Input.GetKeyDown("3"))
            {
                InstantiateWepon(2);
            }
            else if (Input.GetKeyDown("4"))
            {
                InstantiateWepon(3);
            }
            
        }
    }

    //ヒエラルキー内に
    /*
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
    */
    public void InstantiateWepon(int slotNum)
    {
        equipSlotNum = slotNum;

        //今装備している武器を削除
        if (equip.childCount != 0)
        {
            DestroyImmediate(weapon);
            //Destroy(equip.GetChild(0).gameObject);
            //Debug.Log("削除後子数：" + equip.childCount);
            //Debug.Log("削除後子名：" + equip.GetChild(0));
        }
        for (int i = 0; i < equipPanel.childCount; i++)
        {
            if (i == slotNum)
            {
                equipPanel.GetChild(i).GetComponent<Image>().color = new Color(1f, 0f, 0f, 0.5f);
            }
            else
            {
                equipPanel.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        //MyStatusスクリプトからItemDataを取得
        var equipSlotData = myStatus.GetEquipSlotData(slotNum);
        //装備スロットに装備が設定されていなければ以降処理をしない
        if (equipSlotData == null)
        {
            return;
        }

        weapon = null;

        //プレハブの名前
        string prefabName = "";
        //装備スロットのItemDataの名前からプレハブの名前を設定
        if (equipSlotData.GetItemName() == "剣")
        {
            prefabName = "Sword";
        }
        else if (equipSlotData.GetItemName() == "杖")
        {
            prefabName = "Staff";
        }
        else if (equipSlotData.GetItemName() == "銃")
        {
            prefabName = "Frontloader Standard";
        }
        foreach (var item in weapons)
        {
            if (item.name == prefabName)
            {
                //新しく装備する武器をインスタンス化
                weapon = GameObject.Instantiate(item) as GameObject;
                proceesMyAttack.SetCollider(weapon.GetComponent<Collider>());
                break;
            }
        }
        //指定した武器が見つからなければ以降処理しない
        if (weapon == null)
        {
            return;
        }
        var weaponStatus = weapon.GetComponent<WeapomStatus>();

        weapon.transform.SetParent(equip);
        weapon.transform.localPosition = weaponStatus.GetPos();
        weapon.transform.localEulerAngles = weaponStatus.GetRot();
        weapon.transform.localScale = weaponStatus.GetScale();

        myStatus.SetEquip(weapon);

        if (weaponStatus.GetWeaponType() == ItemDataBase.Item.Gun)
        {
            GetComponent<Shot>().SetComponent();
        }
    }

    public int GetEquipSlotNum()
    {
        return equipSlotNum;
    }
        /*
        if (equip.childCount == 0) {
            equipment++;
            if (equipment >= weapons.Length)
            {
                equipment = 0;
            }
            
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
        */
    
}