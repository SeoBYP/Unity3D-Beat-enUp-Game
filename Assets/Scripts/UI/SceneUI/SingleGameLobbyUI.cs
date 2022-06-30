using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
using Charecters;
public class SingleGameLobbyUI : SceneUI
{
    enum Buttons
    {
        SelectStageButton,
        CharacterButton,
        InventoryButton,
        ShopButton,
        SettingButton,
        ItemShopButton,
        GemShopButton,
        GoldShopButton,
        ExitButton,
    }
    enum Gameobjects
    {
        SelectShop,
        PlayerPos,
    }
    InLobbyCharacterStatusInfo _characterStatusInfo;
    TopBar _topBar;
    Transform _PlayerPos;
    InLobbyCharacter go;
    public override void Init()
    {
        CurrentUIList = Managers.UIList.SingleGameLobbyUI;
        base.Init();
        Binds();
        GetGameObject(0).SetActive(false);
        _characterStatusInfo = GetComponentInChildren<InLobbyCharacterStatusInfo>();
        if (_characterStatusInfo != null)
            _characterStatusInfo.Init();
        _PlayerPos = Get<GameObject>((int)Gameobjects.PlayerPos).transform;
        SetTopBar();
        LoadCharacter();
    }

    public void CheckCharacter()
    {
        go = _PlayerPos.GetComponentInChildren<InLobbyCharacter>();
        if (go != null)
        {
            go.Clear();
            LoadCharacter();
        }
        else
        {
            LoadCharacter();
        }
    }

    private void LoadCharacter()
    {
        int seleteID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        string name = DataManager.Instance.GetPlayer(1).GetCharacter(seleteID).NAME;
        GameObject character = ResourcesManager.Instance.Instantiate($"InLobbyChracters/{name}", _PlayerPos);
        go = character.GetComponent<InLobbyCharacter>();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(Gameobjects));
        BindEvent(GetButton((int)Buttons.SelectStageButton).gameObject, OnPlayBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CharacterButton).gameObject, OnCharacterBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.InventoryButton).gameObject, OnInventoryBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ShopButton).gameObject, OnShopBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.SettingButton).gameObject, OnSettingBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ItemShopButton).gameObject, OnItemShopBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GemShopButton).gameObject, OnGemShopBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.GoldShopButton).gameObject, OnGoldShopBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ExitButton).gameObject, OnHomeBtnClicked, Define.UIEvents.Click);
    }

    public void SetTopBar()
    {
        if(_topBar == null)
        {
            _topBar = GetComponentInChildren<TopBar>();
        }
        _topBar.Init();
    }

    private void OnPlayBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<GameStagePopupUI>();
    }
    private void OnCharacterBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<CharacterPopupUI>();
    }
    private void OnInventoryBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<InventoryPopupUI>();
    }
    private void OnShopBtnClicked(PointerEventData data)
    {
        GetGameObject(0).SetActive(true);
    }
    private void OnSettingBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<OptionSettingPopupUI>();

    }
    private void OnItemShopBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<ItemShopPopupUI>();
        GetGameObject(0).SetActive(false);
    }
    private void OnGemShopBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<GemAndGoldShopPopupUI>().SetGemTab();
        GetGameObject(0).SetActive(false);
    }
    private void OnGoldShopBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<GemAndGoldShopPopupUI>().SetGoldTab();
        GetGameObject(0).SetActive(false);
    }
    private void OnHomeBtnClicked(PointerEventData data)
    {
        Managers.UIManager.Instance.ShowPopupUI<ExitPopupUI>();
    }
}
