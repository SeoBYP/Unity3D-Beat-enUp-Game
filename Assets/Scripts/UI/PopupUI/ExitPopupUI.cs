using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class ExitPopupUI : PopupUI
{
    enum Buttons
    {
        BackButton,
        HomeButton,
        ExitButton,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.BackButton).gameObject, OnBackButtonClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.HomeButton).gameObject, OnHomeButtonClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitButtonClicked, Define.UIEvents.Click);
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    private void OnBackButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnHomeButtonClicked(PointerEventData data)
    {
        Managers.SceneManagerEx.Instance.LoadScene(Managers.Scene.Lobby);
    }
    private void OnExitButtonClicked(PointerEventData data)
    {
        print("Exit");
    }
    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
    }
}
