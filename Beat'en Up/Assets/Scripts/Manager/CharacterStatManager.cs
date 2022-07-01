using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharecterInformation;
namespace Managers
{
    public enum CharacterType
    {
        Knight,
        Fighter,
        Berserker,
        ALL,
        None,
    }
    public enum CharacterIndex
    {
        ID,
        NAME,
        JOB,
        LEVEL,
        MAXLEVEL,
        EXP,
        MAXEXP,
        HP,
        ATTACK,
        DEFENCE,
        HeadID,
        UpperArmorID,
        UnderArmorID,
        ShoesID,
        WeaponID,
        AccessoryID,
        ADDHP,
        ADDATTACK,
        ADDDEFENCE,
        ADDMAXEXP
    }
    class CharacterStatManager : Manager<CharacterStatManager>
    {
        Dictionary<int, CharacterStat> Characters = new Dictionary<int, CharacterStat>();

        public override void Init()
        {
            BulidCharacterStat();
        }

        void BulidCharacterStat()
        {
            if (DataManager.TableDic.ContainsKey(TableType.CharacterInformation))
            {
                for(int i = 0; i <= DataManager.TableDic[TableType.CharacterInformation].InfoDic.Count; i++)
                {
                    DataContents characterData = DataManager.TableDic[TableType.CharacterInformation];
                    if (characterData.InfoDic.ContainsKey(i))
                        Characters.Add(i, new CharacterStat(i));
                }
            }
        }
        public bool CheckContains(int charID) { return Characters.ContainsKey(charID); }

        public Sprite GetJobIcon(int charID)
        {
            if (Characters.ContainsKey(charID))
            {
                return Characters[charID].JobIcon();
            }
            return null;
        }

        public Sprite GetCharacterIcon(int charID)
        {
            if (Characters.ContainsKey(charID))
            {
                return Characters[charID].CharacterIcon();
            }
            return null;
        }

        public Sprite GetCharacterPopupIcon(int charID)
        {
            if (Characters.ContainsKey(charID))
            {
                return Characters[charID].CharacterPopupIcon();
            }
            return null;
        }

        public string GetString(int charID, CharacterIndex index)
        {
            if (Characters.ContainsKey(charID))
            {
                switch (index)
                {
                    case CharacterIndex.NAME:
                        return Characters[charID].NAME;
                    case CharacterIndex.JOB:
                        return Characters[charID].JOB;
                }
            }
            return string.Empty;
        }
        public int GetInt(int charID, CharacterIndex index)
        {
            if (Characters.ContainsKey(charID))
            {
                switch (index)
                {
                    case CharacterIndex.LEVEL:
                        return Characters[charID].LEVEL;
                    case CharacterIndex.MAXLEVEL:
                        return Characters[charID].MAXLEVEL;
                    case CharacterIndex.EXP:
                        return Characters[charID].EXP;
                    case CharacterIndex.MAXEXP:
                        return Characters[charID].MAXEXP;
                    case CharacterIndex.HeadID:
                        return Characters[charID].HeadID;
                    case CharacterIndex.UpperArmorID:
                        return Characters[charID].UpperArmorID;
                    case CharacterIndex.UnderArmorID:
                        return Characters[charID].UnderArmorID;
                    case CharacterIndex.ShoesID:
                        return Characters[charID].ShoesID;
                    case CharacterIndex.WeaponID:
                        return Characters[charID].WeaponID;
                    case CharacterIndex.AccessoryID:
                        return Characters[charID].AccessoryID;
                    case CharacterIndex.ADDMAXEXP:
                        return Characters[charID].ADDMAXEXP;
                }
            }
            return 0;
        }
        public float GetFloat(int charID, CharacterIndex index)
        {
            if (Characters.ContainsKey(charID))
            {
                switch (index)
                {
                    case CharacterIndex.HP:
                        return Characters[charID].HP;
                    case CharacterIndex.ATTACK:
                        return Characters[charID].ATTACK;
                    case CharacterIndex.DEFENCE:
                        return Characters[charID].DEFENCE;
                    case CharacterIndex.ADDHP:
                        return Characters[charID].ADDHP;
                    case CharacterIndex.ADDATTACK:
                        return Characters[charID].ADDATTACK;
                    case CharacterIndex.ADDDEFENCE:
                        return Characters[charID].ADDDEFENCE;
                }
            }
            return 0;
        }

        public void SetEnemyLevelUp(int charID)
        {
            if (Characters.ContainsKey(charID))
            {
                if(Characters[charID].JOB == "Enemy" || Characters[charID].JOB == "BossEnemy")
                {
                    Characters[charID].EnemyLevelUP();
                }
            }
        }

        public bool CheckChatacterJobWithItemID(int charID,int itemID)
        {
            if (Characters.ContainsKey(charID))
            {
                string job = ItemDataManager.Instance.GetString(itemID, ItemData.CHARACTERTYPE);
                if(job == Characters[charID].JOB || job == "ALL")
                {
                    return true;
                }
            }
            return false;
        }
    }
}