using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class CharacterPopupUI : PopupUI
{
    enum Buttons
    {
        ExitButton,
    }
    enum Texts
    {
        EnergyText,
        GoldText,
        GemText,
    }
    CharacterPopup _characterPopup;
    CharactersSelectPopup _charactersSelectPopup;
    CharacterStatusPopup _characterStatusPopup;
    TopBar _topBar;
    public override void Init()
    {
        base.Init();
        Binds();
        SetCharacterPopupUI();
    }

    public void SetCharacterPopupUI()
    {
        _characterPopup = GetComponentInChildren<CharacterPopup>();
        if (_characterPopup != null)
            _characterPopup.Init();
        _charactersSelectPopup = GetComponentInChildren<CharactersSelectPopup>();
        if (_charactersSelectPopup != null)
            _charactersSelectPopup.Init();
        _characterStatusPopup = GetComponentInChildren<CharacterStatusPopup>();
        if (_characterStatusPopup != null)
            _characterStatusPopup.Init();
        SetTopBar();
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    public void ReSetCharacterPopupUI()
    {
        if (_characterPopup != null)
            _characterPopup.SetCharecter();
        if (_characterStatusPopup != null)
            _characterStatusPopup.SetStatus();
        SetTopBar();
        UIManager.Instance.Get<SingleGameLobbyUI>(UIList.SingleGameLobbyUI).CheckCharacter();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitButtonClicked, Define.UIEvents.Click);
    }

    public void SetTopBar()
    {
        if (_topBar == null)
        {
            _topBar = GetComponentInChildren<TopBar>();
        }
        _topBar.Init();
    }

    private void OnExitButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }

    public override void ClosePopupUI()
    {
        InLobbyCharacterStatusInfo statusInfo = FindObjectOfType<InLobbyCharacterStatusInfo>();
        if (statusInfo != null)
            statusInfo.SetStatus();
        base.ClosePopupUI();
    }
}
