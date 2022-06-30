using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Charecters;
public class BossEnemySpawn : MonoBehaviour
{
    private int EnemyID;

    public void SpwanBossEnemy()
    {
        int index = Random.Range(-1, 1);
        Vector3 randomPos = transform.position + new Vector3(index, 0, 0);

        GameObject go = ResourcesManager.Instance.Instantiate("EnemyPrefab/" + LoadEnemy());
        if (go != null)
        {
            BossEnemyController enemy = go.GetComponent<BossEnemyController>();
            if (enemy != null)
            {
                string name = CharacterStatManager.Instance.GetString(EnemyID, CharacterIndex.NAME);
                UIManager.Instance.Get<BossStageUI>(UIList.BossStageUI).StartBossStageAni(name);
                if (CheckStageCleared())
                {
                    CharacterStatManager.Instance.SetEnemyLevelUp(EnemyID);
                }
                enemy.SetStat(EnemyID);
            }
        }
        go.transform.position = transform.position;
    }

    private bool CheckStageCleared()
    {
        int seleteStage = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteStage;
        if (StageInfoManager.Instance.CheckEnemyLevelUp(seleteStage))
        {
            return true;
        }
        return false;
    }

    private string LoadEnemy()
    {
        for(int i = 0; i <= DataManager.TableDic[TableType.CharacterInformation].InfoDic.Count; i++)
        {
            if (CharacterStatManager.Instance.GetString(i, CharacterIndex.JOB) == "BossEnemy")
            {
                EnemyID = i;
                return CharacterStatManager.Instance.GetString(EnemyID, CharacterIndex.NAME);
            }
        }
        return null;
    }
}
