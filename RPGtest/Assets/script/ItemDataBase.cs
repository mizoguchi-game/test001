using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour {

    public enum Item
    {
        Sword,
        Gun,
        Staff
    }
    
    //private ItemData[] itemLists = new ItemData[2];
    private List<ItemData> itemLists = new List<ItemData>();


    private void Awake()
    {
        Add("Sword", "剣", Item.Sword, "ただの剣");
        Add("Sword", "杖", Item.Staff, "ただの杖");
        Add("Sword", "銃", Item.Gun, "ただの銃");
    }

    private void Add(string itemSprite, string itemName, Item itemType, string information)
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
