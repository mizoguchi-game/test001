using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyStatus : MonoBehaviour {

    [SerializeField]
    private int hp;

    [SerializeField]
    private int power;
    private WeapomStatus weapomStatus;

    [SerializeField]
    private int numOfBulletsForGun;

    private GameObject equip;

    //アイテムを持っているかどうかのフラグ
    [SerializeField]
    private bool[] ItemFlags = new bool[6];

    //装備スロットに装備している武器
    [SerializeField]
    private ItemData[] equipSlotDatas = new ItemData[4];

    //ゲーム画面に装備スロットの親パネル
    [SerializeField]
    private Transform equipPanel;

    //アイテムスロットにアイテムデータをセット
    public void SetItemData(ItemData itemData, int SlotNum)
    {
        equipSlotDatas[SlotNum] = itemData;
        equipPanel.GetChild(SlotNum).GetChild(0).GetComponent<Image>().sprite = itemData.GetItemSprite();
    }

    //装備スロットの武器情報取得メソッド
    public ItemData GetEquipSlotData(int num)
    {
        return equipSlotDatas[num];
    }

    //アイテムを保持しているかどうか
    public bool GetItemFlag(ItemDataBase.Item item)
    {
        return ItemFlags[(int)item];
    }

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
