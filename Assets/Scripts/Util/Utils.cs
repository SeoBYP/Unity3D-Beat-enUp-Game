using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using Managers;

public class Utils
{
    public static T GreateObject<T>(Transform parent,bool init = false) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        obj.transform.SetParent(parent);
        T t = obj.GetComponent<T>();
        if (init)
            t.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        return t;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if(component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;
        if(recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject go,string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    #region CharacterStatSetting
    public static float ImpactPower { get; private set; } = 1;

    public static void CheckPlayerEquip()
    {
        for(int i = 0; i < DataManager.Instance.GetPlayer(1).GetPlayerCharactersCount(); i++)
        {
            int charID = DataManager.Instance.GetPlayer(1).Characters[i].ID;
            CheckCharacterItem(charID, CharacterIndex.HeadID);
            CheckCharacterItem(charID, CharacterIndex.UpperArmorID);
            CheckCharacterItem(charID, CharacterIndex.UnderArmorID);
            CheckCharacterItem(charID, CharacterIndex.ShoesID);
            CheckCharacterItem(charID, CharacterIndex.WeaponID);
            CheckCharacterItem(charID, CharacterIndex.AccessoryID);
        }
    }

    private static void CheckCharacterItem(int charID, CharacterIndex index)
    {
        int itemid = DataManager.Instance.GetPlayer(1).GetCharacterItemID(charID, index);
        if (itemid != 0)
        {
            DataManager.Instance.GetPlayer(1).GetItem(itemid).SetItemEquip(charID);
        }
    }

    public static float SetHPAmount(int charID)
    {
        float hp = DataManager.Instance.GetPlayer(1).GetCharacter(charID).HP;
        CheckCharacterItemIDHP(ref hp, charID, CharacterIndex.HeadID);
        CheckCharacterItemIDHP(ref hp, charID, CharacterIndex.UpperArmorID);
        CheckCharacterItemIDHP(ref hp, charID, CharacterIndex.UnderArmorID);
        CheckCharacterItemIDHP(ref hp, charID, CharacterIndex.ShoesID);
        CheckCharacterItemIDHP(ref hp, charID, CharacterIndex.WeaponID);
        CheckCharacterItemIDHP(ref hp, charID, CharacterIndex.AccessoryID);
        return hp;
    }

    public static void SetImpactPower()
    {
        ImpactPower += 0.005f;
        if (ImpactPower > 3)
            ImpactPower = 3f;
        
    }

    public static void ReSetImpactPower()
    {
        ImpactPower = 1;
    }

    public static float SetAttackAmount(int charID)
    {
        float attack = DataManager.Instance.GetPlayer(1).GetCharacter(charID).ATTACK;
        CheckCharacterItemIDAttack(ref attack, charID, CharacterIndex.HeadID);
        CheckCharacterItemIDAttack(ref attack, charID, CharacterIndex.UpperArmorID);
        CheckCharacterItemIDAttack(ref attack, charID, CharacterIndex.UnderArmorID);
        CheckCharacterItemIDAttack(ref attack, charID, CharacterIndex.ShoesID);
        CheckCharacterItemIDAttack(ref attack, charID, CharacterIndex.WeaponID);
        CheckCharacterItemIDAttack(ref attack, charID, CharacterIndex.AccessoryID);
        return attack * ImpactPower;
    }

    public static float SetDefenceAmount(int charID)
    {
        float defence = DataManager.Instance.GetPlayer(1).GetCharacter(charID).DEFENCE;
        CheckCharacterItemIDDefence(ref defence, charID, CharacterIndex.HeadID);
        CheckCharacterItemIDDefence(ref defence, charID, CharacterIndex.UpperArmorID);
        CheckCharacterItemIDDefence(ref defence, charID, CharacterIndex.UnderArmorID);
        CheckCharacterItemIDDefence(ref defence, charID, CharacterIndex.ShoesID);
        CheckCharacterItemIDDefence(ref defence, charID, CharacterIndex.WeaponID);
        CheckCharacterItemIDDefence(ref defence, charID, CharacterIndex.AccessoryID);
        return defence;
    }

    private static void CheckCharacterItemIDHP(ref float hp, int charID, CharacterIndex index)
    {
        int id = DataManager.Instance.GetPlayer(1).GetCharacterItemID(charID, index);
        if (id != 0)
        {
            hp = hp + (hp * (ItemDataManager.Instance.GetFloat(id, ItemData.HP) / 100));
        }
    }
    private static void CheckCharacterItemIDAttack(ref float attack, int charID, CharacterIndex index)
    {
        int id = DataManager.Instance.GetPlayer(1).GetCharacterItemID(charID, index);
        if (id != 0)
        {
            attack = attack + (attack * (ItemDataManager.Instance.GetFloat(id, ItemData.ATTACK) / 100));
        }
    }
    private static void CheckCharacterItemIDDefence(ref float defence, int charID, CharacterIndex index)
    {
        int id = DataManager.Instance.GetPlayer(1).GetCharacterItemID(charID, index);
        if (id != 0)
        {
            defence = defence + (defence * (ItemDataManager.Instance.GetFloat(id, ItemData.DEFENCE) / 100));
        }
    }
    #endregion
}

public static class Extention
{

    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvents type = Define.UIEvents.Click)
    {
        BaseUI.BindEvent(go, action, type);
    }

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }
}

public interface IItemSlot
{
    public void IInit(int itemID);
    public void ISetItemSprite(int itemID);
    public void IOnItemSlotBtnClicked(PointerEventData data);
    public bool ICheckItemType(ItemType type);
    public bool ICheckItemCharacterType(ItemCharacterType type);
    public void ISetACtive(bool state);
    public void ISetStars(bool state);
    public void IDestroy();
    public void ISetPlayerEquip(bool state);

    public void IEableButton(bool state);
}
public interface IItemShopSlot
{
    public void IInit(int itemID);
    public void ISetItemSprite(int itemID);
    public void IOnItemShopSlotBtnClicked(PointerEventData data);
    public void ISetItemShopSlotInfo(int itemID);
}
public interface IStageBtn
{
    public void IInit();
    public void ISetStageBtnInfo(bool clear, bool currently, int number = 0);
    public void SetActive(bool state);
}
