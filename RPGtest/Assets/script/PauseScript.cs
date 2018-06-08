using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    //ポーズにした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;

    //ポーズUIのインスタンス
    private GameObject instancePauseUI;

    [SerializeField]
    private GameObject PropertyUI;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PropertyUI.activeSelf == false)
            {
                PropertyUI.SetActive(true);
            }
            else
            {
                PropertyUI.SetActive(false);
            }

        }
    }
}
