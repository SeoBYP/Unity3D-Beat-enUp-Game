using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using System;
using Newtonsoft.Json.Linq;

namespace CharecterInformation
{
    [Serializable]
    public class CharacterStat
    {
        public int ID { get; private set; }
        public string NAME { get; private set; }
        public string JOB { get; private set; }
        public int LEVEL { get; private set; }
        public int MAXLEVEL { get; private set; }
        public int EXP { get; set; }
        public int MAXEXP { get; private set; }
        public float HP { get; private set; }
        public float ATTACK { get; private set; }
        public float DEFENCE { get; private set; }
        public int HeadID { get; private set; }
        public int UpperArmorID { get; private set; }
        public int UnderArmorID { get; private set; }
        public int ShoesID { get; private set; }
        public int WeaponID { get; private set; }
        public int AccessoryID { get; private set; }
        public float ADDHP { get; private set; }
        public float ADDATTACK { get; private set; }
        public float ADDDEFENCE { get; private set; }
        public int ADDMAXEXP { get; private set; }

        public CharacterStat(JObject data, int index)
        {
            ID = data["Characters"][index]["ID"].ToObject<int>();
            NAME = data["Characters"][index]["NAME"].ToObject<string>();
            JOB = data["Characters"][index]["JOB"].ToObject<string>();
            LEVEL = data["Characters"][index]["LEVEL"].ToObject<int>();
            MAXLEVEL = data["Characters"][index]["MAXLEVEL"].ToObject<int>();
            EXP  = data["Characters"][index]["EXP"].ToObject<int>();
            MAXEXP = data["Characters"][index]["MAXEXP"].ToObject<int>();
            HP = data["Characters"][index]["HP"].ToObject<float>();
            ATTACK = data["Characters"][index]["ATTACK"].ToObject<float>();
            DEFENCE = data["Characters"][index]["DEFENCE"].ToObject<float>();
            HeadID = data["Characters"][index]["HeadID"].ToObject<int>();
            UpperArmorID = data["Characters"][index]["UpperArmorID"].ToObject<int>();
            UnderArmorID = data["Characters"][index]["UnderArmorID"].ToObject<int>();
            ShoesID = data["Characters"][index]["ShoesID"].ToObject<int>();
            WeaponID = data["Characters"][index]["WeaponID"].ToObject<int>();
            AccessoryID = data["Characters"][index]["AccessoryID"].ToObject<int>();
            ADDHP = data["Characters"][index]["ADDHP"].ToObject<float>();
            ADDATTACK= data["Characters"][index]["ADDATTACK"].ToObject<float>();
            ADDDEFENCE = data["Characters"][index]["ADDDEFENCE"].ToObject<float>();
            ADDMAXEXP = data["Characters"][index]["ADDMAXEXP"].ToObject<int>();
        }

        public CharacterStat(int key = 0)
        {
            DataContents itemData = DataManager.TableDic[TableType.CharacterInformation];

            ID = key;
            NAME = DataManager.ToString(TableType.CharacterInformation, key, "NAME");
            JOB = DataManager.ToString(TableType.CharacterInformation, key, "JOB");
            LEVEL = DataManager.ToInter(TableType.CharacterInformation, key, "LEVEL");
            MAXLEVEL = DataManager.ToInter(TableType.CharacterInformation, key, "MAXLEVEL");
            EXP = DataManager.ToInter(TableType.CharacterInformation, key, "EXP");
            MAXEXP = DataManager.ToInter(TableType.CharacterInformation, key, "MAXEXP");
            HP = DataManager.ToFloat(TableType.CharacterInformation, key, "HP");
            ATTACK = DataManager.ToFloat(TableType.CharacterInformation, key, "ATTACK");
            DEFENCE = DataManager.ToFloat(TableType.CharacterInformation, key, "DEFENCE");
            HeadID = DataManager.ToInter(TableType.CharacterInformation, key, "HeadID");
            UpperArmorID = DataManager.ToInter(TableType.CharacterInformation, key, "UpperArmorID");
            UnderArmorID = DataManager.ToInter(TableType.CharacterInformation, key, "UnderArmorID");
            ShoesID = DataManager.ToInter(TableType.CharacterInformation, key, "ShoesID");
            WeaponID = DataManager.ToInter(TableType.CharacterInformation, key, "WeaponID");
            AccessoryID = DataManager.ToInter(TableType.CharacterInformation, key, "AccessoryID");
            ADDHP = DataManager.ToFloat(TableType.CharacterInformation, key, "ADDHP");
            ADDATTACK = DataManager.ToFloat(TableType.CharacterInformation, key, "ADDATTACK");
            ADDDEFENCE = DataManager.ToFloat(TableType.CharacterInformation, key, "ADDDEFENCE");
            ADDMAXEXP = DataManager.ToInter(TableType.CharacterInformation, key, "ADDMAXEXP");
        }

        public Sprite JobIcon()
        {
            return UIManager.Instance.LoadJobIcon(JOB);
        }

        public Sprite CharacterIcon()
        {
            return UIManager.Instance.LoadCharacterIcon(NAME);
        }

        public Sprite CharacterPopupIcon()
        {
            return UIManager.Instance.LoadCharacterPopupIcon(NAME);
        }

        public void CharacterLevelUp()
        {
            if (EXP < MAXEXP)
                return;
            else
            {
                while(EXP > MAXEXP)
                {
                    EXP -= MAXEXP;
                    MAXEXP += ADDMAXEXP;
                    LEVEL++;
                    if (LEVEL > MAXLEVEL)
                    {
                        LEVEL = MAXLEVEL;
                    }
                    ATTACK += ADDATTACK;
                    DEFENCE += ADDDEFENCE;
                    //CRITICAL += (float)Math.Round(CRITICAL * ADDCRITICAL, 1);
                    HP += ADDHP;
                }
            }
        }

        public void EnemyLevelUP()
        {
            LEVEL++;
            if (LEVEL > MAXLEVEL)
            {
                LEVEL = MAXLEVEL;
            }
            ATTACK += ADDATTACK;
            DEFENCE += ADDDEFENCE;
            HP += ADDHP;
        }

        public void SetCharacterHeadItemID(int itemID) { HeadID = itemID; }
        public void SetCharacterUpperArmorItemID(int itemID) { UpperArmorID = itemID;}
        public void SetCharacterUnderArmorItemID(int itemID) { UnderArmorID = itemID;}
        public void SetCharacterShoesItemID(int itemID) { ShoesID = itemID; }
        public void SetCharacterWeaponItemID(int itemID) { WeaponID = itemID; }
        public void SetCharacterAccessoryItemID(int itemID) { AccessoryID = itemID; }

        public void UnSetCharacterHeadItemID() { HeadID = 0; }
        public void UnSetCharacterUpperArmorItemID() { UpperArmorID = 0; }
        public void UnSetCharacterUnderArmorItemID() { UnderArmorID = 0; }
        public void UnSetCharacterShoesItemID() { ShoesID = 0; }
        public void UnSetCharacterWeaponItemID() { WeaponID = 0; }
        public void UnSetCharacterAccessoryItemID() { AccessoryID = 0; }

        public bool HasCharacterHeadItem() { if (HeadID != 0) return true; else return false; }
        public bool HasCharacterUpperArmorItem() { if (UpperArmorID != 0) return true; else return false; }
        public bool HasCharacterUnderArmorItem() { if (UnderArmorID != 0) return true; else return false; }
        public bool HasCharacterShoesItem() { if (ShoesID != 0) return true; else return false; }
        public bool HasCharacterWeaponItem() { if (WeaponID != 0) return true; else return false; }
        public bool HasCharacterAccessoryItem() { if (AccessoryID != 0) return true; else return false; }
    }
}