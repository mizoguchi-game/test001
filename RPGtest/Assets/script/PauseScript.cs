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

    private RectTransform title;
    private RectTransform main;
    private RectTransform information;

    private int width;
    private int height;

    private bool changeWindouSize = false;

    private void Start()
    {
        title = PropertyUI.transform.GetChild(0).Find("Title").GetComponent<RectTransform>();//11%
        main = PropertyUI.transform.GetChild(0).Find("main").GetComponent<RectTransform>();//68%
        information = PropertyUI.transform.GetChild(0).Find("Information").GetComponent<RectTransform>();//20%

        width = Screen.width;
        height = Screen.height;

        title.sizeDelta = new Vector2(width,(float)(height * 0.11));
        main.sizeDelta = new Vector2(width, (float)(height * 0.68));
        information.sizeDelta = new Vector2(width, (float)(height * 0.20));
    }

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
                PropertyUI.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PropertyUI.activeSelf == false && instancePauseUI != null)
            {
                PropertyUI.SetActive(true);
            }
            else
            {
                PropertyUI.SetActive(false);
            }

        }

        if (PropertyUI.activeSelf == true)
        {
            if (width != Screen.width)
            {
                width = Screen.width;
                changeWindouSize = true;

            }
            if (height != Screen.height)
            {
                height = Screen.height;
                changeWindouSize = true;
            }
            if (changeWindouSize == true)
            {
                title.sizeDelta = new Vector2(width, (float)(height * 0.11));
                main.sizeDelta = new Vector2(width, (float)(height * 0.68));
                information.sizeDelta = new Vector2(width, (float)(height * 0.20));
                changeWindouSize = false;
            }
        }
    }
}
