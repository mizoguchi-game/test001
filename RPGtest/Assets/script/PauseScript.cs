using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    //ポーズにした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;

    //ポーズUIのインスタンス
    private GameObject instancePauseUI;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            if (instancePauseUI == null)
            {
                instancePauseUI = GameObject.Instantiate(pauseUI) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(instancePauseUI);
                Time.timeScale = 1f;
            }
        }
	}
}
