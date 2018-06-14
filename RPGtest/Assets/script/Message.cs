using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

    private Text messageText;//メッセージUI
    private string message;//表示するメッセージ
    [SerializeField]
    private int maxTextLength = 90;//一回のメッセージの最大文字数 
    private int textLength = 0;//一回のメッセージの現在文字数
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
        clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
        clickIcon.enabled = false;
        messageText = GetComponentInChildren<Text>();
        messageText.text = "";
        SetMessage("hogehogehogehogehogehogehogehogehogehogehoge\n"
            + "hogehogehogehogehogehogehogehogehogehogehoge\n"
            + "hogehogehogehogehogehogehogehogehogehogehoge\n");

    }
	
	// Update is called once per frame
	void Update () {
        //メッセージが終わっていない、または設定されている
        if (isEndMessage||message == null)
        {
            return;
        }
        //一回に表示するメッセージを表示していない
        if (!isOneMessage)
        {
            //テキスト表示時間を経過したら
            if (elapsdTime >= textSpeed)
            {
                messageText.text += message[nowTextNum];
                //改行文字だたら改行を足す
                if (message[nowTextNum] == '\n')
                {
                    nowLine++;
                }
                nowTextNum++;
                textLength++;
                elapsdTime = 0f;
                //メッセージを全部表示、または行数が最大数表示された。
                if (nowTextNum >= message.Length || textLength >= maxTextLength || nowLine >= maxLine)
                {
                    isOneMessage = true;
                }
            }
            elapsdTime += Time.deltaTime;

            //メッセージ表示中にマウスの左ボタンを押したら一括表示
            if (Input.GetMouseButtonDown(0))
            {
                //ここまでに表示されているテキストを代入
                var allText = messageText.text;

                //表示するメッセージを繰り返す
                for (var i = nowTextNum;i < message.Length;i++)
                {
                    allText += message[i];

                    if (message[i] == '\n')
                    {
                        nowLine++;
                    }
                    nowTextNum++;
                    textLength++;

                    //メッセージが全て表示される、または一回表示限度を超えた
                    if (nowTextNum >= message.Length || textLength >= maxTextLength || nowLine >= maxLine)
                    {
                        messageText.text = allText;
                        isOneMessage = true;
                        break;
                    }
                }
            }
            //一回に表示されるメッセージを表示した
        }
        else
        {
            elapsdTime += Time.deltaTime;

            //クリックアイコンを点灯する時間を超えた時反転させる。
            if (elapsdTime >= ClickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsdTime = 0f;
            }

            //マウスクリックされたら次の文字表示処理
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(messageText.text.Length);
                messageText.text = "";//表示テキストを初期化
                nowLine = 0;//現在の行を初期化
                clickIcon.enabled = false;
                elapsdTime = 0f;
                textLength = 0;//現在の文字数を初期化
                isOneMessage = false;//メッセージ表示フラグを初期化

                //メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if (nowTextNum >= message.Length)
                {
                    nowTextNum = 0;
                    isEndMessage = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
	}

    private void SetMessage(string _message)
    {
        message = _message;
    }

    //他スクリプトからのメッセージ設定
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
        isEndMessage = false;
    }
}
