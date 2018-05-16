﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            MoveEnemy.EnamyState state = GetComponentInParent<MoveEnemy>().GetState();

            if (state != MoveEnemy.EnamyState.Chase)
            {
                Debug.Log("Player発見");
                GetComponentInParent<MoveEnemy>().SetState("Chase", other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("見失う");
            GetComponentInParent<MoveEnemy>().SetState("wait");
        }    
    }
}