using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class CharacterSlot : BaseUI
{
    enum Images
    {
        Icon,
        Focus,
    }
    Button _slotbutton;
    CharactersSelectPopup _charactersSelect;
    CharacterPopupUI _characterPopupUI;
    private int currentCharID;
    public override void Init()
    {
        _charactersSelect = GetComponentInParent<CharactersSelectPopup>();
        _characterPopupUI = GetComponentInParent<CharacterPopupUI>();
        Bind<Image>(typeof(Images));
        _slotbutton = GetComponent<Button>();
        BindEvent(_slotbutton.gameObject, OnSlotBtnClicked, Define.UIEvents.Click);
        SetFocus(false);
    }

    public void SetIcon(int charID)
    {
        currentCharID = charID;
        GetImage((int)Images.Icon).sprite = CharacterStatManager.Instance.GetCharacterIcon(charID);
    }

    private void OnSlotBtnClicked(PointerEventData data)
    {
        _charactersSelect.ReSetSlotDicFocus();
        this.SetFocus(true);
        DataManager.Instance.GetPlayer(1).PlayerInfo.SetSeleteCharacterID(currentCharID);
        _characterPopupUI.ReSetCharacterPopupUI();
    }

    public void SetFocus(bool state)
    {
        GetImage((int)Images.Focus).gameObject.SetActive(state);
    }
}
