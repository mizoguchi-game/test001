using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBlockScript : MonoBehaviour {

    public int hp = 2;

    private GameObject obj;
    private SceneScript script;

    private void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        hp--;
        if(hp <= 0)
        {
           script.score += 20;
            Destroy(gameObject);
        }
    }
}
