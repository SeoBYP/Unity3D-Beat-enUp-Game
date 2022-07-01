using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class EnemyHPBar : PopupUI
{
    enum Images
    {
        HealthUI,
    }
    enum Texts
    {
        Name,
    }
    private Image _enemyhpUI;
    private float enemyDefaultHp;
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
        enemyDefaultHp = CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.HP);
    }

    public void DisplayHealth(float value)
    {
        value /= enemyDefaultHp;
        if (value < 0)
            value = 0;
        _enemyhpUI.fillAmount = value;
    }

    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
    }
}
