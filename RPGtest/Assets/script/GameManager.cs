using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject charcter;

    public enum waponTypes : int
    {
        SWORD,
        WAND,
        NUM,
    }

    private int m_weaponNo = 0;

    private string[] m_weaponsPath = new string[]
    {
        "sword","wand"
    };

    private float m_kayInterval = 0f;

    private EquipmentManeger m_characterEquipmentManager;

    void Start()
    {
        m_characterEquipmentManager = charcter.GetComponent<EquipmentManeger>();
    }

    void Update()
    {
        if(Time.time - m_kayInterval > 0.5f)
        {
           if(Input.GetKey(KeyCode.RightArrow) == true)
            {
                m_kayInterval = Time.time;
                NextWeapon();
            }else if (Input.GetKey(KeyCode.LeftArrow) == true)
            {
                m_kayInterval = Time.time;
                PrevWeapon();
            }

        }
    }
}
