using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Charecters;
public class EnemySpawn : MonoBehaviour
{
    List<EnemyController> enemydic = new List<EnemyController>();

    private int EnemyID;
    public void SpwanEnemy()
    {
        int index = Random.Range(-1, 1);
        Vector3 randomPos = transform.position + new Vector3(index, 0, 0);

        GameObject go = ResourcesManager.Instance.Instantiate("EnemyPrefab/" + LoadEnemy());
        if(go != null)
        {
            EnemyController enemy = go.GetComponent<EnemyController>();
            if (enemy != null)
            {
                if (CheckStageCleared())
                {
                    CharacterStatManager.Instance.SetEnemyLevelUp(EnemyID);
                }
                enemy.SetStat(EnemyID);
                enemydic.Add(enemy);
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
        while (true)
        {
            EnemyID = Random.Range(1, 10);
            if(CharacterStatManager.Instance.GetString(EnemyID,CharacterIndex.JOB) == "Enemy")
            {
                return CharacterStatManager.Instance.GetString(EnemyID, CharacterIndex.NAME);
            }
        }
    }
}

public class GameData
{
    public static int CurrentEnemyCount = 0;

    public static void SetStageData()
    {
        int stageid = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteStage;
        CurrentEnemyCount = StageInfoManager.Instance.GetStageEnemyCount(stageid);
        //StageEnemyCount = StageInfoManager.Instance.GetStageEnemyCount(stageID);
        //CurrentEnemyCount = StageInfoManager.Instance.GetStageEnemyCount(stageID);
    }

    public static bool CheckStageClear()
    {
        if (CurrentEnemyCount <= 0)
        {
            return true;
        }
        return false;
    }
}
