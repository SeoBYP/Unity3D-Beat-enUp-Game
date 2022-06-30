using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class ItemShopPopup : BaseUI
{
    enum Transforms
    {
        ItemShopSlotGroup,
    }
    Transform _itemShopSlotGroup;
    ScrollRect _scrollRect;
    public override void Init()
    {
        Bind<Transform>(typeof(Transforms));
        _itemShopSlotGroup = Get<Transform>(0);
        _scrollRect = GetComponentInChildren<ScrollRect>();
        SetItemShopSlot();
    }

    private void SetItemShopSlot()
    {
        for(int i = 0; i < DataManager.TableDic[TableType.ItemInformation].InfoDic.Count; i++)
        {
            if(ItemDataManager.Instance.GetString(i,ItemData.RARITY) == "Unique")
            {
                UIManager.Instance.LoadItemShopSlot(_itemShopSlotGroup, i);
                if(i % 5 == 0)
                {
                    SetContentSize();
                }
            }
                
        }
    }

    private void SetContentSize()
    {
        if(_scrollRect != null)
        {
            float height = _scrollRect.content.sizeDelta.y + 200;
            float width = _scrollRect.content.sizeDelta.x;
            _scrollRect.content.sizeDelta = new Vector3(width, height);
        }
    }
}
