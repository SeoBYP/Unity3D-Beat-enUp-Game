using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Managers;
public class TitleUI : SceneUI
{
    enum Buttons
    {
        TitleButton,
    }

    enum GameObjects
    {
        LogoBackground,
        PlayerLeftIcon,
        PlayerRightIcon,
        LevelUpBadge,
    }

    //private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    private Button _titleBtn;

    public override void Init()
    {
        CurrentUIList = Managers.UIList.TitleUI;
        base.Init();
        Bind();
        StartCoroutine(SetTitle());
        //SetLogo();
    }

    private void Bind()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        _titleBtn = GetButton(0);
        _titleBtn.gameObject.SetActive(false);
        BindEvent(_titleBtn.gameObject, OnButtonClicked, Define.UIEvents.Click);
        for (int i = 0; i < 4; i++)
        {
            GetGameObject(i).SetActive(false);
        }
    }

    private void SetLogo()
    {
        GetGameObject((int)GameObjects.LevelUpBadge).SetActive(true);
        GetGameObject((int)GameObjects.LevelUpBadge).GetComponent<Animation>().Play();
    }

    void SetBackGround()
    {
        GetGameObject((int)GameObjects.LogoBackground).SetActive(true);
        GetGameObject((int)GameObjects.PlayerLeftIcon).SetActive(true);
        GetGameObject((int)GameObjects.PlayerLeftIcon).GetComponent<Animation>().Play();
        GetGameObject((int)GameObjects.PlayerRightIcon).SetActive(true);
        GetGameObject((int)GameObjects.PlayerRightIcon).GetComponent<Animation>().Play();
    }

    IEnumerator SetTitle()
    {
        GameAudioManager.Instance.Play2DSound("Intro");
        SetLogo();
        yield return new WaitForSeconds(3.0f);
        SetBackGround();
        yield return new WaitForSeconds(1.0f);
        GameAudioManager.Instance.PlayBackGround("TitleBGM");
        _titleBtn.gameObject.SetActive(true);
    }

    private void OnButtonClicked(PointerEventData data)
    {
        Managers.SceneManagerEx.Instance.LoadScene(Managers.Scene.Lobby);
    }
}
