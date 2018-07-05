using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillParam : MonoBehaviour {

    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private SkillType type;//このスキルの種類
    [SerializeField] private int spendPoint;//このスキルを覚える為に必要なスキルポイント
    [SerializeField] private string skillTitle;//スキルタイトル
    [SerializeField] private string skillInformation;//スキル情報
    public Text text;

    // Use this for initialization
    void Start() {
        //スキルを覚えられる状態でなければボタンを無効化
        CheckOnOff();
    }

    //スキルボタンを押した際に実行するメソッド
    public void OnClick()
    {
        //スキルを覚えていたら何もせずreturn
        if (skillSystem.IsSkill(type))
        {
            return;
        }
        //スキルを覚えられるかチェック
        if (skillSystem.Check(type,spendPoint))
        {
            skillSystem.SetSkill(type, spendPoint);
            changeButtonColor(new Coloe(0f, 0f, 1f, 1f));
            text.text = skillTitle + "を覚えた";
        }
        else
        {
            text.text = "スキルを覚えられません";
        }
    }

    //他のスキルを取得した後の自身のボタンの処理
    public void CheckOnOff()
    {
        //スキルを覚えられるかチェック
        if (!skillSystem.Check(type))
        {
            ChangeButtonColor(new Color(0.8f, 0.8f, 0.8f, 0.8f));
            //スキルをまだ覚えていない
        }
        else if(!skillSystem.IsSkill(type))
        {
            ChangeButtonColor(new Color(1f, 1f, 1f, 1f));
        }
    }
    //スキル情報を表示
    public void SetText()
    {
        text.text = skillTitle + ":消費スキルポイント" + spendPoint + "\n" + skillInformation;
    }
}
