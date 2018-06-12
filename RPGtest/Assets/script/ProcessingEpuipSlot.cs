using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingEpuipSlot : MonoBehaviour {
    
    private ItemData myItemData;
    [SerializeField]
    private Text informationText;
    [SerializeField]
    private MyStatus myStatus;

    //装備スロットの番号
    [SerializeField]
    private int slotNum;

    //装備切り替え処理スクリプト
    [SerializeField]
    private ChangeEquip changeEquip;

    public void MouseOver()
    {
        if(myItemData != null)
        {
            ShowInformation();
        }
    }

    public void MouseExit()
    {
        if (myItemData != null)
        {
            informationText.text = "";
        }
    }

    //スロットの上にアイテムがドロップされた際に実行
    public void MouseDrop()
    {
        if (FindObjectOfType<DragItemData>() == null)
        {
            return;
        }
        //DragItemUIに設定されているDragItemDataスクリプトからアイテムを取得
        var dragitemdata = FindObjectOfType<DragItemData>();
        myItemData = dragitemdata.GetItem();
        //ドラッグしているアイテムの削除
        dragitemdata.DeleteDragItem();
        //MyStatusスクリプトの装備スロットにアイテム情報を設定
        myStatus.SetItemData(myItemData, slotNum);

        if (changeEquip.GetEquipSlotNum() == slotNum)
        {
            changeEquip.InstantiateWepon(slotNum);
        }

        transform.GetChild(0).GetComponent<Image>().color = Color.white;

        ShowInformation();
    }

    private void ShowInformation()
    {
        //アイテムイメージを設定
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();
        //スロットのTextを取得し名前を設定
        Text nameUI = GetComponentInChildren<Text>();
        nameUI.text = myItemData.GetItemName();
        //アイテム情報を表示する
        var text = "<Color=white>" + myItemData.GetItemName() + "</Color>\n";
        text += myItemData.GetItemInformation();
        informationText.text = text;
    }
}
