using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class BossHPBar : BaseUI
{
    enum Images
    {
        BossHealthUI,
    }
    enum Texts
    {
        BossNameText,
    }
    private Image _enemyhpUI;
    private float DefaultHp;
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        _enemyhpUI = GetImage(0);
        _enemyhpUI.fillAmount = 1;
    }

    public void SetEnemyName(int charID)
    {
        GetText(0).text = CharacterStatManager.Instance.GetString(charID, CharacterIndex.NAME);
        DefaultHp = CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.HP);
    }

    public void DisplayHealth(float value)
    {
        value /= DefaultHp;
        if (value < 0)
            value = 0;
        _enemyhpUI.fillAmount = value;
    }
}
