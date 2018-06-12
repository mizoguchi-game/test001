using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateSlotScript : MonoBehaviour {

    //アイテム情報のスロットプレハブ
    [SerializeField]
    private GameObject slot;
    //主人公のステータス
    [SerializeField]
    private MyStatus myItemStatus;
    //アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;

    //アクティブになった時
    private void OnEnable()
    {
        //アイテムデータベースに登録されているアイテム用のスロットを全作成
        CreateSlot(itemDataBase.GetItemData());
    }

    public void CreateSlot(List<ItemData> itemLists) {
        int i = 0;
        foreach (ItemData item in itemLists)
        {
            if (myItemStatus.GetItemFlag(item.GetItemType()))
            {
                //スロットのインスタンス化
                var instanceSlot = Instantiate(slot) as GameObject;
                //スロットゲームオブジェクトの名前を設定
                instanceSlot.name = "ItemSlot" + i++;
                //親をMainにする
                instanceSlot.transform.SetParent(transform);
                //Scaleを設定しないと0になるので設定
                instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);
                //アイテムの情報をスロットのProcessingSlotに設定する
                instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
                Debug.Log(item.GetItemName());
            }
        }
    }
}
