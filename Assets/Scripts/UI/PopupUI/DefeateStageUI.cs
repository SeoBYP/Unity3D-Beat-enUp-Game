using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

public class DefeateStageUI : PopupUI
{
    enum Buttons
    {
        ReStartBtn,
        ExitStageBtn,
    }
    List<ResultStar> resultStarList = new List<ResultStar>();
    private Scene currentScene;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.ReStartBtn).gameObject, OnRestartBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ExitStageBtn).gameObject, OnExitStageBtnClicked, Define.UIEvents.Click);
        SetResultStar();
        GameAudioManager.Instance.Play2DSound("Defeate");
    }

    public void SetScene(Scene scene) { currentScene = scene; }

    private void SetResultStar()
    {
        ResultStar[] resultStars = GetComponentsInChildren<ResultStar>();
        for (int i = 0; i < resultStars.Length; i++)
        {
            if (resultStarList.Contains(resultStars[i]) == false)
            {
                resultStarList.Add(resultStars[i]);
                resultStars[i].Init();
                resultStars[i].UnSetClearStar();
            }
        }
    }

    private void OnRestartBtnClicked(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(currentScene);
    }
    private void OnExitStageBtnClicked(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(Scene.SingleGameLobby);
    }

}
