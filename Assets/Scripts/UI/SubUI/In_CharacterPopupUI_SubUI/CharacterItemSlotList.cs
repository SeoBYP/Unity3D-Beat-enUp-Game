using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class CharacterItemSlotList : BaseUI
{
    enum Slots
    {
        HeadItemSlot,
        UpperArmorItemSlot,
        UnderArmorItemSlot,
        ShoesItemSlot,
        WeaponItemSlot,
        AccessoryItemSlot,
    }
    CharacterPopupUI _characterPopupUI;
    public override void Init()
    {
        _characterPopupUI = GetComponentInParent<CharacterPopupUI>();
        Bind<CharacterItemSlot>(typeof(Slots));

        for(int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case (int)Slots.HeadItemSlot:
                    Get<CharacterItemSlot>(i).Init();
                    Get<CharacterItemSlot>(i).SetSlotItemType(ItemType.Head,CharacterIndex.HeadID);
                    break;
                case (int)Slots.UpperArmorItemSlot:
                    Get<CharacterItemSlot>(i).Init();
                    Get<CharacterItemSlot>(i).SetSlotItemType(ItemType.UpperArmor, CharacterIndex.UpperArmorID);
                    break;
                case (int)Slots.UnderArmorItemSlot:
                    Get<CharacterItemSlot>(i).Init();
                    Get<CharacterItemSlot>(i).SetSlotItemType(ItemType.UnderArmor, CharacterIndex.UnderArmorID);
                    break;
                case (int)Slots.ShoesItemSlot:
                    Get<CharacterItemSlot>(i).Init();
                    Get<CharacterItemSlot>(i).SetSlotItemType(ItemType.Shoes,CharacterIndex.ShoesID);
                    break;
                case (int)Slots.WeaponItemSlot:
                    Get<CharacterItemSlot>(i).Init();
                    Get<CharacterItemSlot>(i).SetSlotItemType(ItemType.Weapon,CharacterIndex.WeaponID);
                    break;
                case (int)Slots.AccessoryItemSlot:
                    Get<CharacterItemSlot>(i).Init();
                    Get<CharacterItemSlot>(i).SetSlotItemType(ItemType.Accessory, CharacterIndex.AccessoryID);
                    break;
            }
        }
    }

    public void SetCharacterItemSlot(int itemID,int charID)
    {
        for (int i = 0; i < 6; i++)
        {
            Get<CharacterItemSlot>(i).SetItem(itemID,charID);
        }
    }

    public void UnSetCharacterItemSlot(int itemID,int charID)
    {
        for(int i = 0; i < 6; i++)
        {
            Get<CharacterItemSlot>(i).UnSetItem(itemID, charID);
        }
    }

    public void CheckSlot(int charID)
    {
        for (int i = 0; i < 6; i++)
        {
            Get<CharacterItemSlot>(i).CheckCharacterItemID(charID);
        }
    }
}
