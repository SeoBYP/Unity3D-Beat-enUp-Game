using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class ItemInformationPopup : BaseUI
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
        GoldText,
    }
    enum Images
    {
        TypeIcon,
    }
    enum Buttons
    {
        ReinforceButton,
    }

    private int currentItemID;
    Transform _itemSlot;

    public override void Init()
    {
        Bind<Transform>(typeof(Transforms));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        SetActive(false);
        _itemSlot = Get<Transform>(0);
        BindEvent(GetButton(0).gameObject, OnReinforceBtnClicked, Define.UIEvents.Click);
    }

    public void SetItemInformation(int itemtableid = 0)
    {
        UIManager.Instance.LoadItemSlot(_itemSlot,itemtableid);
        ItemInformation(itemtableid);
    }

    private void ItemInformation(int itemID)
    {
        currentItemID = itemID;
        if(ItemType.None == ItemDataManager.Instance.GetItemType(itemID))
        {
            GetText((int)Texts.ItemName).text = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).NAME;
            GetText((int)Texts.TypeText).text = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).TYPE;
            GetText((int)Texts.RarityText).text = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).RARITY;
            GetText((int)Texts.GoldText).text = $"GOLD : {DataManager.Instance.GetPlayer(1).GetItem(currentItemID).REINFORGOLD}";
            GetImage((int)Images.TypeIcon).sprite = ItemDataManager.Instance.GetItemTypeIconSprite(itemID);
            SetNoneItemStat();
            return;
        }

        GetText((int)Texts.ItemName).text = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).NAME;
        GetText((int)Texts.TypeText).text = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).TYPE;
        GetText((int)Texts.RarityText).text = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).RARITY;
        GetText((int)Texts.GoldText).text = $"GOLD : {DataManager.Instance.GetPlayer(1).GetItem(currentItemID).REINFORGOLD}";
        GetImage((int)Images.TypeIcon).sprite = ItemDataManager.Instance.GetItemTypeIconSprite(itemID);

        SetItemStatus(itemID);
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
        float statindex = DataManager.Instance.GetPlayer(1).GetItemStats(id,index);
        string stattext = string.Empty;
        if(statindex == 0)
            return;
        else if(statindex < 5 && statindex > 0)
        {
            stattext += $"{index} : + {statindex}%\n";
            GetText((int)Texts.NormalStatText).text += stattext;
        }
        else if(statindex >= 5 && statindex <= 10)
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

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }

    private void SetTopBars()
    {
        TopBar[] topBars = FindObjectsOfType<TopBar>();
        for (int i = 0; i < topBars.Length; i++)
        {
            topBars[i].Init();
        }
    }

    private void OnReinforceBtnClicked(PointerEventData data)
    {
        int itemgold = DataManager.Instance.GetPlayer(1).GetItem(currentItemID).REINFORGOLD;
        int playergold = DataManager.Instance.GetPlayer(1).PlayerInfo.Gold;
        if(ItemType.None == ItemDataManager.Instance.GetItemType(currentItemID))
        {
            UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("강화할 수 없는 아이템입니다.");
            return;
        }
        if (itemgold <= playergold)
        {
            DataManager.Instance.GetPlayer(1).GetItem(currentItemID).ItemLevelUP();
            ItemInformation(currentItemID);
            playergold -= itemgold;
            DataManager.Instance.GetPlayer(1).PlayerInfo.SetPlayerGold(playergold);
            SetTopBars();
        }
        else
            UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("골드가 부족합니다.");
    }
}
