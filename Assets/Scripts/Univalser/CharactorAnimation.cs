using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorAnimation : MonoBehaviour
{
    private Animator anim;
    //private AnimatorStateInfo _stateinfo;
    public void Init()
    {
        anim = GetComponent<Animator>();
    }
    //플레이어 움직임 애니
    public void Locomotion(float speed)
    {
        anim.SetFloat(PlayerAnimationTags.SPEED_INDEX, speed);
    }
    //플레이어 공격 애니
    public void Punch_1()
    {
        anim.SetTrigger(PlayerAnimationTags.PUNCH_1_TRIGGER);
    }
    public void Punch_2()
    {
        anim.SetTrigger(PlayerAnimationTags.PUNCH_2_TRIGGER);
    }
    public void Punch_3()
    {
        anim.SetTrigger(PlayerAnimationTags.PUNCH_3_TRIGGER);
    }
    public void Kick_1()
    {
        anim.SetTrigger(PlayerAnimationTags.KICK_1_TRIGGER);
    }
    public void Kick_2()
    {
        anim.SetTrigger(PlayerAnimationTags.KICK_2_TRIGGER);
    }

    public void JumpAttack()
    {
        anim.SetTrigger(PlayerAnimationTags.JUMPKICK_TRIGGER);
    }

    public void KickSkill_1()
    {
        anim.SetTrigger(PlayerAnimationTags.KICKSKILL1_TRIGGER);
    }

    public void KickSkill_2()
    {
        anim.SetTrigger(PlayerAnimationTags.KICKSKILL2_TRIGGER);
    }

    public void PunchSkill_1()
    {
        anim.SetTrigger(PlayerAnimationTags.PUNCHSKILL1_TRIGGER);
    }

    //플레이어 상태 애니
    public void Defence(bool state)
    {
        anim.SetBool(PlayerAnimationTags.DEFENCE_BOOL,state);
    }
    public void Jump(bool grounded)
    {
        anim.SetBool(PlayerAnimationTags.JUMP_BOOL, grounded);
    }


    public void KnockDown()
    {
        anim.SetTrigger(EnemyAnimationTags.KNOCK_DOWN_TRIGGER);
    }

    public void StandUp()
    {
        anim.SetTrigger(EnemyAnimationTags.STAND_UP_TRIGGER);
    }

    public void Hit()
    {
        anim.SetTrigger(EnemyAnimationTags.HIT_TRIGGER);
    }

    public void Death()
    {
        anim.SetTrigger(EnemyAnimationTags.DEATH_TRIGGER);
    }

    public void EnemyAttack(int attack)
    {
        if (attack == 0)
            anim.SetTrigger(EnemyAnimationTags.ATTACK_1_TRIGGER);
        if (attack == 1)
            anim.SetTrigger(EnemyAnimationTags.ATTACK_2_TRIGGER);
        if (attack == 2)
            anim.SetTrigger(EnemyAnimationTags.ATTACK_3_TRIGGER);
    }

    public void EnemyLocomotion(float enemyspeed)
    {
        anim.SetFloat(PlayerAnimationTags.SPEED_INDEX, enemyspeed);
    }

    public void EnemyDeath()
    {
        anim.SetTrigger(EnemyAnimationTags.DEATH_TRIGGER);
    }
    public void EnemyKnockBack()
    {
        anim.SetTrigger(EnemyAnimationTags.KNOCKBACK_TRIGGER);
    }
}
