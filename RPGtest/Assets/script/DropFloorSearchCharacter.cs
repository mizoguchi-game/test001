using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFloorSearchCharacter : MonoBehaviour {
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag =="NPC")
        {
            GetComponentInParent<FallFloor>().setState(FallFloor.CollisionState.Enter);
        }        
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "NPC")
        {
            GetComponentInParent<FallFloor>().setState(FallFloor.CollisionState.Stay);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "NPC")
        {
            GetComponentInParent<FallFloor>().setState(FallFloor.CollisionState.Exit);
        }
    }

    
}
