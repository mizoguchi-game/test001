using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateBlockScript : MonoBehaviour {

    void Start()
    { 
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter ()
    {
        Destroy(gameObject);
    }
}
