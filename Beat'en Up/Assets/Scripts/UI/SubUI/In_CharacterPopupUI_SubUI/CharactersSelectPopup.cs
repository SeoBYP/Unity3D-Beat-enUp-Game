using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class CharactersSelectPopup : BaseUI
{
    enum GameObjects
    {
        CharacterList,
    }

    Dictionary<int,CharacterSlot> CharacterSlotDic = new Dictionary<int, CharacterSlot>();
    ScrollRect _scrollRect;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _scrollRect = GetComponentInChildren<ScrollRect>();
        SetPlayerCharacterSlot();
    }

    private void SetPlayerCharacterSlot()
    {
        int count = DataManager.Instance.GetPlayer(1).GetPlayerCharactersCount();
        for (int i = 0; i < count; i++)
        {
            int charid = DataManager.Instance.GetPlayer(1).Characters[i].ID;
            int selete = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
            if (CharacterSlotDic.ContainsKey(i) == false)
            {
                CharacterSlot slot = CreateCharacterSlot();
                if (slot != null)
                {
                    slot.SetIcon(charid);
                    CharacterSlotDic.Add(i, slot);
                }
                if (charid == selete)
                {
                    CharacterSlotDic[i].SetFocus(true);
                }
            }
        }
    }

    public void ReSetSlotDicFocus()
    {
        for(int i = 0; i < CharacterSlotDic.Count; i++)
        {
            if (CharacterSlotDic.ContainsKey(i))
            {
                CharacterSlotDic[i].SetFocus(false);
            }
        }
    }

    private CharacterSlot CreateCharacterSlot()
    {
        return UIManager.Instance.SubUILoad<CharacterSlot>(SubUIList.CharacterSlot,GetGameObject(0).transform);
    }

    //private void SetContentSize()
    //{
    //    if (_scrollRect != null)
    //    {
    //        float height = _scrollRect.content.sizeDelta.y + 200;
    //        float width = _scrollRect.content.sizeDelta.x;
    //        _scrollRect.content.sizeDelta = new Vector3(width, height);
    //    }
    //}
}
