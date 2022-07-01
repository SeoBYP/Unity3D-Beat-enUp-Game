using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class ItemShopPopupUI : PopupUI
{
    enum Buttons
    {
        ExitButton,
        TradeButton,
    }
    enum Texts
    {
        EnergyText,
        GoldText,
        GemText,
    }
    ItemShopPopup _itemShopPopup;
    ItemTradePopup _itemTradePopup;
    InventoryPopup _inventoryPopup;
    TopBar _topBar;
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.TradeButton).gameObject, OnTradeBtnClicked, Define.UIEvents.Click);
        Binds();
        SetTopBar();
    }

    private void Binds()
    {
        _itemShopPopup = GetComponentInChildren<ItemShopPopup>();
        if (_itemShopPopup != null)
            _itemShopPopup.Init();
        _itemTradePopup = GetComponentInChildren<ItemTradePopup>();
        if (_itemTradePopup != null)
            _itemTradePopup.Init();
        _inventoryPopup = GetComponentInChildren<InventoryPopup>();
        if (_inventoryPopup != null)
            _inventoryPopup.Init();
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

    public void SetSale(int itemID, IItemSlot itemSlot)
    {
        _itemTradePopup.SetSaleItemSlot(itemID,itemSlot);
    }

    public void SetBuy(int itemID)
    {
        _itemTradePopup.SetBuyItemSlot(itemID);
    }

    private void OnExitBtnClicked(PointerEventData data)
    {
        base.ClosePopupUI();
    }

    private void OnTradeBtnClicked(PointerEventData data)
    {
        _itemTradePopup.ClearSlots();
        if(_inventoryPopup != null)
            _inventoryPopup.Init();
        SetTopBar();
    }
}
