using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class BossStageButton : BaseUI,IStageBtn
{
    enum Images
    {
        Lock,
    }
    enum Buttons
    {
        StageBtn,
    }
    List<ResultStar> resultStarList = new List<ResultStar>();
    private bool IsLock = false;
    private int stage;
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton(0).gameObject, OnStageBtnClicked, Define.UIEvents.Click);
        GetResultStars();
    }

    public void IInit()
    {
        Init();
    }

    public void ISetStageBtnInfo(bool clear, bool currently, int number = 0)
    {
        stage = number;
        SetLock(true);
        GetButton((int)Buttons.StageBtn).enabled = false;
        UnSetReslutStars();
        if (clear)
        {
            SetResultClearStars();
            SetLock(false);
            GetButton((int)Buttons.StageBtn).enabled = true;
        }
        else
        {
            if (currently)
            {
                GetButton((int)Buttons.StageBtn).enabled = true;
                SetLock(false);
            }
            else
                SetLock(true);
        }
    }

    private void GetResultStars()
    {
        ResultStar[] resultStars = GetComponentsInChildren<ResultStar>();
        for (int i = 0; i < resultStars.Length; i++)
        {
            if (resultStarList.Contains(resultStars[i]) == false)
            {
                resultStarList.Add(resultStars[i]);
                resultStars[i].Init();
            }
        }
    }

    private void SetResultClearStars()
    {
        for (int i = 0; i < resultStarList.Count; i++)
        {
            resultStarList[i].SetClearStar();
        }
    }

    private void UnSetReslutStars()
    {
        for (int i = 0; i < resultStarList.Count; i++)
        {
            resultStarList[i].UnSetClearStar();
        }
    }

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }

    private void SetLock(bool state)
    {
        IsLock = state;
        GetImage(0).gameObject.SetActive(state);
    }

    private void OnStageBtnClicked(PointerEventData data)
    {
        if (IsLock == true)
            return;
        if (DataManager.Instance.GetPlayer(1).PlayerInfo.CheckEnegy())
        {
            DataManager.Instance.GetPlayer(1).PlayerInfo.SetSeleteStage(stage);
            SceneManagerEx.Instance.LoadScene(Scene.BossStage);
        }
    }
}
