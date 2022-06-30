using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class CharacterItemSlot : BaseUI
{
    enum Images
    {
        BackGround,
        //Frame,
    }
    enum Transfroms
    {
        ItemSlotPos,
    }
    Button button;
    //CharacterPopup _characterPopup;
    CharacterEquipmentPopupUI popupUI;

    ItemType currItemSlotType;
    CharacterIndex currcharacterIndex;
    Transform itemslotpos;
    IItemSlot _CurritemSlot;
    public override void Init()
    {
        Binds();
    }

    private void Binds()
    {
        Bind<Image>(typeof(Images));
        Bind<Transform>(typeof(Transfroms));
        itemslotpos = Get<Transform>(0);
        button = GetComponent<Button>();
        if (button != null)
            BindEvent(button.gameObject, OnItemSlotClicked, Define.UIEvents.Click);
    }

    public void SetSlotItemType(ItemType type,CharacterIndex index)
    {
        currItemSlotType = type;
        currcharacterIndex = index;
    }

    public void SetItem(int itemID,int charID)
    {
        if(currItemSlotType == ItemDataManager.Instance.GetItemType(itemID))
        {
            if (CharacterStatManager.Instance.GetInt(charID, currcharacterIndex) == 0)
            {
                DeactiveImages();

                IItemSlot item = UIManager.Instance.LoadItemSlot(itemslotpos, itemID);
                item.ISetStars(false);
                item.IEableButton(false);
                DataManager.Instance.GetPlayer(1).SetCharacterItemID(charID, itemID, currcharacterIndex);
            }
        }
    }

    public void UnSetItem(int itemID,int charID)
    {
        if(currItemSlotType == ItemDataManager.Instance.GetItemType(itemID))
        {
            if (CharacterStatManager.Instance.GetInt(charID, currcharacterIndex) != 0)
            {
                ActiveImages();

                itemslotpos.GetComponentInChildren<IItemSlot>().IDestroy();
                DataManager.Instance.GetPlayer(1).UnSetCharacterItemID(charID, itemID, currcharacterIndex);
            }
        }
    }

    public void CheckCharacterItemID(int charID)
    {
        int itemid = DataManager.Instance.GetPlayer(1).GetCharacterItemID(charID, currcharacterIndex);
        if (itemid != 0)
        {
            _CurritemSlot = UIManager.Instance.LoadItemSlot(itemslotpos, itemid);
            if(_CurritemSlot != null)
            {
                _CurritemSlot.ISetStars(false);
            }
            DeactiveImages();
        }
        else if(itemid <= 0)
        {
            if (_CurritemSlot != null)
            {
                _CurritemSlot.ISetACtive(false);
                ActiveImages();
            }
        }
    }


    private void ActiveImages()
    {
        for (int i = 0; i < 1; i++)
        {
            GetImage(i).gameObject.SetActive(true);
        }
    }

    private void DeactiveImages()
    {
        for (int i = 0; i < 1; i++)
        {
            GetImage(i).gameObject.SetActive(false);
        }
    }

    private void OnItemSlotClicked(PointerEventData data)
    {
        if(popupUI == null)
        {
            OpenharacterEquipmentPopup();
        }
    }

    public void OpenharacterEquipmentPopup()
    {
        popupUI = UIManager.Instance.ShowPopupUI<CharacterEquipmentPopupUI>();
    }
}
