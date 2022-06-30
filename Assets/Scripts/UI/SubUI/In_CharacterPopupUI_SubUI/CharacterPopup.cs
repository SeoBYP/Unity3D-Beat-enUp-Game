using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class CharacterPopup : BaseUI
{
    enum Buttons
    {
        LeftButton,
        RightButton,
        //LevelUpButton,
        //PowerUpButton,
    }
    enum Images
    {
        CharacterImage
    }
    enum Texts
    {
        CharacterNameTexts,
    }
    private int currentCharID;
    CharacterItemSlotList _characterItemSlotList;
    public override void Init()
    {
        Binds();
        SetCharecter();
    }

    private void Binds()
    {
        //Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        //BindEvent(GetButton((int)Buttons.LeftButton).gameObject, OnLeftBtnClicked, Define.UIEvents.Click);
        //BindEvent(GetButton((int)Buttons.RightButton).gameObject, OnRightBtnClicked, Define.UIEvents.Click);
        //BindEvent(GetButton((int)Buttons.LevelUpButton).gameObject, OnLeftBtnClicked, Define.UIEvents.Click);
        //BindEvent(GetButton((int)Buttons.PowerUpButton).gameObject, OnPowerUpBtnClicked, Define.UIEvents.Click);
    }

    public void SetCharecter()
    {
        currentCharID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        GetImage((int)Images.CharacterImage).sprite = CharacterStatManager.Instance.GetCharacterPopupIcon(currentCharID);
        GetText((int)Texts.CharacterNameTexts).text = DataManager.Instance.GetPlayer(1).GetCharacter(currentCharID).NAME;
        _characterItemSlotList = GetComponentInChildren<CharacterItemSlotList>();
        if (_characterItemSlotList != null)
        {
            _characterItemSlotList.Init();
            _characterItemSlotList.CheckSlot(currentCharID);
        }
    }

    public void SetCharacterEquipment(int itemID)
    {
        if (CharacterStatManager.Instance.CheckChatacterJobWithItemID(currentCharID, itemID))
        {
            if (DataManager.Instance.GetPlayer(1).CheckCharacterEquip(currentCharID, itemID))
            {
                UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("이미 착용중인\n아이템이 있습니다.");
                return;
            }
            else
            {
                _characterItemSlotList.SetCharacterItemSlot(itemID, currentCharID);
                FindObjectOfType<ItemEquipmentPopup>().SetItemSlotEquip(true);
            }
        }
        else
        {
            UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("이 캐릭터는\n착용할 수 없는 아이템입니다.");
        }
    }

    public void UnSetCharacterEquipment(int itemID)
    {
        if (CharacterStatManager.Instance.CheckChatacterJobWithItemID(currentCharID, itemID))
        {
            _characterItemSlotList.UnSetCharacterItemSlot(itemID, currentCharID);
            FindObjectOfType<ItemEquipmentPopup>().SetItemSlotEquip(false);
        }
    }

    //private void OnLeftBtnClicked(PointerEventData data)
    //{
        
    //    SetCharecter();
    //}
    //private void OnRightBtnClicked(PointerEventData data)
    //{
    //    SetCharecter();
    //}
    //private void OnLevelUpBtnClicked(PointerEventData data)
    //{
    //    print("To do // OnLevelUpBtnClicked");
    //}
    //private void OnPowerUpBtnClicked(PointerEventData data)
    //{
    //    print("To do // OnPowerUpBtnClicked");
    //}
}
