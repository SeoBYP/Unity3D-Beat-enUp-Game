using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Charecters;
using Managers;
public class AnimationListener : MonoBehaviour
{
    public GameObject LeftArmAttackPoint, RightArmAttackPoint,
        LeftKickAttackPoint, RightKickAttackPoint;

    private float stantupTimer = 2f;

    private CharactorAnimation animationScript;
    private CameraController shakeCamera;
    private void Start()
    {
        animationScript = GetComponent<CharactorAnimation>();
        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<CameraController>();
    }

    private void LeftArmAttack_On()
    {
        LeftArmAttackPoint.SetActive(true);
    }
    private void LeftArmAttack_Off()
    {
        if (LeftArmAttackPoint.activeInHierarchy)
        {
            LeftArmAttackPoint.SetActive(false);
        }
    }

    private void RightArmAttack_On()
    {
        RightArmAttackPoint.SetActive(true);
    }
    private void RightArmAttack_Off()
    {
        if (RightArmAttackPoint.activeInHierarchy)
        {
            RightArmAttackPoint.SetActive(false);
        }
    }

    private void LeftKickAttack_On()
    {
        LeftKickAttackPoint.SetActive(true);
    }
    private void LeftKickAttack_Off()
    {
        if (LeftKickAttackPoint.activeInHierarchy)
        {
            LeftKickAttackPoint.SetActive(false);
        }
    }

    private void RightKickAttack_On()
    {
        RightKickAttackPoint.SetActive(true);
    }
    private void RightKickAttack_Off()
    {
        if (RightKickAttackPoint.activeInHierarchy)
        {
            RightKickAttackPoint.SetActive(false);
        }
    }

    void TagLeft_Arm()
    {
        LeftArmAttackPoint.tag = Tags.LEFT_ARM_TAG;
    }

    void UntagLeft_Arm()
    {
        LeftArmAttackPoint.tag = Tags.UNTAGGED_TAG;
    }

    void TagLeft_Leg()
    {
        LeftKickAttackPoint.tag = Tags.LEFT_LEG_TAG;
    }

    void UntagLeft_Leg()
    {
        LeftKickAttackPoint.tag = Tags.UNTAGGED_TAG;
    }

    void OnKnockBack()
    {
        LeftKickAttackPoint.tag = Tags.KNOCKBACK_TAG;
    }

    void OffKnockBack()
    {
        LeftKickAttackPoint.tag = Tags.UNTAGGED_TAG;
    }

    void OnRightKickKnockBack()
    {
        RightKickAttackPoint.tag = Tags.KNOCKBACK_TAG;
    }

    void OffRightKickKnockBack()
    {
        RightKickAttackPoint.tag = Tags.UNTAGGED_TAG;
    }

    void Attack_FX_Sound()
    {
        GameAudioManager.Instance.Play2DSound("Whoosh");
    }

    void Attack_FX()
    {
        ResourcesManager.Instance.Instantiate("Effects/FighterSkillEffect").transform.position = SetEffectPos();
        GameAudioManager.Instance.Play2DSound("RockSpike");
    }

    private Vector3 SetEffectPos()
    {
        Vector3 startPos = transform.position;
        Vector3 direction = transform.forward.normalized * 1;
        return startPos + direction;
    }

    void Enemy_HitGround()
    {
        Managers.GameAudioManager.Instance.Play2DSound("Drop");
    }
    void Enemy_StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(stantupTimer);
        animationScript.StandUp();
    }

    void Enemy_Death()
    {
        StartCoroutine(DeactiveGameObject());
    }

    IEnumerator DeactiveGameObject()
    {
        EnemyController enemy = gameObject.GetComponentInParent<EnemyController>();
        yield return new WaitForSeconds(2);
        enemy.gameObject.SetActive(false);
    }

    void ShakeCameraOnFall()
    {
        CameraController.ShouldShake = true;
    }
}
