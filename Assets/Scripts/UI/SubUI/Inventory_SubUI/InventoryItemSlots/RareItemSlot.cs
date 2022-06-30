using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class RareItemSlot : BaseUI,IItemSlot
{
    enum Images
    {
        Icon,
        PlayerEquip,
    }
    enum Gameobjects
    {
        Stars,
    }
    Button button;
    InventoryPopupUI _inventory;
    ItemShopPopupUI _itemShop;
    CharacterEquipmentPopupUI _chaEquipment;
    CharacterItemSlot _itemslot;

    private int _itemID;
    private ItemType _itemType;
    private ItemCharacterType _characterType;

    public void IInit(int itemID)
    {
        _itemID = itemID;
        _itemType = ItemDataManager.Instance.GetItemType(itemID);
        _characterType = ItemDataManager.Instance.SetItemCharacterType(itemID);
        Init();
        ISetItemSprite(itemID);
        ISetPlayerEquip(false);
    }

    public override void Init()
    {
        button = GetComponent<Button>();
        BindEvent(button.gameObject, IOnItemSlotBtnClicked, Define.UIEvents.Click);
        _inventory = GetComponentInParent<InventoryPopupUI>();
        _itemShop = GetComponentInParent<ItemShopPopupUI>();
        _chaEquipment = GetComponentInParent<CharacterEquipmentPopupUI>();
        _itemslot = GetComponentInParent<CharacterItemSlot>();
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(Gameobjects));
        ISetStars(true);
    }

    public void IOnItemSlotBtnClicked(PointerEventData data)
    {
        if (_inventory != null)
            _inventory.SetItemInformation(_itemID);
        if (_chaEquipment != null)
        {
            _chaEquipment.SetItemEquipmentInfo(_itemID,this);
        }
        else if (_itemslot != null)
            _itemslot.OpenharacterEquipmentPopup();
        if (_itemShop != null)
        {
            if (DataManager.Instance.GetPlayer(1).GetItem(_itemID).PlayerEquip == false)
            {
                _itemShop.SetSale(_itemID, this);
                //ISetACtive(false);
            }
            else
                UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("착용중인 아이템입니다.");
        }
    }
    public void ISetItemSprite(int itemID) { GetImage((int)Images.Icon).sprite = ItemDataManager.Instance.GetItemIconSprite(itemID); }
    public void ISetPlayerEquip(bool state) { GetImage((int)Images.PlayerEquip).gameObject.SetActive(state); }

    public bool ICheckItemType(ItemType type)
    {
        if (_itemType == type)
            return true;
        return false;
    }

    public bool ICheckItemCharacterType(ItemCharacterType type)
    {
        if (_characterType == type)
            return true;
        return false;
    }

    public void ISetACtive(bool state)
    {
        this.gameObject.SetActive(state);
    }

    public void ISetStars(bool state)
    {
        GetGameObject((int)Gameobjects.Stars).SetActive(state);
    }

    public void IDestroy()
    {
        Destroy(this.gameObject);
    }
    public void IEableButton(bool state)
    {
        button.enabled = false;
    }
}
