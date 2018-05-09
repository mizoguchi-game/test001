using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    private GameObject obj;
    private SceneScript script;

    private void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }

    private void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.tag == "Ball")
        {
            Destroy(gameObject);
        }
        script.score += 10;

    }

}
