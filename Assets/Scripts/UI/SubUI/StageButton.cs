using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class StageButton : BaseUI, IStageBtn
{
    enum Buttons
    {
        StageBtn,
    }
    enum Texts
    {
        Text,
    }
    enum Gameobejcts
    {
        CheckClearStage,
    }

    List<ResultStar> resultStarList = new List<ResultStar>();
    private bool IsLock = false;
    private int stage;
    public override void Init()
    {
        Binds();
    }

    public void IInit()
    {
        Init();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(Gameobejcts));

        BindEvent(GetButton((int)Buttons.StageBtn).gameObject, OnStageBtnClicked, Define.UIEvents.Click);
        GetResultStars();
    }

    private void SetLock(bool state)
    {
        IsLock = state;
        GetGameObject((int)Gameobejcts.CheckClearStage).SetActive(state);
    }

    private void SetStageText(int number)
    {
        GetText((int)Texts.Text).text = number.ToString();
    }

    public void ISetStageBtnInfo(bool clear,bool currently,int number = 0)
    {
        stage = number;
        SetStageText(number);
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
        for(int i = 0; i < resultStars.Length; i++)
        {
            if(resultStarList.Contains(resultStars[i]) == false)
            {
                resultStarList.Add(resultStars[i]);
                resultStars[i].Init();
            }
        }
    }

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
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

    private void OnStageBtnClicked(PointerEventData data)
    {
        if (IsLock == true)
            return;
        if (DataManager.Instance.GetPlayer(1).PlayerInfo.CheckEnegy())
        {
            DataManager.Instance.GetPlayer(1).PlayerInfo.SetSeleteStage(stage);
            SceneManagerEx.Instance.LoadScene(Scene.SingleGame);
        }
    }
}
