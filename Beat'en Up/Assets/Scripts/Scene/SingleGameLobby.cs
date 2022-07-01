using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    public class SingleGameLobby : BaseScene
    {
        private static bool loadJson = false;
        protected override void Init()
        {
            base.Init();
            sceneType = Scene.SingleGameLobby;
            if(loadJson == false)
            {
                DataManager.Instance.LoadPlayerInfo("PlayerInformation.json");
                loadJson = true;
            }
            else
            {
                DataManager.Instance.Save("PlayerInformation.json");
            }

            UIManager.Instance.Add<SingleGameLobbyUI>(UIList.SingleGameLobbyUI);
            GameAudioManager.Instance.LoadSound();
            GameAudioManager.Instance.PlayBackGround("SingleGameLobby");
        }
        public override void Clear()
        {
            UIManager.Instance.CloseAllPopupUI();
            UIManager.Instance.CloseSceneUI(UIList.SingleGameLobbyUI);
        }

       
    }
}

