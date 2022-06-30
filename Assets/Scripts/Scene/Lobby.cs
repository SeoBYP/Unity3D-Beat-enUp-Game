using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Lobby : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            sceneType = Scene.Lobby;
            UIManager.Instance.Add<LobbyUI>(UIList.LobbyUI);
            //UIManager.Instance.ShowSceneUI<TitleUI>();
        }

        public override void Clear()
        {
            UIManager.Instance.CloseAllPopupUI();
            UIManager.Instance.CloseSceneUI(UIList.LobbyUI);
        }
    }
}

