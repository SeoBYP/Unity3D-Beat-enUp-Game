using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInformation;
namespace Managers
{
    public enum ItemType
    {
        Weapon,
        UpperArmor,
        UnderArmor,
        Head,
        Shoes,
        Accessory,
        None,
    }

    public enum ItemCharacterType
    {
        Knight,
        Fighter,
        Berserker,
        ALL,
        None,
    }

    public enum ItemData
    {
        SUBID,
        NAME,
        TYPE,
        ATTACK,
        DEFENCE,
        CRITICAL,
        HP,
        CHARACTERTYPE,
        LEVEL,
        RARITY,
        ADDATTACK,
        ADDDEFENCE,
        ADDCRITICAL,
        ADDHP,
        MAXLEVEL,
        REINFORGOLD,
        PRICE,
        SALEPRICE,
        ADDPRICE,
        CharacterID,
        PlayerEquip,
    }
    class ItemDataManager : Manager<ItemDataManager>
    {
        Dictionary<int, Item> Items = new Dictionary<int, Item>();

        public override void Init()
        {
            BuildItemDataBase();
        }

        void BuildItemDataBase()
        {
            if (DataManager.TableDic.ContainsKey(TableType.ItemInformation))
            {
                for (int i = 0; i < DataManager.TableDic[TableType.ItemInformation].InfoDic.Count; i++)
                {
                    DataContents itemData = DataManager.TableDic[TableType.ItemInformation];
                    if (itemData.InfoDic.ContainsKey(i))
                    {
                        Items.Add(i,new Item(i));
                    }
                }
            }
        }

        public bool CheckContains(int itemID) { return Items.ContainsKey(itemID); }

        public Sprite GetItemIconSprite(int itemID)
        {
            if (Items.ContainsKey(itemID))
            {
                return Items[itemID].Icon();
            }
            return null;
        }
        public Sprite GetItemTypeIconSprite(int itemID)
        {
            if (Items.ContainsKey(itemID))
            {
                return Items[itemID].ItemTypeIcon();
            }
            return null;
        }

        public string GetString(int itemID, ItemData index)
        {
            if (Items.ContainsKey(itemID))
            {
                switch (index)
                {
                    case ItemData.NAME:
                        return Items[itemID].NAME;
                    case ItemData.TYPE:
                        return Items[itemID].TYPE;
                    case ItemData.CHARACTERTYPE:
                        return Items[itemID].CHARACTERTYPE;
                    case ItemData.RARITY:
                        return Items[itemID].RARITY;
                }
            }
            return null;
        }
        public int GetInt(int itemID, ItemData index)
        {
            if (Items.ContainsKey(itemID))
            {
                switch (index)
                {
                    case ItemData.SUBID:
                        return Items[itemID].SUBID;
                    case ItemData.LEVEL:
                        return Items[itemID].LEVEL;
                    case ItemData.MAXLEVEL:
                        return Items[itemID].MAXLEVEL;
                    case ItemData.REINFORGOLD:
                        return Items[itemID].REINFORGOLD;
                    case ItemData.PRICE:
                        return Items[itemID].PRICE;
                    case ItemData.SALEPRICE:
                        return Items[itemID].SALEPRICE;
                    case ItemData.CharacterID:
                        return Items[itemID].CharacterID;
                }
            }
            return 0;
        }

        public float GetFloat(int itemID, ItemData index)
        {
            if (Items.ContainsKey(itemID))
            {
                switch (index)
                {
                    case ItemData.ATTACK:
                        return Items[itemID].ATTACK;
                    case ItemData.DEFENCE:
                        return Items[itemID].DEFENCE;
                    case ItemData.CRITICAL:
                        return Items[itemID].CRITICAL;
                    case ItemData.HP:
                        return Items[itemID].HP;
                    case ItemData.ADDATTACK:
                        return Items[itemID].ADDATTACK;
                    case ItemData.ADDDEFENCE:
                        return Items[itemID].ADDDEFENCE;
                    case ItemData.ADDCRITICAL:
                        return Items[itemID].ADDCRITICAL;
                    case ItemData.ADDHP:
                        return Items[itemID].ADDHP;
                    case ItemData.ADDPRICE:
                        return Items[itemID].ADDPRICE;
                }
            }
            return 0;
        }

        public bool GetPlayerEquip(int itemID)
        {
            return Items[itemID].PlayerEquip;
        }

        public void SetItemEquip(int itemID,int charID)
        {
            if (Items.ContainsKey(itemID))
            {
                Items[itemID].SetItemEquip(charID);
            }
        }

        public void UnSetItemEquip(int itemID)
        {
            if (Items.ContainsKey(itemID))
            {
                Items[itemID].UnSetItemEquip();
            }
        }

        public void ItemLevelUP(int itemID)
        {
            if (Items.ContainsKey(itemID))
            {
                Items[itemID].ItemLevelUP();
            }
        }

        public ItemType GetItemType(int itemID)
        {
            string type = string.Empty;
            if (Items.ContainsKey(itemID))
            {
                type = Items[itemID].TYPE;
            }
            switch (type)
            {
                case "Weapon":
                    return ItemType.Weapon;
                case "UpperArmor":
                    return ItemType.UpperArmor;
                case "UnderArmor":
                    return ItemType.UnderArmor;
                case "Head":
                    return ItemType.Head;
                case "Shoes":
                    return ItemType.Shoes;
                case "Accessory":
                    return ItemType.Accessory;
            }
            return ItemType.None;
        }

        public bool CheckItemType(int itemID,ItemType itemType)
        {
            if (Items.ContainsKey(itemID))
            {
                if (Items[itemID].TYPE == itemType.ToString())
                    return true;
            }
            return false;
        }

        public ItemCharacterType SetItemCharacterType(int itemID)
        {
            string type = string.Empty;
            if (Items.ContainsKey(itemID))
            {
                type = Items[itemID].CHARACTERTYPE;
            }
            switch (type)
            {
                case "Knight":
                    return ItemCharacterType.Knight;
                case "Fighter":
                    return ItemCharacterType.Fighter;
                case "Berserker":
                    return ItemCharacterType.Berserker;
                case "ALL":
                    return ItemCharacterType.ALL;
            }
            return ItemCharacterType.None;
        }

        public bool CheckItemCharacterType(int itemID,ItemCharacterType type)
        {
            if (Items.ContainsKey(itemID))
            {
                if (Items[itemID].TYPE == type.ToString())
                    return true;
            }
            return false;
        }

    }
}