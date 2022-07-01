using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
public class BossStageUI : SceneUI
{
    enum Images
    {
        HealthUI,
        PowersUI,
        PlayerIcon,
    }
    enum Texts
    {
        BossNameText,
        TitleBossNameText,
    }
    enum GameObjects
    {
        BossHPBar,
        Warning,
        WarningBackGround,
    }

    private Image _playerhpUI;
    private Image _playerpowerUI;
    //private Image _bosshealthUI;
    private BossHPBar bossHPBar;
    public override void Init()
    {
        CurrentUIList = UIList.BossStageUI;
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        _playerhpUI = GetImage((int)Images.HealthUI);
        _playerhpUI.fillAmount = 1;
        _playerpowerUI = GetImage((int)Images.PowersUI);
        bossHPBar = GetComponentInChildren<BossHPBar>();
        if (bossHPBar != null)
            bossHPBar.Init();
        int charID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        GetImage((int)Images.PlayerIcon).sprite = CharacterStatManager.Instance.GetCharacterIcon(charID);
        GetGameObject((int)GameObjects.BossHPBar).SetActive(false);
        
    }

    public void DisplayHealth(float value)
    {
        int charID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        value /= Utils.SetHPAmount(charID);

        if (value < 0f)
            value = 0;

        _playerhpUI.fillAmount = value;
    }

    public void DisplayerPowers(float powers)
    {
        float value = (powers - 1) / 2;
        if (value <= 0)
            value = 0;

        _playerpowerUI.fillAmount = value;
    }

    public BossHPBar SetBossHPBar(int charID)
    {
        if (bossHPBar != null)
        {
            bossHPBar.SetEnemyName(charID);
            return bossHPBar;
        }

        return null;
    }

    public void StartBossStageAni(string name)
    {
        GetText((int)Texts.TitleBossNameText).text = name;
        StartCoroutine(PlayBossStageAni());
    }

    IEnumerator PlayBossStageAni()
    {
        GetGameObject((int)GameObjects.WarningBackGround).GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1.6f);
        GetGameObject((int)GameObjects.WarningBackGround).SetActive(false);
        GetGameObject((int)GameObjects.Warning).SetActive(false);
        GetGameObject((int)GameObjects.BossHPBar).SetActive(true);
        yield break;
    }


    public void Defeate()
    {
        UIManager.Instance.ShowPopupUI<DefeateStageUI>().SetScene(Scene.BossStage);
    }

}
