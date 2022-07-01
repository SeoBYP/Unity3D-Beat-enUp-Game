using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class OptionSettingPopupUI : PopupUI
{
    enum Buttons
    {
        ExitButton,
        SoundSettingBtn,
        SaveButton,
    }
    enum Gameobjects
    {
        SoundOption,
    }
    SoundOption _soundOption;
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(Gameobjects));

        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.SaveButton).gameObject, OnSaveBtnClicked, Define.UIEvents.Click);
        _soundOption = GetComponentInChildren<SoundOption>();
        if (_soundOption != null)
            _soundOption.Init();
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    private void OnSaveBtnClicked(PointerEventData data)
    {
        if (_soundOption != null)
            _soundOption.SaveSoundOption();
    }

    private void OnExitBtnClicked(PointerEventData data)
    {
        base.ClosePopupUI();
    }
}
