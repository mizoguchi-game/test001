using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemStatus : MonoBehaviour {

	public enum Item
    {
        Sword,
        Staff,
        Gun
    };

    [SerializeField]
    private bool[] itemFlags = new bool[6];

    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }
}
