using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    public GUIStyle style;

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Game Over!!", style);
    }
}
