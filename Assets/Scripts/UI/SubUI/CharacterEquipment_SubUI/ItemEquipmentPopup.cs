using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class ItemEquipmentPopup : BaseUI
{
    enum Transforms
    {
        ItemSlot,
    }
    enum Texts
    {
        RarityText,
        ItemName,
        TypeText,
        UniqueStatText,
        RareStatText,
        NormalStatText,
    }
    enum Images
    {
        TypeIcon,
        Arrow,
    }
    enum Buttons
    {
        EquipmentButton,
        DeEquipmentButton,
    }

    private int currentItemID;
    Transform _itemSlotPos;
    private CharacterEquipmentPopupUI _characterPopup;
    private IItemSlot _itemSlot;
    public override void Init()
    {
        Binds();
        SetActive(false);
        _itemSlotPos = Get<Transform>(0);
        _characterPopup = GetComponentInParent<CharacterEquipmentPopupUI>();
    }

    private void Binds()
    {
        Bind<Transform>(typeof(Transforms));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.EquipmentButton).gameObject, OnEquipmentBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.DeEquipmentButton).gameObject, OnDeEquipmentBtnClicked, Define.UIEvents.Click);
    }

    public void SetItemInformation(int itemtableid = 0,IItemSlot slot = null)
    {
        UIManager.Instance.LoadItemSlot(_itemSlotPos, itemtableid);
        ItemInformation(itemtableid);
        _itemSlot = slot;
    }

    private void ItemInformation(int itemID)
    {
        currentItemID = itemID;
        if(ItemType.None == ItemDataManager.Instance.GetItemType(itemID))
        {
            GetText((int)Texts.ItemName).text = ItemDataManager.Instance.GetString(itemID, ItemData.NAME);
            GetText((int)Texts.TypeText).text = ItemDataManager.Instance.GetString(itemID, ItemData.TYPE);
            GetText((int)Texts.RarityText).text = ItemDataManager.Instance.GetString(itemID, ItemData.RARITY);
            GetImage((int)Images.TypeIcon).sprite = ItemDataManager.Instance.GetItemTypeIconSprite(itemID);
            SetNoneItemStat();
            return;
        }
        GetText((int)Texts.ItemName).text = ItemDataManager.Instance.GetString(itemID, ItemData.NAME);
        GetText((int)Texts.TypeText).text = ItemDataManager.Instance.GetString(itemID, ItemData.TYPE);
        GetText((int)Texts.RarityText).text = ItemDataManager.Instance.GetString(itemID, ItemData.RARITY);
        GetImage((int)Images.TypeIcon).sprite = ItemDataManager.Instance.GetItemTypeIconSprite(itemID);
        SetItemStatus(itemID);
        SetArrow(true);
    }

    private void SetNoneItemStat()
    {
        GetText((int)Texts.NormalStatText).text = "일반아이템";
        GetText((int)Texts.RareStatText).text = " ";
        GetText((int)Texts.UniqueStatText).text = " ";
    }

    private void SetItemStatus(int itemID)
    {
        GetText((int)Texts.NormalStatText).text = "Nomral Stat\n";
        GetText((int)Texts.RareStatText).text = "Rare Stat\n";
        GetText((int)Texts.UniqueStatText).text = "Unique Stat\n";

        SetStat(itemID, ItemData.ATTACK);
        SetStat(itemID, ItemData.DEFENCE);
        SetStat(itemID, ItemData.CRITICAL);
        SetStat(itemID, ItemData.HP);
    }
    private void SetStat(int id, ItemData index)
    {
        float statindex = ItemDataManager.Instance.GetFloat(id, index);
        string stattext = string.Empty;
        if (statindex == 0)
            return;
        else if (statindex < 5 && statindex > 0)
        {
            stattext += $"{index} : + {statindex}%\n";
            GetText((int)Texts.NormalStatText).text += stattext;
        }
        else if (statindex >= 5 && statindex <= 10)
        {
            stattext += $"{index} : + {statindex}%\n";
            GetText((int)Texts.RareStatText).text += stattext;
        }
        else if (statindex >= 11)
        {
            stattext += $"{index} : + {statindex}%\n";
            GetText((int)Texts.UniqueStatText).text += stattext;
        }
    }
    private void SetArrow(bool state)
    {
        GetImage((int)Images.Arrow).gameObject.SetActive(state);
    }

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }

    public void SetItemSlotEquip(bool state)        
    {
        if (_itemSlot != null)
            _itemSlot.ISetPlayerEquip(state);
    }

    private void OnEquipmentBtnClicked(PointerEventData data)
    {
        if (_characterPopup != null)
        {
            if(DataManager.Instance.GetPlayer(1).CheckItemEquip(currentItemID) == false)
            {
                if(ItemDataManager.Instance.GetItemType(currentItemID) != ItemType.None)
                    _characterPopup.SetCharacterEquipment(currentItemID);
                else
                {
                    UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("착용할 수 없는 아이템입니다.");
                    return;
                } 
            }
            else
            {
                UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("이미 착용중인 아이템입니다.");
            }
        }

    }
    private void OnDeEquipmentBtnClicked(PointerEventData data)
    {
        int charID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        if(DataManager.Instance.GetPlayer(1).CheckCharacterEquip(charID,currentItemID) == false)
        {
            UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("현재 캐릭터에서 장비를\n해제할 수 없습니다.");
            return;
        }
        if (_characterPopup != null)
            _characterPopup.UnSetCharacterEquipment(currentItemID);
        _itemSlot.ISetPlayerEquip(false);
    }
}
