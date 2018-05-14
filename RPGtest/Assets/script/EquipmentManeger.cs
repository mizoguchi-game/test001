using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManeger : MonoBehaviour {

    public string WeaponPoint;

    private GameObject m_WeaponPoint;
    private GameObject m_Weapon = null;

    // Use this for initialization
    void Start () {
        var children = GetComponentsInChildren<Transform>(true);
        foreach (var transfom in children)
        {
            if (transfom.name == WeaponPoint)
            {
                m_WeaponPoint = transfom.gameObject;
            }
        }
	}
	
	public void EquipWepon(string name)
    {
        //すでに武器が作成されていた場合削除する
        if(m_Weapon != null)
        {
            Destroy(m_Weapon);
            m_Weapon = null;
            Resources.UnloadUnusedAssets();
        }

        //Prefabをインスタンス化
        m_Weapon = Instantiate(Resources.Load(name), m_WeaponPoint.transform.position, m_WeaponPoint.transform.rotation) as GameObject;

        //見えない武器の子として登録
        m_Weapon.transform.parent = m_WeaponPoint.transform;
    }
}
