using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class TopBar : BaseUI
{
    enum Texts
    {
        EnergyText,
        GoldText,
        GemText,
    }
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        SetEnegyText();
        SetGoldText();
        SetGemText();
    }

    private void SetEnegyText()
    {
        int enegevalue = DataManager.Instance.GetPlayer(1).PlayerInfo.Enegy;
        if(enegevalue == 0)
        {
            GetText((int)Texts.EnergyText).text = $"{0}/100";
            return;
        }
        GetText((int)Texts.EnergyText).text = $"{enegevalue}/100";
    }

    private void SetGoldText()
    {
        int goldvalue = DataManager.Instance.GetPlayer(1).PlayerInfo.Gold;
        if(goldvalue == 0)
        {
            GetText((int)Texts.GoldText).text = "0";
            return;
        }
        GetText((int)Texts.GoldText).text = GetThousandCommaText(goldvalue);
    }

    private void SetGemText()
    {
        int gemvalue = DataManager.Instance.GetPlayer(1).PlayerInfo.Gem;
        if(gemvalue == 0)
        {
            GetText((int)Texts.GemText).text = "0";
            return;
        }
        GetText((int)Texts.GemText).text = GetThousandCommaText(gemvalue);
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
