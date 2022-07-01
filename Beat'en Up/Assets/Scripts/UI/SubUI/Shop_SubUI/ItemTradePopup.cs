using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class ItemTradePopup : BaseUI
{
    enum Transforms
    {
        BuyItemList,
        SaleItemList,
    }
    enum Texts
    {
        GoldText,
    }

    Transform _buyItemList;
    Transform _saleItemList;

    Dictionary<int, TradeItemSlot> BuySlots = new Dictionary<int, TradeItemSlot>();
    Dictionary<int, TradeItemSlot> SaleSlots = new Dictionary<int, TradeItemSlot>();

    private int ItemPrice = 0;

    public override void Init()
    {
        Bind<Transform>(typeof(Transforms));
        Bind<Text>(typeof(Texts));
        _buyItemList = Get<Transform>((int)Transforms.BuyItemList);
        _saleItemList = Get<Transform>((int)Transforms.SaleItemList);
        SetGoldText();
    }

    public void SetSaleItemSlot(int itemid, IItemSlot itemSlot)
    {
        if (SaleSlots.ContainsKey(itemid) == false)
        {
            SaleSlots.Add(itemid, UIManager.Instance.SubUILoad<TradeItemSlot>(SubUIList.TradeItemSlot, _saleItemList));
            SaleSlots[itemid].SetItemShopInfo(itemid,itemSlot);
            SetADDItemPrice(itemid);
        }
        else
        {
            SaleSlots[itemid].SetItemCount();
            SetADDItemPrice(itemid);
        }
    }

    public void SetBuyItemSlot(int itemid)
    {
        if(BuySlots.ContainsKey(itemid) == false)
        {
            BuySlots.Add(itemid, UIManager.Instance.SubUILoad<TradeItemSlot>(SubUIList.TradeItemSlot, _buyItemList));
            BuySlots[itemid].SetItemShopInfo(itemid);
            SetDeleteItemPrice(itemid);
        }
        else
        {
            BuySlots[itemid].SetItemCount();
            SetDeleteItemPrice(itemid);
        }

    }

    public void DeleteBuyItemSlots(int itemID,int itemcount)
    {
        if (BuySlots.ContainsKey(itemID))
        {
            BuySlots.Remove(itemID);
            for (int i = 0; i < itemcount; i++)
                SetADDItemPrice(itemID);
            SetGoldText();
        }
    }

    public void DeleteSaleItemSlot(int itemID,int itemcount)
    {
        if (SaleSlots.ContainsKey(itemID))
        {
            SaleSlots.Remove(itemID);
            for(int i = 0; i < itemcount; i++)
                SetDeleteItemPrice(itemID);
            SetGoldText();
        }
    }

    public void ClearSlots()
    {
        if(CheckPlayerGold() == false)
        {
            UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("골드가 부족합니다.");
            return;
        }
        for(int i = 0; i < DataManager.TableDic[TableType.ItemInformation].InfoDic.Count; i++)
        {
            if (BuySlots.ContainsKey(i))
            {
                BuySlots[i].AddPlayerItem();
                Destroy(BuySlots[i].gameObject);
            }
            if (SaleSlots.ContainsKey(i))
            {
                SaleSlots[i].DeletaPlayerItem();
                Destroy(SaleSlots[i].gameObject);
            }
        }
        BuySlots.Clear();
        SaleSlots.Clear();
        //UIManager.Instance.Get<>
        ItemPrice = 0;
        SetGoldText();
    }



    public void SetADDItemPrice(int itemID)
    {
        ItemPrice += ItemDataManager.Instance.GetInt(itemID, ItemData.SALEPRICE);
        SetGoldText();
    }
    public void SetDeleteItemPrice(int itemID)
    {
        ItemPrice -= ItemDataManager.Instance.GetInt(itemID, ItemData.SALEPRICE);
        SetGoldText();
    }

    private void SetGoldText()
    {
        GetText(0).text = $"{ItemPrice}";
    }

    private void SetTopBars()
    {
        TopBar[] topBars = FindObjectsOfType<TopBar>();
        for(int i = 0; i < topBars.Length; i++)
        {
            topBars[i].Init();
        }
    }

    private bool CheckPlayerGold()
    {
        int playergold = DataManager.Instance.GetPlayer(1).PlayerInfo.Gold;
        if(ItemPrice >= 0)
        {
            playergold += ItemPrice;
            DataManager.Instance.GetPlayer(1).PlayerInfo.SetPlayerGold(playergold);
            SetTopBars();
            return true;
        }
        else
        {
            if (playergold >= Mathf.Abs(ItemPrice))
            {
                playergold += ItemPrice;
                DataManager.Instance.GetPlayer(1).PlayerInfo.SetPlayerGold(playergold);
                SetTopBars();
                return true;
            }
        }
        return false;
    }
}
