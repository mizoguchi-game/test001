using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateBlockScript : MonoBehaviour {

    private GameObject obj;
    private SceneScript script;

    void Start()
    { 
        GetComponent<Collider>().isTrigger = true;
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }

    private void OnTriggerEnter ()
    {
        

        script.score += 5;

        Destroy(gameObject);
    }
}
