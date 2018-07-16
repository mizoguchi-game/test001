using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainComera;
    [SerializeField] private GameObject cubePrehab;
    [SerializeField] private GameObject rootObject;
    private NKPrefabMan nKPrefabMan;

    public void GameStart()
    {
        SceneManager.LoadScene("test");
    }

    public void GameEnd()
    {

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(mainComera);
    }

    private void Start()
    {
        nKPrefabMan = GetComponent<NKPrefabMan>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enter");
            SceneManager.LoadScene("SceneMoveData");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var tmpcubePrehab = GameObject.Instantiate(cubePrehab) as GameObject;
            tmpcubePrehab.transform.SetParent(rootObject.transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            nKPrefabMan.savePrefab(rootObject,"test");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            var test = nKPrefabMan.loadPrefab("test");
            rootObject = GameObject.Instantiate(test) as GameObject;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Destroy(rootObject);
        }
    }
}
