using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFloorSearchCharacter : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
        {
            GetComponentInParent<FallFloor>().SetState(FallFloor.ColliderState.Stay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
        {
            GetComponentInParent<FallFloor>().SetState(FallFloor.ColliderState.Absent);
        }
    }

}