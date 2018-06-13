using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

    private Text messageText;//メッセージUI
    private string message;//表示するメッセージ
    [SerializeField]
    private int maxTextLength = 90;//一回のメッセージの最大文字数 
    private int testLength = 0;//一回のメッセージの現在文字数
    [SerializeField]
    private int maxLine = 3;//一回のメッセージの最大行数
    private int nowLine = 0;//現在の行
    [SerializeField]
    private float textSpeed = 0.05f;//テキストスピード
    private float elapsdTime = 0f;//経過時間
    private int nowTextNum = 0;//今見ている文字番号
    private Image clickIcon;
    [SerializeField]
    private float ClickFlashTime = 0.2f;//クリックアイコン
    private bool isOneMessage = false;//一回分のメッセージを表示したかどうか
    private bool isEndMessage = false;//メッセージすべてを表示したかどうか。


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
