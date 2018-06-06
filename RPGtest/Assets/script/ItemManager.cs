using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[SerializeField]
public class ItemDictionary : SerializableDictionaryBase<ItemManager.Item, int> { }

public class ItemManager : MonoBehaviour {

    public enum Item
    {
        Sword,
        Gun,
        Staff
    }

    [SerializeField]
    public ItemDictionary itemDictionary;

    public IDictionary<Item,int> ItemDictionary
    {
        get { return itemDictionary; }
        set { itemDictionary.CopyFrom(value); }
    }

	// Use this for initialization
	void Start () {
        ItemDictionary[Item.Sword] = 0;
        ItemDictionary[Item.Staff] = 1;

        foreach(var item in ItemDictionary)
        {
            Debug.Log(item.Key + ":" + GetNum(item.Key));
        }
		
	}

    //アイテムをどれだけ保持しているのかの数を返す
    public int GetNum(Item key)
    {
        return itemDictionary[key];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
