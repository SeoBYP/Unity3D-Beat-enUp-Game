using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Charecters;
namespace Managers
{
    enum StageIndex
    {
        Stage1,
        Stage2,
        Stage3,
    }

    class SingleGame : BaseScene
    {
        private StageIndex CurrentStage { get; set; }
        Dictionary<StageIndex, Stage> stageDic = new Dictionary<StageIndex, Stage>();

        public UnityAction m_SpawnEnemy;
        private SingleGameUI _singleGameUI;
        private CameraController _camera;
        protected override void Init()
        {
            base.Init();
            m_SpawnEnemy += CheckStageClear;
            sceneType = Scene.SingleGame;
            CurrentStage = StageIndex.Stage1;
            LoadStage();
            stageDic[CurrentStage].SpawnEnemy();
            GameAudioManager.Instance.PlayBackGround("Music");
            UIManager.Instance.Add<SingleGameUI>(UIList.SingleGameUI);
            LoadCharacter();
            GetComponentInChildren<CameraController>().Init();
        }

        private void LoadCharacter()
        {
            int seleteID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
            string name = DataManager.Instance.GetPlayer(1).GetCharacter(seleteID).NAME;
            ResourcesManager.Instance.Instantiate($"PlayerPrefab/{name}");
        }

        void LoadStage()
        {
            Stage[] stages = GetComponentsInChildren<Stage>();
            StageIndex index = StageIndex.Stage1;
            for (int i = 0; i < stages.Length; i++)
            {
                if (stageDic.ContainsKey(index) == false)
                {
                    stageDic.Add(index, stages[i]);
                    stages[i].Init();
                    index++;
                }
            }
        }

        public void CheckStageClear()
        {
            if (GameData.CheckStageClear())
            {
                _singleGameUI = UIManager.Instance.Get<SingleGameUI>(UIList.SingleGameUI);
                _singleGameUI.SetGoNextStage();
                OpenNextStage();
            }
        }

        public void OpenNextStage()
        {
            CurrentStage++;
            if (CurrentStage.GetHashCode() >= 3)
            {
                UIManager.Instance.ShowPopupUI<ClearStageUI>();
                return;
            }
            stageDic[CurrentStage].OpenNextStage();
            stageDic[CurrentStage].Init();
            stageDic[CurrentStage].SpawnEnemy();
        }

        public override void Clear()
        {
            BaseCharecterController[] controllers = FindObjectsOfType<BaseCharecterController>();
            for(int i= 0; i < controllers.Length; i++)
            {
                controllers[i].Clear();
            }

            UIManager.Instance.CloseAllPopupUI();
            UIManager.Instance.CloseSceneUI(UIList.SingleGameUI);
        }
    }
}

