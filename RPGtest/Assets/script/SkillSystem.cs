using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    attack1,
    attack2,
    attack3,
    attack4,
    attack5,
    attack6
}

public class SkillSystem : MonoBehaviour {

    [SerializeField] private int skillPoint;//スキルを覚えさせるためのスキルポイント
    [SerializeField] private bool[] skills;//スキルを覚えているかどうかのフラグ  
    [SerializeField] private SkillParam[] skillParams;//スキル毎のパラメーター

    public Text skillText;

    private void Awake()
    {
        //スキル数分の配列を確保
        skills = new bool[skillParams.Length];
        SetText();
    }

    //スキルを覚える
    public void SetSkill(SkillType type, int point)
    {
        skills[(int)type] = true;
        SetSkillPoint(point);
        SetText();
        CheckOnOff();
    }

    //スキルを覚えているかどうかのチェック
    public bool IsSkill(SkillType type)
    {
        return skills[(int)type];
    }

    //スキルポイントを減らす
    public void SetSkillPoint(int point)
    {
        skillPoint -= point;
    }

    //スキルポイントを取得
    public int GetSkillPoint()
    {
        return skillPoint;
    }

    //スキルを覚えられるかチェック
    public bool Check(SkillType type, int spendPoint = 0)
    {
        //持っているスキルポイントが足りない
        if (skillPoint < spendPoint)
        {
            return false;
        }
        //攻撃UP2は攻撃UP1を覚えて居なければ駄目。
        if (type == SkillType.attack2)
        {
            return skills[(int)SkillType.attack1];
        }
        else if (type == SkillType.attack3)
        {
            return skills[(int)SkillType.attack2];
        }
        else if (type == SkillType.attack6)
        {
            return skills[(int)SkillType.attack5];
        }
        return true;
    }

    private void CheckOnOff() {
        foreach (var skillParam in skillParams)
        {
            skillParam.CheckOnOff();
        }
    }

    private void SetText (){
        skillText.text = "スキルポイント：" + skillPoint;
    }
}
