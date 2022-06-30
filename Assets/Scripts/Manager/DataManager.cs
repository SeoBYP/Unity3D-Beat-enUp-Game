using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using ItemInformation;
using CharecterInformation;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;

public class Test<T>
{
    //publicdata;
    public List<T> listData;
    public List<T> ToList() { return listData; }

    public Test(List<T> data)
    {
        this.listData = data;
    }
}

public enum TableType
{
    CharacterInformation,
    ItemInformation,
    StageInformation,
}

namespace Managers
{
    #region PlayerInfo
    [Serializable]
    public class Player
    {
        [SerializeField]
        public int PlayerID;
        [SerializeField]
        public PlayerInfo PlayerInfo;
        [SerializeField]
        public List<Item> Items = new List<Item>();
        [SerializeField]
        public List<CharacterStat> Characters = new List<CharacterStat>();

        public Player(string text)
        {
            //JArray jsonArray = JArray.Parse(text);
            //var data = JObject.Parse(jsonArray[0].ToString());
            var data = JObject.Parse(text);

            PlayerID = data["PlayerID"].ToObject<int>();
            PlayerInfo = data["PlayerInfo"].ToObject<PlayerInfo>();

            MatchCollection Itemsmatch = Regex.Matches(text, "SUBID");
            for(int i = 0; i < Itemsmatch.Count; i++)
            {
                Items.Add(new Item(data, i));
            }
            MatchCollection Charactersmatch = Regex.Matches(text, "JOB");
            for (int i = 0; i < Charactersmatch.Count; i++)
            {
                Characters.Add(new CharacterStat(data, i));
            }
        }
        #region PlayerItems
        public int GetPlayerItemsCount()
        {
            if (Items == null)
                return 0;
            return Items.Count;
        }

        public int GetPlayerItemID(int itemNumber)
        {
            return Items[itemNumber].ID;
        }

        public Item GetItem(int itemID)
        {
            foreach (Item item in Items)
            {
                if (item.ID == itemID)
                {
                    return item;
                }
            }
            return null;
        }

        public float GetItemStats(int itemID,ItemData index)
        {
            Item item = GetItem(itemID);
            if (item != null)
            {
                switch (index)
                {
                    case ItemData.ATTACK:
                        return item.ATTACK;
                    case ItemData.DEFENCE:
                        return item.DEFENCE;
                    case ItemData.CRITICAL:
                        return item.CRITICAL;
                    case ItemData.HP:
                        return item.HP;
                }
            }
            return 0;
        }

        public bool CheckPlayerItem(int itemID)
        {
            foreach (Item item in Items)
            {
                if (item.ID == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetPlayerItem(int itemID) { Items.Add(new Item(itemID)); }

        public void DeletePlayerItem(int itemID)
        {
            foreach (Item item in Items)
            {
                if (item.ID == itemID)
                {
                    Items.Remove(item);
                    return;
                }
            }
        }

        public bool CheckItemEquip(int itemID)
        {
            return GetItem(itemID).PlayerEquip;
        }

        #endregion
        #region PlayerCharacters
        public CharacterStat GetCharacter(int charID)
        {
            foreach (CharacterStat character in Characters)
            {
                if (charID == character.ID)
                {
                    return character;
                }
            }
            return null;
        }

        public bool CheckPlayerCharacter(int charID)
        {
            foreach (CharacterStat character in Characters)
            {
                if (character.ID == charID)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetExp(int charID, int exp)
        {
            GetCharacter(charID).EXP += exp;
            GetCharacter(charID).CharacterLevelUp();
        }

        public int GetPlayerCharactersCount()
        {
            if (Characters == null)
                return 0;
            return Characters.Count;
        }

        public int GetCharacterItemID(int charID,CharacterIndex index)
        {
            CharacterStat character = GetCharacter(charID);
            if (character != null)
            {
                switch (index)
                {
                    case CharacterIndex.HeadID:
                        return character.HeadID;
                    case CharacterIndex.UpperArmorID:
                        return character.UpperArmorID;
                    case CharacterIndex.UnderArmorID:
                        return character.UnderArmorID;
                    case CharacterIndex.ShoesID:
                        return character.ShoesID;
                    case CharacterIndex.WeaponID:
                        return character.WeaponID;
                    case CharacterIndex.AccessoryID:
                        return character.AccessoryID;
                }
            }
            return 0;
        }

        public void SetPlayerCharacter(int charID) { Characters.Add(new CharacterStat(charID)); }

        public void SetCharacterItemID(int charID, int itemID, CharacterIndex index)
        {
            CharacterStat character = GetCharacter(charID);
            if (character != null)
            {
                switch (index)
                {
                    case CharacterIndex.HeadID:
                        character.SetCharacterHeadItemID(itemID);
                        ItemDataManager.Instance.SetItemEquip(itemID, charID);
                        break;
                    case CharacterIndex.UpperArmorID:
                        character.SetCharacterUpperArmorItemID(itemID);
                        ItemDataManager.Instance.SetItemEquip(itemID, charID);
                        break;
                    case CharacterIndex.UnderArmorID:
                        character.SetCharacterUnderArmorItemID(itemID);
                        ItemDataManager.Instance.SetItemEquip(itemID, charID);
                        break;
                    case CharacterIndex.ShoesID:
                        character.SetCharacterShoesItemID(itemID);
                        ItemDataManager.Instance.SetItemEquip(itemID, charID);
                        break;
                    case CharacterIndex.WeaponID:
                        character.SetCharacterWeaponItemID(itemID);
                        ItemDataManager.Instance.SetItemEquip(itemID, charID);
                        break;
                    case CharacterIndex.AccessoryID:
                        character.SetCharacterAccessoryItemID(itemID);
                        ItemDataManager.Instance.SetItemEquip(itemID, charID);
                        break;
                }
            }
        }

        public void UnSetCharacterItemID(int charID, int itemID, CharacterIndex index)
        {
            CharacterStat character = GetCharacter(charID);
            if (character != null)
            {
                switch (index)
                {
                    case CharacterIndex.HeadID:
                        character.UnSetCharacterHeadItemID();
                        ItemDataManager.Instance.UnSetItemEquip(itemID);
                        break;
                    case CharacterIndex.UpperArmorID:
                        character.UnSetCharacterUpperArmorItemID();
                        ItemDataManager.Instance.UnSetItemEquip(itemID);
                        break;
                    case CharacterIndex.UnderArmorID:
                        character.UnSetCharacterUnderArmorItemID();
                        ItemDataManager.Instance.UnSetItemEquip(itemID);
                        break;
                    case CharacterIndex.ShoesID:
                        character.UnSetCharacterShoesItemID();
                        ItemDataManager.Instance.UnSetItemEquip(itemID);
                        break;
                    case CharacterIndex.WeaponID:
                        character.UnSetCharacterWeaponItemID();
                        ItemDataManager.Instance.UnSetItemEquip(itemID);
                        break;
                    case CharacterIndex.AccessoryID:
                        character.UnSetCharacterAccessoryItemID();
                        ItemDataManager.Instance.UnSetItemEquip(itemID);
                        break;
                }
            }
        }

        public bool CheckCharacterEquip(int charID, int itemID)
        {
            CharacterStat character = GetCharacter(charID);
            if (character != null)
            {
                ItemType index = ItemDataManager.Instance.GetItemType(itemID);
                switch (index)
                {
                    case ItemType.Head:
                        return character.HasCharacterHeadItem();
                    case ItemType.UpperArmor:
                        return character.HasCharacterUpperArmorItem();
                    case ItemType.UnderArmor:
                        return character.HasCharacterUnderArmorItem();
                    case ItemType.Shoes:
                        return character.HasCharacterShoesItem();
                    case ItemType.Weapon:
                        return character.HasCharacterWeaponItem();
                    case ItemType.Accessory:
                        return character.HasCharacterAccessoryItem();
                }
            }
            return false;
        }

        #endregion
    }
    [Serializable]
    public class PlayerInfo
    {
        public int ViewStage;
        public int SeleteStage;
        public int MaxStage;
        public int CurrentChater;
        public int MaxChater;
        public int ClearStage;
        public int Enegy;
        public int Gold;
        public int Gem;
        public float BGMSound;
        public float EffectSound;
        public int SeleteCharacterID;

        public int NextChapter()
        {
            ++CurrentChater;
            if (CurrentChater > MaxChater)
                CurrentChater = MaxChater;
            return CurrentChater;
        }

        public int PrevChapter()
        {
            --CurrentChater;
            if (CurrentChater < 0)
                CurrentChater = 0;
            return CurrentChater;
        }

        public void SetSeleteStage(int stage)
        {
            SeleteStage = stage;
        }
        public void AddSeleteStage() { SeleteStage++; }

        public void SetPlayerGold(int gold)
        {
            Gold = gold;
        }

        public void SetPlayerGem(int gem)
        {
            Gem = gem;
        }

        private void DeleteEnegy()
        {
            if (Enegy <= 0)
                return;

            Enegy -= 10;
        }

        public bool CheckEnegy()
        {
            DeleteEnegy();
            if (Enegy >= 10)
                return true;

            return false;
        }

        public void SetClearStage()
        {
            ClearStage += 1;
        }

        public void SetSeleteCharacterID(int charID)
        {
            SeleteCharacterID = charID;
        }

        public void SetSounds(float BGM, float Effect)
        {
            BGMSound = BGM;
            EffectSound = Effect;
        }
    }

    #endregion
    class DataManager : Manager<DataManager>
    {
        public static Dictionary<TableType, DataContents> TableDic = new Dictionary<TableType, DataContents>();
        private List<Player> PlayerData { get; set; } = new List<Player>();

        public override void Init()
        {
            Load(TableType.ItemInformation);
            Load(TableType.CharacterInformation);
            Load(TableType.StageInformation);
        }

        public Player GetPlayer(int playerID)
        {
            foreach (Player player in PlayerData)
            {
                if (player.PlayerID == playerID)
                    return player;
            }
            return null;
        }

        public void LoadPlayerInfo(string JsonString)
        {
            PlayerData.Clear();
            string path = Application.dataPath + "/PlayerInfo/" + JsonString;
            if(File.Exists(path) == false)
            {
                return;
            }
            string text = File.ReadAllText(path);
            if (text == null)
            {
                print("Json Load Is Failed");
                return;
            }
            Player player = new Player(text);
            PlayerData.Add(player);
        }

        public void Save(string JsonString)
        {
            var filePath = Application.dataPath + "/PlayerInfo/" + JsonString;
            string jdata = JsonConvert.SerializeObject(PlayerData);
            JArray jsonArray = JArray.Parse(jdata);
            var data = JObject.Parse(jsonArray[0].ToString());
            
            File.WriteAllText(filePath, data.ToString());
        }

        public void Load(TableType tableType)
        {
            string path = Application.persistentDataPath + "/Data/" + tableType.ToString();
            if (!TableDic.ContainsKey(tableType))
            {
                if (File.Exists(path))
                {
                    //Data saveBase = new Data();
                    //saveBase.LoadSaveData(path);
                    //TableDic.Add(tableType, saveBase);
                    return;
                }
                DataContents lowBase = new DataContents();
                lowBase.LoadData("Data/" + tableType.ToString());
                TableDic.Add(tableType, lowBase);
            }
        }
        public static int ToInter(TableType tableType, int tableIndex, string subject)
        {
            if (TableDic.ContainsKey(tableType))
                return TableDic[tableType].ToInter(tableIndex, subject);
            return 0;
        }

        public static float ToFloat(TableType tableType, int tableIndex, string subject)
        {
            if (TableDic.ContainsKey(tableType))
                return TableDic[tableType].Tofloat(tableIndex, subject);
            return 0;
        }

        public static string ToString(TableType tableType, int tableIndex, string subject)
        {
            if (TableDic.ContainsKey(tableType))
                return TableDic[tableType].Tostring(tableIndex, subject);
            return string.Empty;
        }
    }
}

