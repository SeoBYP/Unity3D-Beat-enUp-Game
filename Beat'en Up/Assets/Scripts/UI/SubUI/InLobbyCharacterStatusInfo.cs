using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class InLobbyCharacterStatusInfo : BaseUI
{
    enum Buttons
    {
        PrevButton,
        NextButton,
    }
    enum GameObjects
    {
        Content1,
        Content2,
    }
    enum Texts
    {
        CurrentPageText,
        NameText,
        LevelText,
        EXPText,
        HPText,
        AttackText,
        DefenceText,
    }
    enum Images
    {
        Icon,
        EXPFillAmount,
        HPFillAmount,
        AttackFillAmount,
        DefenceFillAmount,
    }

    private GameObjects CurrentContent;
    public override void Init()
    {
        CurrentContent = GameObjects.Content1;
        Binds();

        DeActivePage();
        GetGameObject((int)CurrentContent).SetActive(true);

    }


    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        SetStatus();
        BindEvent(GetButton((int)Buttons.PrevButton).gameObject, OnPrevButtonClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.NextButton).gameObject, OnNextButtonClicked, Define.UIEvents.Click);
    }

    private void DeActivePage()
    {
        for(int i = 0; i < 2; i++)
        {
            GetGameObject(i).SetActive(false);
        }
    }

    void SetNextPage()
    {
        DeActivePage();
        CurrentContent++;
        if (CurrentContent.GetHashCode() >= 2)
            CurrentContent = GameObjects.Content2;
        GetGameObject((int)CurrentContent).SetActive(true);
    }
    void SetPrevPage()
    {
        DeActivePage();
        CurrentContent--;
        if (CurrentContent.GetHashCode() < 0)
            CurrentContent = 0;
        GetGameObject((int)CurrentContent).SetActive(true);
    }

    private void OnNextButtonClicked(PointerEventData data)
    {
        SetNextPage();

    }
    private void OnPrevButtonClicked(PointerEventData data)
    {
        SetPrevPage();
    }

    public void SetStatus()
    {
        int charID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        GetText((int)Texts.NameText).text = DataManager.Instance.GetPlayer(1).GetCharacter(charID).NAME;
        GetImage((int)Images.Icon).sprite = CharacterStatManager.Instance.GetCharacterIcon(charID);
        SetLevelAndExp(charID);
        SetHP(charID);
        SetAttack(charID);
        SetDefence(charID);
    }
    private void SetLevelAndExp(int charID)
    {
        float exp = DataManager.Instance.GetPlayer(1).GetCharacter(charID).EXP;
        float maxexp = DataManager.Instance.GetPlayer(1).GetCharacter(charID).MAXEXP;
        float EXPAmount = exp / maxexp;
        GetImage((int)Images.EXPFillAmount).fillAmount = EXPAmount;
        GetText((int) Texts.EXPText).text = $"{exp} / {maxexp}";
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
