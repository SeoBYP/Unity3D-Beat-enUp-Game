using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class GameStagePopupUI : PopupUI
{
    enum Buttons
    {
        NextStageButton,
        PrevStageButton,
        CloseButton,
    }
    enum Texts
    {
        ChapterText,
    }

    List<IStageBtn> stageButtonList = new List<IStageBtn>();
    BossStageButton bossStageBtn;

    public override void Init()
    {
        base.Init();
        Binds();

        SetPage(DataManager.Instance.GetPlayer(1).PlayerInfo.CurrentChater);
        SetChapterText();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        SetStageButtons();

        BindEvent(GetButton((int)Buttons.CloseButton).gameObject, OnCloseBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.NextStageButton).gameObject, OnNextStageBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.PrevStageButton).gameObject, OnPrevStageBtnClicked, Define.UIEvents.Click);
        GameAudioManager.Instance.Play2DSound("PopupOpen");
    }

    private void OnNextStageBtnClicked(PointerEventData data)
    {
        int chapter = DataManager.Instance.GetPlayer(1).PlayerInfo.NextChapter();
        int maxchapter = DataManager.Instance.GetPlayer(1).PlayerInfo.MaxChater;
        if (chapter >= maxchapter)
            chapter = maxchapter - 1;
        SetPage(chapter);
        GameAudioManager.Instance.Play2DSound("Page");
        SetChapterText();
    }

    private void OnPrevStageBtnClicked(PointerEventData data)
    {
        SetPage(DataManager.Instance.GetPlayer(1).PlayerInfo.PrevChapter());
        GameAudioManager.Instance.Play2DSound("Page");
        SetChapterText();
    }

    private void SetChapterText()
    {
        int chapter = DataManager.Instance.GetPlayer(1).PlayerInfo.CurrentChater;
        int maxchapter = DataManager.Instance.GetPlayer(1).PlayerInfo.MaxChater;
        if (chapter >= maxchapter)
            chapter = maxchapter - 1;
        GetText((int)Texts.ChapterText).text = $"Chapter {chapter + 1}";
    }

    public void SetPage(int page)
    {
        int viewstage = DataManager.Instance.GetPlayer(1).PlayerInfo.ViewStage;
        int clearstage = DataManager.Instance.GetPlayer(1).PlayerInfo.ClearStage;
        int maxstage = DataManager.Instance.GetPlayer(1).PlayerInfo.MaxStage;
        for (int i = 0; i < viewstage; i++)
        {
            int stage = i + 1 + page * viewstage;
            if (stage > maxstage)
                stageButtonList[i].SetActive(false);
            else
                stageButtonList[i].SetActive(true);
            bool clear = false;
            bool currently = false;

            if (stage < clearstage)
                clear = true;
            if (stage == clearstage)
                currently = true;
            stageButtonList[i].IInit();
            stageButtonList[i].ISetStageBtnInfo(clear,currently,stage);
        }
    }

    private void SetStageButtons()
    {
        IStageBtn[] stageButtons = GetComponentsInChildren<IStageBtn>();
        for(int i = 0; i < stageButtons.Length; i++)
        {
            if(stageButtonList.Contains(stageButtons[i]) == false)
            {
                stageButtonList.Add(stageButtons[i]);
                //stageButtons[i].Init();
                ////stageButtons[i].SetStageBtnInfo(i + 1);
                //if (i == 0)
                //    stageButtons[i].SetLock(false);
                //else
                //    stageButtons[i].SetLock(true);
            }
        }
    }

    private void OnCloseBtnClicked(PointerEventData data)
    {
        base.ClosePopupUI();
    }
}
