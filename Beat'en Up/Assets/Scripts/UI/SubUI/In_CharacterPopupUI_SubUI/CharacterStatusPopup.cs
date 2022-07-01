using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class CharacterStatusPopup : BaseUI
{
    enum Texts
    {
        JobName,
        LevelText,
        EXPText,
        HPText,
        AttackText,
        DefenceText,
    }
    enum Images
    {
        JobIcon,
        EXPFillAmount,
        HPFillAmount,
        AttackFillAmount,
        DefenceFillAmount,
    }
    
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        //Bind<Image>(typeof(BarImages));
        SetStatus();
    }

    public void SetStatus()
    {
        int charID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        SetJob(charID);
        SetLevelAndExp(charID);
        SetHP(charID);
        SetAttack(charID);
        SetDefence(charID);
    }

    private void SetJob(int charID)
    {
        GetImage((int)Images.JobIcon).sprite = CharacterStatManager.Instance.GetJobIcon(charID);
        GetText((int)Texts.JobName).text = CharacterStatManager.Instance.GetString(charID,CharacterIndex.JOB);
    }

    private void SetLevelAndExp(int charID)
    {
        float exp = DataManager.Instance.GetPlayer(1).GetCharacter(charID).EXP;//CharacterStatManager.Instance.GetInt(charID, CharacterIndex.EXP);
        float maxexp = DataManager.Instance.GetPlayer(1).GetCharacter(charID).MAXEXP;
        float EXPAmount = exp / maxexp;
        GetImage((int)Images.EXPFillAmount).fillAmount = EXPAmount;
        GetText((int)Texts.EXPText).text = $"{exp} / {maxexp}";
        GetText((int)Texts.LevelText).text = $"{DataManager.Instance.GetPlayer(1).GetCharacter(charID).LEVEL}";
    }
    private void SetHP(int charID)
    {
        float HPAmount = Utils.SetHPAmount(charID);//(CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.HP) / 1000);
        GetImage((int)Images.HPFillAmount).fillAmount = HPAmount / 1000;
        GetText((int)Texts.HPText).text = $"{Mathf.Round(HPAmount)}";
    }
    private void SetAttack(int charID)
    {
        float AttackAmount = Utils.SetAttackAmount(charID);//(CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.ATTACK) / 1000);
        GetImage((int)Images.AttackFillAmount).fillAmount = AttackAmount / 1000;
        GetText((int)Texts.AttackText).text = $"{Mathf.Round(AttackAmount)}";
    }
    private void SetDefence(int charID)
    {
        float DefenceAmount = Utils.SetDefenceAmount(charID);//(CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.DEFENCE) / 1000);
        GetImage((int)Images.DefenceFillAmount).fillAmount = DefenceAmount / 1000;
        GetText((int)Texts.DefenceText).text = $"{Mathf.Round(DefenceAmount)}";
    }

    
}
