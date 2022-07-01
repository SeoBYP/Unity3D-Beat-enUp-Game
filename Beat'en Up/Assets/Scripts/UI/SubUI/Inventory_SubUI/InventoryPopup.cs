using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
using ItemInformation;

public class InventoryPopup : BaseUI
{
    enum Transforms
    {
        ItemSlotGrop,
    }
    enum Buttons
    {
        WeaponButton,
        ArmorButton,
        AccessoryButton,
        ShoesButton,
        OthersButton,
        ALLButton,
    }

    List<IItemSlot> itemSlots = new List<IItemSlot>();
    Transform _itemSlotGroup;
    ScrollRect _scrollRect;
    Vector3 _defaultSize;
    public override void Init()
    {
        Bind<Transform>(typeof(Transforms));
        Bind<Button>(typeof(Buttons));
        BindEvents();
        _itemSlotGroup = Get<Transform>(0);
        _scrollRect = GetComponentInChildren<ScrollRect>();
        if (_scrollRect != null)
            _defaultSize = _scrollRect.content.sizeDelta;
        LoadPlayerItemSlot();
    }

    private void BindEvents()
    {
        BindEvent(GetButton((int)Buttons.WeaponButton).gameObject, OnWeaponBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ArmorButton).gameObject, OnArmorBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.AccessoryButton).gameObject, OnAccessoryBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ShoesButton).gameObject, OnShoseBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.OthersButton).gameObject, OnOthersBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ALLButton).gameObject, OnALLBtnClicked, Define.UIEvents.Click);
    }

    private void LoadPlayerItemSlot()
    {
        ClearItemSlots();
        Utils.CheckPlayerEquip();
        int count = 0;
        _scrollRect.content.sizeDelta = _defaultSize;
        foreach (Item item in DataManager.Instance.GetPlayer(1).Items)
        {
            int itemID = item.ID;
            if (ItemDataManager.Instance.CheckContains(itemID))
            {
                IItemSlot itemSlot = UIManager.Instance.LoadItemSlot(_itemSlotGroup, itemID);
                itemSlots.Add(itemSlot);
                itemSlot.ISetPlayerEquip(DataManager.Instance.GetPlayer(1).GetItem(itemID).PlayerEquip);
                count++;
                if (count % 4 == 0)
                {
                    SetContentSize();
                }
            }
        }
    }

    private void ClearItemSlots()
    {
        foreach(IItemSlot item in itemSlots)
        {
            item.IDestroy();
        }
        itemSlots.Clear();
    }

    private void OnWeaponBtnClicked(PointerEventData data)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
                continue;
            if (itemSlots[i].ICheckItemType(ItemType.Weapon))
                itemSlots[i].ISetACtive(true);
            else
                itemSlots[i].ISetACtive(false);
        }
    }
    private void OnArmorBtnClicked(PointerEventData data)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
                continue;
            if (itemSlots[i].ICheckItemType(ItemType.UpperArmor))
                itemSlots[i].ISetACtive(true);
            else if (itemSlots[i].ICheckItemType(ItemType.UnderArmor))
                itemSlots[i].ISetACtive(true);
            else if (itemSlots[i].ICheckItemType(ItemType.Head))
                itemSlots[i].ISetACtive(true);
            else
                itemSlots[i].ISetACtive(false);
        }
    }
    private void OnAccessoryBtnClicked(PointerEventData data)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
                continue;
            if (itemSlots[i].ICheckItemType(ItemType.Accessory))
                itemSlots[i].ISetACtive(true);
            else
                itemSlots[i].ISetACtive(false);
        }
    }
    private void OnShoseBtnClicked(PointerEventData data)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
                continue;
            if (itemSlots[i].ICheckItemType(ItemType.Shoes))
                itemSlots[i].ISetACtive(true);
            else
                itemSlots[i].ISetACtive(false);
        }
    }
    private void OnOthersBtnClicked(PointerEventData data)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
                continue;
            if (itemSlots[i].ICheckItemType(ItemType.None))
                itemSlots[i].ISetACtive(true);
            else
                itemSlots[i].ISetACtive(false);
        }
    }
    private void OnALLBtnClicked(PointerEventData data)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i] == null)
                continue;
            itemSlots[i].ISetACtive(true);
        }
    }

    public void SetContentSize()
    {
        if(_scrollRect != null)
        {
            float height = _scrollRect.content.sizeDelta.y + 150;
            float width = _scrollRect.content.sizeDelta.x;
            _scrollRect.content.sizeDelta = new Vector3(width, height);
        }
    }
}
