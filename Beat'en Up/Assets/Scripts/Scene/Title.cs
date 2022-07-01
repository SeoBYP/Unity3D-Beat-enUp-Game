using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Title : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            sceneType = Scene.Title;
            DataManager.Instance.LoadPlayerInfo("PlayerInformation.json");
            GameAudioManager.Instance.LoadSound();
            UIManager.Instance.Add<TitleUI>(UIList.TitleUI);
            //UIManager.Instance.ShowSceneUI<TitleUI>();
        }

        public override void Clear()
        {
            UIManager.Instance.CloseAllPopupUI();
            UIManager.Instance.CloseSceneUI(UIList.TitleUI);
        }
    }
}

