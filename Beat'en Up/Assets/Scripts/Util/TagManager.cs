using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region Player
public class PlayerAnimationTags
{
    public const string SPEED_INDEX = "Speed";
    public const string PUNCH_1_TRIGGER = "Punch1";
    public const string PUNCH_2_TRIGGER = "Punch2";
    public const string PUNCH_3_TRIGGER = "Punch3";

    public const string KICK_1_TRIGGER = "Kick1";
    public const string KICK_2_TRIGGER = "Kick2";
    public const string JUMPKICK_TRIGGER = "JumpKick";

    public const string KICKSKILL1_TRIGGER = "KickSkill1";
    public const string KICKSKILL2_TRIGGER = "KickSkill2";
    public const string PUNCHSKILL1_TRIGGER = "PunchSkill1";

    public const string DEFENCE_BOOL = "Defence";
    public const string JUMP_BOOL = "Jump";
  
    public const string KNOCK_DOWN_TRIGGER = "KnockDown";
    public const string STAND_UP_TRIGGER = "StandUp";
    public const string HIT_TRIGGER = "Hit";
    public const string DEATH_TRIGGER = "Death";
}

public class MoveInput
{
    private const KeyCode _sprint = KeyCode.LeftShift;
    private const string _x_Axis = "Horizontal";
    private const string _z_Axis = "Vertical";

    public static float X_Axis { get { return UnityEngine.Input.GetAxis(_x_Axis); } }
    public static float Z_Axis { get { return UnityEngine.Input.GetAxis(_z_Axis); } }
    public static bool SPRINT { get { return UnityEngine.Input.GetKey(_sprint); } }
}

public class AttackInput
{
    private const KeyCode _punchAttack = KeyCode.J;
    private const KeyCode _kickAttack = KeyCode.K;

    public static bool PUNCH { get { return UnityEngine.Input.GetKeyDown(_punchAttack); } }
    public static bool KICK { get { return UnityEngine.Input.GetKeyDown(_kickAttack); } }
    public static bool PUNCHPOWER { get { return UnityEngine.Input.GetKeyUp(_punchAttack); } }
    public static bool KICKPOWER { get { return UnityEngine.Input.GetKeyUp(_kickAttack); } }
}

public class StateInput
{
    private const KeyCode _defence = KeyCode.L;
    private const KeyCode _jump = KeyCode.Space;

    public static bool DEFENCE { get { return UnityEngine.Input.GetKey(_defence); } }
    public static bool JUMP { get { return UnityEngine.Input.GetKeyDown(_jump); } }
}
#endregion
public class EnemyAnimationTags
{
    public const string ATTACK_1_TRIGGER = "EnemyAttack1";
    public const string ATTACK_2_TRIGGER = "EnemyAttack2";
    public const string ATTACK_3_TRIGGER = "EnemyAttack3";

    public const string KNOCK_DOWN_TRIGGER = "KnockDown";
    public const string KNOCKBACK_TRIGGER = "KnockBack";
    public const string STAND_UP_TRIGGER = "StandUp";
    public const string HIT_TRIGGER = "Hit";
    public const string DEATH_TRIGGER = "Death";

}

public class Tags
{
    //AnimatorStateInfo Tag
    public const string ATTACK_TAG = "ATTACK";
    public const string SKILLATTACK_TAG = "SKILLATTACK";
    public const string JUMPKICK_TAG = "JUMPKICK";
    public const string LANDING_TAG = "LANDING";
    public const string DEFENCE_TAG = "DEFENCE";
    public const string HIT_TAG = "HIT";

    public const string GROUND_TAG = "Ground";
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

    public const string KNOCKBACK_TAG = "KnockBack";
    public const string LEFT_ARM_TAG = "LeftArm";
    public const string LEFT_LEG_TAG = "LeftLeg";
    public const string UNTAGGED_TAG = "Untagged";
    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string HEALTH_UI = "HealthUI";
}
