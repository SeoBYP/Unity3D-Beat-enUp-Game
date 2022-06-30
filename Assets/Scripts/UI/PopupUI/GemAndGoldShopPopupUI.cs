using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class GemAndGoldShopPopupUI : PopupUI
{
    enum Buttons
    {
        ExitButton,
        GemTab,
        GoldTab,
    }
    enum Texts
    {
        EnergyText,
        GoldText,
        GemText,
    }
    enum Images
    {
        GemTabFocus,
        GoldTabFocus,
    }
    GemShopPopup _gemShopPopup;
    GoldShopPopup _goldShopPopup;
    TopBar _topBar;
    public override void Init()
    {
        base.Init();
        Binds();
        _gemShopPopup = GetComponentInChildren<GemShopPopup>();
        if (_gemShopPopup != null)
            _gemShopPopup.Init();
        _goldShopPopup = GetComponentInChildren<GoldShopPopup>();
        if (_goldShopPopup != null)
            _goldShopPopup.Init();
        SetTopBar();
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnExitButtonClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GemTab).gameObject, OnGemTabBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GoldTab).gameObject, OnGoldTabBtnClicked, Define.UIEvents.Click);
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
        base.ClosePopupUI();
    }
    private void OnGemTabBtnClicked(PointerEventData data)
    {
        SetGemTab();
    }
    private void OnGoldTabBtnClicked(PointerEventData data)
    {
        SetGoldTab();
    }

    public void SetGemTab()
    {
        GetImage((int)Images.GemTabFocus).gameObject.SetActive(true);
        GetImage((int)Images.GoldTabFocus).gameObject.SetActive(false);
        if (_gemShopPopup != null)
            _gemShopPopup.SetActive(true);
        if (_goldShopPopup != null)
            _goldShopPopup.SetActive(false);

    }
    public void SetGoldTab()
    {
        GetImage((int)Images.GoldTabFocus).gameObject.SetActive(true);
        GetImage((int)Images.GemTabFocus).gameObject.SetActive(false);
        if (_goldShopPopup != null)
            _goldShopPopup.SetActive(true);
        if (_gemShopPopup != null)
            _gemShopPopup.SetActive(false);
    }
}
