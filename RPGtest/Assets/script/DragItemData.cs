using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragItemData : MonoBehaviour {

    //ドラックしているアイテム
    private ItemData dragItemData;

    //ドラッグするアイテムデータをセット
	public void SetDragItem(ItemData dragItemData)
    {
        this.dragItemData = dragItemData;
        transform.GetComponent<Image>().sprite = dragItemData.GetItemSprite();
    }
	
    //ドラッグしているアイテムデータの取得
    public ItemData GetItem()
    {
        return dragItemData;
    }

    //ドラッグしているアイテムの削除
    public void DeleteDragItem()
    {
        dragItemData = null;
    }

    //ドラックアイテムが設定されている場合はマウスを追従
	void Update () {
        Debug.Log("dragItemData:"+dragItemData.GetItemName());
        if (dragItemData != null)
        {
            Debug.Log("ドラッグ移動中");
            transform.position = Input.mousePosition;
        }
	}
}
