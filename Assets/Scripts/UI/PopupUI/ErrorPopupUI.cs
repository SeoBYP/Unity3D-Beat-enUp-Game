using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class ErrorPopupUI : PopupUI
{
    enum Texts
    {
        PopupText,
    }
    enum Buttons
    {
        BackGround,
    }
    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton(0).gameObject, OnBtnClicked, Define.UIEvents.Click);
    }

    public void SetText(string error)
    {
        GameAudioManager.Instance.Play2DSound("ErrorMessage");
        GetText(0).text = error;
    }

    private void OnBtnClicked(PointerEventData data)
    {
        base.ClosePopupUI();
    }
}
