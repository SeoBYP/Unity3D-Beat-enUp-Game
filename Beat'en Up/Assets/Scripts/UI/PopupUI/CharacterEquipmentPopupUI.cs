using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class CharacterEquipmentPopupUI : PopupUI
{
    enum Buttons
    {
        ExitButton,
    }
    enum Texts
    {
        EnergyText,
        GoldText,
        GemText,
    }
    InventoryPopup _inventoryPopup;
    CharacterPopup _characterPopup;
    ItemEquipmentPopup _itemEquipmentPopup;
    TopBar _topBar;
    public override void Init()
    {
        base.Init();
        Binds();
        _inventoryPopup = GetComponentInChildren<InventoryPopup>();
        if (_inventoryPopup != null)
            _inventoryPopup.Init();
        _characterPopup = GetComponentInChildren<CharacterPopup>();
        if (_characterPopup != null)
            _characterPopup.Init();
        _itemEquipmentPopup = GetComponentInChildren<ItemEquipmentPopup>();
        if (_itemEquipmentPopup != null)
            _itemEquipmentPopup.Init();
        SetTopBar();
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    private void Binds()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitBtnClicked, Define.UIEvents.Click);
    }

    public void SetTopBar()
    {
        if (_topBar == null)
        {
            _topBar = GetComponentInChildren<TopBar>();
        }
        _topBar.Init();
    }

    public void SetItemEquipmentInfo(int itemID,IItemSlot slot)
    {
        _itemEquipmentPopup.SetActive(true);
        _itemEquipmentPopup.SetItemInformation(itemID,slot);
    }

    public void SetCharacterEquipment(int itemID)
    {
        if(_characterPopup != null)
            _characterPopup.SetCharacterEquipment(itemID);
    }

    public void UnSetCharacterEquipment(int itemID)
    {
        if (_characterPopup != null)
            _characterPopup.UnSetCharacterEquipment(itemID);
    }

    private void OnExitBtnClicked(PointerEventData data)
    {
        CharacterPopupUI popupUI = FindObjectOfType<CharacterPopupUI>();
        if (popupUI != null)
            popupUI.SetCharacterPopupUI();
        base.ClosePopupUI();
    }
}
