using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject charcter;

    public enum weaponTypes : int
    {
        SWORD,
        WAND,
        NUM,
    }

    private int m_weaponNo = 0;

    private string[] m_weaponsPath = new string[]
    {
        "sword",
        "staff"
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
           if(Input.GetKey(KeyCode.Q) == true)
            {
                m_kayInterval = Time.time;
                NextWeapon();
            }else if (Input.GetKey(KeyCode.E) == true)
            {
                m_kayInterval = Time.time;
                PrevWeapon();
            }

        }
    }

    private void NextWeapon()
    {
        m_weaponNo++;
        if(m_weaponNo > (int)weaponTypes.NUM)
        {
            m_weaponNo = 0;
        }

        m_characterEquipmentManager.EquipWepon(m_weaponsPath[m_weaponNo]);
    }

    private void PrevWeapon()
    {
        m_weaponNo--;
        if(m_weaponNo < 0)
        {
            m_weaponNo = (int)weaponTypes.NUM - 1;
        }

        m_characterEquipmentManager.EquipWepon(m_weaponsPath[m_weaponNo]);
    }
}
