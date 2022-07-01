using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class InventoryPopupUI : PopupUI
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
    ItemInformationPopup _itemInformationPopup;
    InventoryPopup _inventoryPopup;
    TopBar _topBar;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitButtonClicked, Define.UIEvents.Click);

        _itemInformationPopup = GetComponentInChildren<ItemInformationPopup>();
        if (_itemInformationPopup != null)
            _itemInformationPopup.Init();
        _inventoryPopup = GetComponentInChildren<InventoryPopup>();
        if (_inventoryPopup != null)
            _inventoryPopup.Init();
        SetTopBar();
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    public void SetTopBar()
    {
        if (_topBar == null)
        {
            _topBar = GetComponentInChildren<TopBar>();
        }
        _topBar.Init();
    }

    public void SetItemInformation(int itemID)
    {
        _itemInformationPopup.SetActive(true);
        _itemInformationPopup.SetItemInformation(itemID);
    }

    private void OnExitButtonClicked(PointerEventData data)
    {
        base.ClosePopupUI();
    }
}
