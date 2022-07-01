using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class LobbyUI : SceneUI
{
    enum Buttons
    {
        SingleGameBtn,
        MultyGameBtn,
        OnlineGameBtn,
        OptionSettingBtn,
        ExitBtn,
    }

    enum GameObjects
    {
        SingleGameMap,
        MultyGameMap,
        OnlineGameMap,
    }
    enum Transforms
    {
        SceneMap,
    }

    private GameObjects CurrentGameMap;

    public override void Init()
    {
        CurrentUIList = Managers.UIList.LobbyUI;
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Transform>(typeof(Transforms));

        BindEvent(GetButton((int)Buttons.SingleGameBtn).gameObject, OnSingleGameBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.MultyGameBtn).gameObject, OnMultyGameBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.OnlineGameBtn).gameObject, OnOnlineGameBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.OptionSettingBtn).gameObject, OnOptionSettingBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ExitBtn).gameObject, OnExitBtnClicked, Define.UIEvents.Click);

        DeactiveBindGameObjects();
    }

    private void DeactiveBindGameObjects()
    {
        for (int i = 0; i < 3; i++)
        {
            GetGameObject(i).SetActive(false);
        }
    }

    private void SetGameMap(GameObjects game)
    {
        DeactiveBindGameObjects();
        CurrentGameMap = game;
        print(CurrentGameMap);
        GameObject gameMap = GetGameObject((int)game);
        gameMap.SetActive(true);
        Button playgame = gameMap.GetComponentInChildren<Button>();
        BindEvent(playgame.gameObject, OnPlayGameBtnClicked, Define.UIEvents.Click);
    }

    private void OnSingleGameBtnClicked(PointerEventData data)
    {
        SetGameMap(GameObjects.SingleGameMap);
    }
    private void OnMultyGameBtnClicked(PointerEventData data)
    {
        SetGameMap(GameObjects.MultyGameMap);
    }
    private void OnOnlineGameBtnClicked(PointerEventData data)
    {
        SetGameMap(GameObjects.OnlineGameMap);
    }
    private void OnOptionSettingBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<OptionSettingPopupUI>();
    }
    private void OnExitBtnClicked(PointerEventData data)
    {
        print("OnExitBtnClicked");
    }

    private void OnPlayGameBtnClicked(PointerEventData data)
    {
        switch (CurrentGameMap)
        {
            case GameObjects.SingleGameMap:
                Managers.SceneManagerEx.Instance.LoadScene(Managers.Scene.SingleGameLobby);
                break;
            default:
                UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("차후 업데이트를\n기대해주세요~!");
                break;
        }
    }
}
