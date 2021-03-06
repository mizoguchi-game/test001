﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData{

    //アイテムのImage画像
    private Sprite itemSprite;
    //アイテム名
    private string itemName;
    //アイテムのタイプ
    private ItemDataBase.Item itemType;
    //アイテムの情報
    private string itemInformation;

    public ItemData(Sprite image,string itemName,ItemDataBase.Item itemType,string information)
    {
        this.itemSprite = image;
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemInformation = information;
    }

    public Sprite GetItemSprite()
    {
        return itemSprite;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public ItemDataBase.Item GetItemType()
    {
        return itemType;
    }

    public string GetItemInformation()
    {
        return itemInformation;
    }
}
