using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingSlot : MonoBehaviour {

    //アイテム情報を表示するテキストUI
    private Text informatioonText;
    //アイテムの名前を表示するテキストUIプレハブ
    [SerializeField]
    private GameObject titleUI;
    //アイテム名を表示するテキストUIをインスタンス化した物を入れておく
    private GameObject uiObj;
    //自身のアイテムデータを入れておく
    [SerializeField]
    public ItemData myItemData;
    //ドラッグするUIPrefab
    [SerializeField]
    private GameObject dragItemUI;
    //ドラッグしているUIインスタンス
    private GameObject instanceDragItemUI;

    //スロットが非アクティブになったら削除
    private void OnDisable()
    {
        if(uiObj != null)
        {
            Destroy(uiObj);
        }
        Destroy(gameObject);
    }

    //アイテムデータをセット
    public void SetItemData(ItemData itemData)
    {
        myItemData = itemData;
        transform.GetChild(0).GetComponent<Image>().sprite = myItemData.GetItemSprite();
    }

    // Use this for initialization
    void Start () {
        informatioonText = transform.parent.parent.Find("Information").GetChild(0).GetComponent<Text>();
		
	}

    public void MouseOver()
    {
        Debug.Log("MausOver呼ばれた");
        if (uiObj != null)
        {
            Destroy(uiObj);
        }
        //アイテムの名前を表示するUIの位置を調整してインスタンス化
        uiObj = Instantiate(titleUI, new Vector2(transform.position.x - 25f, transform.position.y + 25f), Quaternion.identity) as GameObject;
        //アイテム表示UIの親をPanelにする
        uiObj.transform.SetParent(transform.parent.parent);
        Text ui = uiObj.GetComponentInChildren<Text>();
        //アイテム表示テキストに自身のアイテムの名前を表示
        ui.text = myItemData.GetItemName();
        //情報表示テキストに自身のアイテムの情報を表示
        informatioonText.text = myItemData.GetItemInformation();
    }

    public void MouseExit()
    {
        //マウスポインタがアイテムスロットを出たらアイテム表示UIを削除
        if (uiObj != null)
        {
            informatioonText.text = "";
            Destroy(uiObj);
        }
    }

    public void MouseBeginDrag()
    {
        if (myItemData.GetItemType() == MyItemStatus.Item.Gun
            || myItemData.GetItemType() == MyItemStatus.Item.Staff
            || myItemData.GetItemType() == MyItemStatus.Item.Sword)
        {
            instanceDragItemUI = Instantiate(dragItemUI, Input.mousePosition, Quaternion.identity) as GameObject;//オブジェクト生成
            instanceDragItemUI.transform.SetParent(transform.parent.parent);//親を設定
            instanceDragItemUI.GetComponent<DragItemData>().SetDragItem(myItemData);
        }
    }

    public void MouseEndDrag()
    {
        Destroy(instanceDragItemUI);
    }
}
