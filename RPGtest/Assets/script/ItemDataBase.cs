using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour {

    //private ItemData[] itemLists = new ItemData[2];
    private List<ItemData> itemLists = new List<ItemData>();


    private void Awake()
    {
        Add("Sword", "剣", MyItemStatus.Item.Sword, "ただの剣");
        Add("Sword", "杖", MyItemStatus.Item.Staff, "ただの杖");
        /*
        //アイテムの全情報を作成
        itemLists[0] = new ItemData(Resources.Load("Sword", typeof(Sprite)) as Sprite,"剣",MyItemStatus.Item.Sword,"ただの剣");
        itemLists[1] = new ItemData(Resources.Load("Sword", typeof(Sprite)) as Sprite,"杖",MyItemStatus.Item.Staff,"ただの杖");
        */
    }

    private void Add(string itemSprite, string itemName, MyItemStatus.Item itemType, string information)
    {
        itemLists.Add(new ItemData(Resources.Load<Sprite>(itemSprite), itemName, itemType, information));
    }

    public List<ItemData> GetItemData()
    {
        return itemLists;
    }

    public int GetItemTotal()
    {
        return itemLists.Count;
    }
}
