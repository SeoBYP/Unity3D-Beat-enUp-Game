using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using System;
using Newtonsoft.Json.Linq;
namespace ItemInformation
{
    [Serializable]
    public class Item
    {
        public int ID { get; private set;}
        public int SUBID { get; private set; }
        public string NAME { get; private set; }
        public string TYPE { get; private set; }
        public float ATTACK { get; private set; }
        public float DEFENCE { get; private set; }
        public float CRITICAL { get; private set; }
        public float HP { get; private set; }
        public string CHARACTERTYPE { get; private set; }
        public int LEVEL { get; private set; }
        public string RARITY { get; private set; }
        public float ADDATTACK { get; private set; }
        public float ADDDEFENCE { get; private set; }
        public float ADDCRITICAL { get; private set; }
        public float ADDHP { get; private set; }
        public int MAXLEVEL { get; private set; }
        public int REINFORGOLD { get; private set; }
        public int PRICE { get; private set; }
        public int SALEPRICE { get; private set; }
        public float ADDPRICE { get; private set; }
        public int CharacterID { get; private set; }
        public bool PlayerEquip { get; private set; }

        public Item(JObject data,int index)
        {
            ID = data["Items"][index]["ID"].ToObject<int>();
            SUBID = data["Items"][index]["SUBID"].ToObject<int>();
            NAME = data["Items"][index]["NAME"].ToObject<string>();
            TYPE = data["Items"][index]["TYPE"].ToObject<string>();
            ATTACK = data["Items"][index]["ATTACK"].ToObject<float>();
            DEFENCE = data["Items"][index]["DEFENCE"].ToObject<float>();
            CRITICAL = data["Items"][index]["CRITICAL"].ToObject<float>();
            HP = data["Items"][index]["HP"].ToObject<float>();
            CHARACTERTYPE = data["Items"][index]["CHARACTERTYPE"].ToObject<string>();
            LEVEL = data["Items"][index]["LEVEL"].ToObject<int>();
            RARITY = data["Items"][index]["RARITY"].ToObject<string>();
            ADDATTACK = data["Items"][index]["ADDATTACK"].ToObject<float>();
            ADDDEFENCE = data["Items"][index]["ADDDEFENCE"].ToObject<float>();
            ADDCRITICAL = data["Items"][index]["ADDCRITICAL"].ToObject<float>();
            ADDHP = data["Items"][index]["ADDHP"].ToObject<float>();
            MAXLEVEL = data["Items"][index]["MAXLEVEL"].ToObject<int>();
            REINFORGOLD = data["Items"][index]["REINFORGOLD"].ToObject<int>();
            PRICE = data["Items"][index]["PRICE"].ToObject<int>();
            SALEPRICE = data["Items"][index]["SALEPRICE"].ToObject<int>();
            ADDPRICE = data["Items"][index]["ADDPRICE"].ToObject<float>();
            CharacterID = data["Items"][index]["CharacterID"].ToObject<int>();
            PlayerEquip = data["Items"][index]["PlayerEquip"].ToObject<bool>();
        }

        public Item(int key)
        {            
            DataContents itemData = DataManager.TableDic[TableType.ItemInformation];
            ID = key;
            SUBID = DataManager.ToInter(TableType.ItemInformation, key, "SUBID");
            NAME = DataManager.ToString(TableType.ItemInformation, key, "NAME");
            TYPE = DataManager.ToString(TableType.ItemInformation, key, "TYPE");
            ATTACK = DataManager.ToFloat(TableType.ItemInformation, key, "ATTACK");
            DEFENCE = DataManager.ToFloat(TableType.ItemInformation, key, "DEFENCE");
            CRITICAL = DataManager.ToFloat(TableType.ItemInformation, key, "CRITICAL");
            HP = DataManager.ToFloat(TableType.ItemInformation, key, "HP");
            CHARACTERTYPE = DataManager.ToString(TableType.ItemInformation, key, "CHARACTERTYPE");
            LEVEL = DataManager.ToInter(TableType.ItemInformation, key, "LEVEL");
            RARITY = DataManager.ToString(TableType.ItemInformation, key, "RARITY");
            ADDATTACK = DataManager.ToFloat(TableType.ItemInformation, key, "ADDATTACK");
            ADDDEFENCE = DataManager.ToFloat(TableType.ItemInformation, key, "ADDDEFENCE");
            ADDCRITICAL = DataManager.ToFloat(TableType.ItemInformation, key, "ADDCRITICAL");
            ADDHP = DataManager.ToFloat(TableType.ItemInformation, key, "ADDHP");
            MAXLEVEL = DataManager.ToInter(TableType.ItemInformation, key, "MAXLEVEL");
            REINFORGOLD = DataManager.ToInter(TableType.ItemInformation, key, "REINFORGOLD");
            PRICE = DataManager.ToInter(TableType.ItemInformation, key, "PRICE");
            SALEPRICE = DataManager.ToInter(TableType.ItemInformation, key, "SALEPRICE");
            ADDPRICE = DataManager.ToFloat(TableType.ItemInformation, key, "ADDPRICE");
            CharacterID = DataManager.ToInter(TableType.ItemInformation, key, "CharacterID");
            PlayerEquip = CheckPlayerEquip(DataManager.ToString(TableType.ItemInformation, key, "PlayerEquip"));
        }

        public Sprite Icon()
        {
            return UIManager.Instance.LoadItemIcon(NAME);
        }

        public Sprite ItemTypeIcon()
        {
            return UIManager.Instance.LoadItemtypeIcon(TYPE);
        }

        public void ItemLevelUP()
        {
            LEVEL++;
            if (LEVEL > MAXLEVEL)
            {
                LEVEL = MAXLEVEL;
                return;
            }
            ATTACK += (float)Math.Round(ATTACK * ADDATTACK,1);
            DEFENCE += (float)Math.Round(DEFENCE * ADDDEFENCE, 1);
            CRITICAL += (float)Math.Round(CRITICAL * ADDCRITICAL,1);
            HP += (float)Math.Round(HP * ADDHP,1);
            REINFORGOLD = (int)Mathf.Round(REINFORGOLD * 1.5f);
            SALEPRICE += (int)Mathf.Round(SALEPRICE * ADDPRICE);
        }

        public void SetItemEquip(int charID)
        {
            CharacterID = charID;
            PlayerEquip = true;
        }

        public void UnSetItemEquip()
        {
            CharacterID = 0;
            PlayerEquip = false;
        }

        public bool CheckPlayerEquip(string index)
        {
            if (index == "FALSE" || index == "false")
                return false;
            else
                return true;
        }
    }
}


