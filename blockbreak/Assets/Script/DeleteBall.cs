using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteBall : MonoBehaviour {

    private GameObject obj;
    private SceneScript script;
    public Transform ball;

    private void OnCollisionEnter(Collision collision)
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();

        script.life --;

        Destroy(collision.gameObject);

        if (script.life > 0)
        {
            Instantiate(ball);
        }
        else if (script.life >= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
