using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class TradeItemSlot : BaseUI
{
    enum Images
    {
        TypeIcon,
    }
    enum Texts
    {
        ItemName,
        ItemRarity,
        ItemCount,
    }
    IItemSlot _itemslot;
    Button button;
    int _itemCount = 0;
    private int currentItemId;
    public override void Init()
    {
        button = GetComponent<Button>();
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        BindEvent(button.gameObject, OnBtnClicked, Define.UIEvents.Click);
    }

    public void SetItemShopInfo(int itemID,IItemSlot itemSlot = null)
    {
        currentItemId = itemID;
        _itemCount = 1;
        _itemslot = itemSlot;
        GetImage((int)Images.TypeIcon).sprite = ItemDataManager.Instance.GetItemTypeIconSprite(itemID);
        GetText((int)Texts.ItemName).text = ItemDataManager.Instance.GetString(itemID, ItemData.NAME);
        GetText((int)Texts.ItemRarity).text = ItemDataManager.Instance.GetString(itemID, ItemData.RARITY);
        GetText((int)Texts.ItemCount).text = $"{_itemCount}";
    }

    public void SetItemCount()
    {
        _itemCount++;
        GetText((int)Texts.ItemCount).text = $"{_itemCount}";
    }

    public void OnDestroy()
    {
        if (_itemslot != null)
            _itemslot.IDestroy();
    }

    public void DeletaPlayerItem()
    {
        DataManager.Instance.GetPlayer(1).DeletePlayerItem(currentItemId);
    }

    public void AddPlayerItem()
    {
        DataManager.Instance.GetPlayer(1).SetPlayerItem(currentItemId);
    } 

    private void OnBtnClicked(PointerEventData data)
    {
        ItemTradePopup itemTrade = FindObjectOfType<ItemTradePopup>();
        if(itemTrade != null)
        {
            itemTrade.DeleteBuyItemSlots(currentItemId,_itemCount);
            itemTrade.DeleteSaleItemSlot(currentItemId, _itemCount);
        }
        Destroy(this.gameObject);
    }
}
