using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamScript : MonoBehaviour {

    public GUIStyle style;
    private GameObject obj;
    private SceneScript script;

    private void OnGUI()
    {

        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();

        GUI.Label(new Rect(10, 10, 200, 400), "点数", style);
        GUI.Label(new Rect(10, 20, 200, 400), "" + script.score, style);
        GUI.Label(new Rect(10, 40, 200, 40), "残基数", style);
        GUI.Label(new Rect(10, 50, 200, 40), "" + script.life, style);
    }
}
