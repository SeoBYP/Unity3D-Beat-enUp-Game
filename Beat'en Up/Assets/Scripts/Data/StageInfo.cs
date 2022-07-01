using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class StageInfo
{
    public int STAGEID { get; private set; }
    public int STAGEEXP { get; private set; }
    public int ENEMYCOUNT { get; private set; }
    public int CLEARCOUNT { get; private set; }

    public StageInfo(int key = 0)
    {
        STAGEID = key;
        STAGEEXP = DataManager.ToInter(TableType.StageInformation, key, "STAGEEXP");
        ENEMYCOUNT = DataManager.ToInter(TableType.StageInformation, key, "ENEMYCOUNT");
        CLEARCOUNT = DataManager.ToInter(TableType.StageInformation, key, "CLEARCOUNT");
    }

    public void SetClearCount() { CLEARCOUNT++; }
}

namespace Managers
{
    class StageInfoManager : Manager<StageInfoManager>
    {
        private Dictionary<int,StageInfo> stages = new Dictionary<int, StageInfo>();

        public override void Init()
        {
            BuildStageInfoBase();
        }

        void BuildStageInfoBase()
        {
            if (DataManager.TableDic.ContainsKey(TableType.StageInformation))
            {
                for(int i = 0; i < DataManager.TableDic[TableType.ItemInformation].InfoDic.Count; i++)
                {
                    DataContents StageData = DataManager.TableDic[TableType.StageInformation];
                    if (StageData.InfoDic.ContainsKey(i))
                    {
                        stages.Add(i,new StageInfo(i));
                    }
                }
            }
        }

        private bool CheckConstainStage(int stageID) { return stages.ContainsKey(stageID); }

        public int GetStageEXP(int stageID)
        {
            if (CheckConstainStage(stageID))
            {
                return stages[stageID].STAGEEXP;
            }
            return 0;
        }

        public int GetStageEnemyCount(int stageID)
        {
            if (CheckConstainStage(stageID))
            {
                return stages[stageID].ENEMYCOUNT;
            }
            return 0;
        }

        public bool CheckEnemyLevelUp(int stageID)
        {
            if (CheckConstainStage(stageID))
            {
                if(stages[stageID].CLEARCOUNT % 2 == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetStageClearCount(int stageID)
        {
            if (CheckConstainStage(stageID))
            {
                stages[stageID].SetClearCount();
            }
        }

    }
}
