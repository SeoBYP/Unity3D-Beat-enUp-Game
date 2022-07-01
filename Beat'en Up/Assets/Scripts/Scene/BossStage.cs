using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Charecters;
namespace Managers
{
    public class BossStage : BaseScene
    {
        private BossEnemySpawn bossEnemySpawn;
        protected override void Init()
        {
            base.Init();
            sceneType = Scene.BossStage;
            UIManager.Instance.Add<BossStageUI>(UIList.BossStageUI);
            LoadCharacter();
            bossEnemySpawn = GetComponentInChildren<BossEnemySpawn>();
            if(bossEnemySpawn != null)
            {
                bossEnemySpawn.SpwanBossEnemy();
            }
            GameAudioManager.Instance.PlayBackGround("Music");
            GetComponentInChildren<CameraController>().Init();
        }

        private void LoadCharacter()
        {
            int seleteID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
            string name = DataManager.Instance.GetPlayer(1).GetCharacter(seleteID).NAME;
            ResourcesManager.Instance.Instantiate($"PlayerPrefab/{name}");
        }

        public override void Clear()
        {
            BaseCharecterController[] controllers = FindObjectsOfType<BaseCharecterController>();
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].Clear();
            }
            UIManager.Instance.CloseAllPopupUI();
            UIManager.Instance.CloseSceneUI(UIList.BossStageUI);
        }

        public void ClaerStage()
        {
            UIManager.Instance.ShowPopupUI<ClearStageUI>();
        }

    }
}