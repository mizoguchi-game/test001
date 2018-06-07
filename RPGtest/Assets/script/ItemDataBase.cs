using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour {

    public ItemData[] itemLists = new ItemData[2];

    private void Awake()
    {
        //アイテムの全情報を作成
        itemLists[0] = new ItemData(Resources.Load("Sword", typeof(Sprite)) as Sprite,"剣",MyItemStatus.Item.Sword,"ただの剣");
        itemLists[1] = new ItemData(Resources.Load("Sword", typeof(Sprite)) as Sprite,"杖",MyItemStatus.Item.Staff,"ただの杖");
    }

    public ItemData[] GetItemDatas()
    {
        return itemLists;
    }

    public int GetItemTotal()
    {
        return itemLists.Length;
    }
}
