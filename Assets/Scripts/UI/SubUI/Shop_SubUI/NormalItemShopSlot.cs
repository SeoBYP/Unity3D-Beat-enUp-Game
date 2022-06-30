using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class NormalItemShopSlot : BaseUI, IItemShopSlot
{
    enum Texts
    {
        RarityText,
        ItemName,
        TypeText,
    }
    enum Images
    {
        Icon,
        TypeIcon,
    }
    Button button;
    ItemShopPopupUI _itemShopPopupUI;

    private int _itemID;

    public void IInit(int itemID)
    {
        _itemShopPopupUI = GetComponentInParent<ItemShopPopupUI>();
        _itemID = itemID;
        Init();
        ISetItemSprite(itemID);
        ISetItemShopSlotInfo(itemID);
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        button = GetComponent<Button>();
        if (button != null)
            BindEvent(button.gameObject, IOnItemShopSlotBtnClicked, Define.UIEvents.Click);
    }

    public void ISetItemSprite(int itemID)
    {
        GetImage((int)Images.Icon).sprite = ItemDataManager.Instance.GetItemIconSprite(itemID);
        GetImage((int)Images.TypeIcon).sprite = ItemDataManager.Instance.GetItemTypeIconSprite(itemID);
    }

    public void ISetItemShopSlotInfo(int itemID)
    {
        GetText((int)Texts.ItemName).text = ItemDataManager.Instance.GetString(itemID, ItemData.NAME);
        GetText((int)Texts.RarityText).text = ItemDataManager.Instance.GetString(itemID, ItemData.RARITY);
        GetText((int)Texts.TypeText).text = ItemDataManager.Instance.GetString(itemID, ItemData.TYPE);
    }

    public void IOnItemShopSlotBtnClicked(PointerEventData data)
    {
        if(_itemShopPopupUI != null)
        {
            _itemShopPopupUI.SetBuy(_itemID);
        }
    }

}
