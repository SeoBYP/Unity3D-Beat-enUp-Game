using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class Stage : MonoBehaviour
{
    private List<EnemySpawn> enemySpawns = new List<EnemySpawn>();
    private List<DeactiveObject> objectDic = new List<DeactiveObject>();

    public void Init()
    {
        GameData.SetStageData();
        FindObjects();
    }

    //private void SetStageInfo()
    //{
        
    //}

    private void FindObjects()
    {
        EnemySpawn[] enemySpawn = GetComponentsInChildren<EnemySpawn>();
        for(int i = 0; i < enemySpawn.Length; i++)
        {
            enemySpawns.Add(enemySpawn[i]);
        }

        DeactiveObject[] deactives = GetComponentsInChildren<DeactiveObject>();
        for (int i = 0; i < deactives.Length; i++)
        {
            objectDic.Add(deactives[i]);
        }
    }

    public void OpenNextStage()
    {
        for(int i = 0; i < objectDic.Count; i++)
        {
            objectDic[i].Deactive();
        }
    }

    public void SpawnEnemy()
    {
        StartCoroutine(IEnemySpawn());
    }

    IEnumerator IEnemySpawn()
    {
        int count = 0;
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            for(int j = 0; j < GameData.CurrentEnemyCount; j++)
            {
                yield return new WaitForSeconds(1);
                enemySpawns[i].SpwanEnemy();
                count++;
            }
            if (count >= GameData.CurrentEnemyCount)
                yield break;
        }
    }
}
