using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class SingleGameUI : SceneUI
{
    enum Images
    {
        HealthUI,
        PowersUI,
        PlayerIcon,
    }
    enum Transforms
    {
        EnemyHpBarGroup,
        GoNextStage,
    }

    private Image _playerhpUI;
    private Image _playerpowerUI;
    private Transform _enemyhpGroup;
    private Transform _goNextStage;

    public override void Init()    
    {
        CurrentUIList = UIList.SingleGameUI;
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<Transform>(typeof(Transforms));

        _playerhpUI = GetImage((int)Images.HealthUI);
        _playerhpUI.fillAmount = 1;
        _playerpowerUI = GetImage((int)Images.PowersUI);
        int charID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
        GetImage((int)Images.PlayerIcon).sprite = CharacterStatManager.Instance.GetCharacterIcon(charID);
        _enemyhpGroup = Get<Transform>((int)Transforms.EnemyHpBarGroup);
        _goNextStage = Get<Transform>((int)Transforms.GoNextStage);
        _goNextStage.gameObject.SetActive(false);
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

    public EnemyHPBar SetEnemyHPBar(int charID)
    {
        EnemyHPBar enemyHP = UIManager.Instance.ShowPopupUI<EnemyHPBar>(null, _enemyhpGroup);
        if(enemyHP != null)
        {
            enemyHP.SetEnemyName(charID);
            return enemyHP;
        }
        return null;
    }

    public void SetGoNextStage()
    {
        _goNextStage.gameObject.SetActive(true);
        StartCoroutine(PlayAni());
    }

    IEnumerator PlayAni()
    {
        _goNextStage.GetComponentInChildren<Animation>().Play();
        yield return new WaitForSeconds(5f);
        _goNextStage.gameObject.SetActive(false);
        yield break;
    }

    public void Defeate()
    {
        UIManager.Instance.ShowPopupUI<DefeateStageUI>().SetScene(Scene.SingleGame);
    }
}
