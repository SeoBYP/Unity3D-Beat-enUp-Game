using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;
public class ClearStageUI : PopupUI
{
    enum Buttons
    {
        NextStageBtn,
        ExitStageBtn,
    }
    enum Images
    {
        EXPFillAmount,
        CharacterIcon,
    }
    enum Texts
    {
        EXPText,
        LevelText,
        GoldAmountText,
        GemAmountText,
        NormalItemPriceText,
        RareItemPriceText,
    }
    enum Transforms
    {
        RewardNormalItemSlot,
        RewardRareItemSlot,
    }
    enum GameObjects
    {
        NormalChangeMoney,
        RareChangeMoney,
        CharacterSlot,
    }
    List<ResultStar> resultStarList = new List<ResultStar>();

    Transform rewardNormal;
    Transform rewardRare;
    private int StageEXP;
    private bool IsSetNormalReward = false;
    private bool IsSetRareReward = false;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<Transform>(typeof(Transforms));
        Bind<GameObject>(typeof(GameObjects));
        rewardNormal = Get<Transform>((int)Transforms.RewardNormalItemSlot);
        rewardRare = Get<Transform>((int)Transforms.RewardRareItemSlot);

        BindEvent(GetButton((int)Buttons.NextStageBtn).gameObject, OnNextStageBtnClicked, Define.UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ExitStageBtn).gameObject, OnExitStageBtnClicked, Define.UIEvents.Click);
        GetGameObject((int)GameObjects.NormalChangeMoney).SetActive(false);
        GetGameObject((int)GameObjects.RareChangeMoney).SetActive(false);
        GetGameObject((int)GameObjects.CharacterSlot).SetActive(false);
        SetResultStar();
        SetReward();
        GameAudioManager.Instance.Play2DSound("Clear");
    }

    private void SetResultStar()
    {
        ResultStar[] resultStars = GetComponentsInChildren<ResultStar>();
        for(int i = 0; i < resultStars.Length; i++)
        {
            if(resultStarList.Contains(resultStars[i]) == false)
            {
                resultStarList.Add(resultStars[i]);
                resultStars[i].Init();
                resultStars[i].SetClearStar();
            }
        }
    }

    private void SetReward()
    {
        CheckPlayerStage();
        SetEXPReward();
        SetGoldAndGemReward();
        SetRewardItem();
        SetCharacterReward();
    }

    private void CheckPlayerStage()
    {
        int seletestage = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteStage;
        int clearstage = DataManager.Instance.GetPlayer(1).PlayerInfo.ClearStage;
        StageInfoManager.Instance.SetStageClearCount(seletestage);
        if (seletestage < clearstage)
            return;
        else
        {
            DataManager.Instance.GetPlayer(1).PlayerInfo.SetClearStage();
            return;
        }
    }

    private void SetPlayerExp()
    {
        int stageID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteStage;
        StageEXP = StageInfoManager.Instance.GetStageEXP(stageID);
        int selete = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        DataManager.Instance.GetPlayer(1).SetExp(selete, StageEXP);
    }

    private void SetEXPReward()
    {
        SetPlayerExp();
        int selete = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        float exp = DataManager.Instance.GetPlayer(1).GetCharacter(selete).EXP;
        float maxexp = DataManager.Instance.GetPlayer(1).GetCharacter(selete).MAXEXP;
        float EXPAmount = exp / maxexp;
        GetImage((int)Images.EXPFillAmount).fillAmount = EXPAmount;
        GetText((int)Texts.EXPText).text = $"{exp} / {maxexp}";
        GetText((int)Texts.LevelText).text = $"{DataManager.Instance.GetPlayer(1).GetCharacter(selete).LEVEL}";
    }

    private void SetCharacterReward()
    {
        int selete = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteStage;
        if(selete % 10 == 0)
        {
            GetGameObject((int)GameObjects.CharacterSlot).SetActive(true);
            int rewardID = RewardCharacterID();
            if(DataManager.Instance.GetPlayer(1).CheckPlayerCharacter(rewardID) == false)
            {
                GetImage((int)Images.CharacterIcon).sprite = CharacterStatManager.Instance.GetCharacterIcon(rewardID);
                DataManager.Instance.GetPlayer(1).SetPlayerCharacter(rewardID);
                return;
            }
            else
            {
                GetImage((int)Images.CharacterIcon).sprite = CharacterStatManager.Instance.GetCharacterIcon(rewardID);
                UIManager.Instance.ShowPopupUI<ErrorPopupUI>().SetText("이미 가지고 있는 캐릭터입니다.\n *경험치 2배*");
                SetEXPReward();
            }
        }
    }

    private int RewardCharacterID()
    {
        int maxcount = DataManager.TableDic[TableType.CharacterInformation].InfoDic.Count;
        while (true)
        {
            int randomID = Random.Range(1, maxcount + 1);
            string job = CharacterStatManager.Instance.GetString(randomID, CharacterIndex.JOB);
            if (job != "BossEnemy" && job != "Enemy")
            {
                return randomID;
            }
        }
    }

    private void SetGoldAndGemReward()
    {
        int randomGold = Random.Range(1000, 3000);
        int randomGem = Random.Range(1, 10);

        int playergold = DataManager.Instance.GetPlayer(1).PlayerInfo.Gold;
        int playergem = DataManager.Instance.GetPlayer(1).PlayerInfo.Gem;

        DataManager.Instance.GetPlayer(1).PlayerInfo.SetPlayerGold(playergold + randomGold);
        DataManager.Instance.GetPlayer(1).PlayerInfo.SetPlayerGem(playergem + randomGem);

        GetText((int)Texts.GoldAmountText).text = $"{randomGold}";
        GetText((int)Texts.GemAmountText).text = $"{randomGem}";
    }

    private void SetRewardItem()
    {
        int count = 0;
        int itemMaxID = DataManager.TableDic[TableType.ItemInformation].InfoDic.Count;
        while (true)
        {
            int randomItemID = Random.Range(1, itemMaxID + 1);
            string rarity = ItemDataManager.Instance.GetString(randomItemID, ItemData.RARITY);
            switch (rarity)
            {
                case "Normal":
                    if(IsSetNormalReward == false)
                    {
                        UIManager.Instance.LoadItemSlot(rewardNormal, randomItemID);
                        SetPlayerItems(randomItemID, GameObjects.NormalChangeMoney,Texts.NormalItemPriceText);
                        count++;
                        IsSetNormalReward = true;
                    }
                    break;
                case "Rare":
                    if (IsSetRareReward == false)
                    {
                        UIManager.Instance.LoadItemSlot(rewardRare, randomItemID);
                        SetPlayerItems(randomItemID, GameObjects.RareChangeMoney, Texts.RareItemPriceText);
                        count++;
                        IsSetRareReward = true;
                    }
                    break;
            }
            if (count >= 2)
            {
                IsSetNormalReward = false;
                IsSetRareReward = false;
                return;
            }
        }
    }

    private void SetPlayerItems(int itemID,GameObjects games,Texts texts)
    {
        if(DataManager.Instance.GetPlayer(1).CheckPlayerItem(itemID) == false)
        {
            DataManager.Instance.GetPlayer(1).SetPlayerItem(itemID);
        }
        else
        {
            if(ItemDataManager.Instance.GetItemType(itemID) == ItemType.None)
            {
                DataManager.Instance.GetPlayer(1).SetPlayerItem(itemID);
                return;
            }
            else
            {
                int money = ItemDataManager.Instance.GetInt(itemID, ItemData.PRICE);
                int gold = DataManager.Instance.GetPlayer(1).PlayerInfo.Gold;
                DataManager.Instance.GetPlayer(1).PlayerInfo.SetPlayerGold(gold + money);
                GetGameObject((int)games).SetActive(true);
                GetText((int)texts).text = money.ToString();
            }
        }
    }

    private void OnNextStageBtnClicked(PointerEventData data)
    {
        DataManager.Instance.GetPlayer(1).PlayerInfo.AddSeleteStage();
        SceneManagerEx.Instance.LoadScene(Scene.SingleGame);
    }
    private void OnExitStageBtnClicked(PointerEventData data)
    {
        ClosePopupUI();
        SceneManagerEx.Instance.LoadScene(Scene.SingleGameLobby);
    }
    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
    }
}
