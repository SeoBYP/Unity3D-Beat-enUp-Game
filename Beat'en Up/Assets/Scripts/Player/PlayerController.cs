using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
namespace Charecters
{
    class PlayerController : BaseCharecterController
    {
        enum ComboState
        {
            NONE,
            PUNCH_1,
            PUNCH_2,
            PUNCH_3,
            KICK_1,
            KICK_2
        }
        enum SkillState
        {
            NONE,
            KICKSKILL1,
            KICKSKILL2,
            PUNCHSKILL1,
        }
        enum PunchHitSound
        {
            PunchHit1,
            PunchHit2,
            PunchHit3,
            PunchHit4,
            PunchHit5,
        }
        [Header("Grounded")]
        private bool Grounded;
        private float GroundedOffset = -0.05f;
        private float GroundedRadius = 0.2f;
        public LayerMask CheckLayer;

        [Header("PlayerJump")]
        private float JumpHeight = 1.2f;
        private float Gravity = -15.0f;
        public float JumpTimeout = 0.3f;
        public float FallTimeout = 0.7f;

        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        private AnimatorStateInfo _aniStateInfo;
        private CharactorAnimation player_Anim;
        private CharacterController _controller;
        private Animator _animator;

        [Header("PlayerAttack")]
        private bool activateTimerReset;
        public float _defaultComboTimer = 0.4f;
        private float _currentComboTimer;
        public float nextAnitime = 0.4f;
        private ComboState currentComboState;
        private SkillState currentSkillState;
        private bool PunchPower = false;
        private bool KickPower = false;

        [Header("PlayerMove")]
        private float SpeedChangeRate = 10.0f;
        private float _walkSpeed = 2f;
        private float _sprintSpeed = 4f;
        private float rotation_Y = -90;
        private float _animationBlend;
        private float X_Move;
        private float Z_Move;
        private float _speed = 0;

        [Header("PlayerStat")]
        private float _hp;
        private float _defence;

        [Header("Effects")]
        private PowersEffect _Powers;
        private SingleGameUI _singleGameUI;
        private BossStageUI _bossStageUI;

        private int CharacterID;
        private PunchHitSound hitSound = PunchHitSound.PunchHit1;

        protected override void Init()
        {
            _controller = GetComponent<CharacterController>();
            player_Anim = GetComponentInChildren<CharactorAnimation>();
            _animator = GetComponentInChildren<Animator>();
            _Powers = GetComponentInChildren<PowersEffect>();
            if (_Powers != null)
                _Powers.SetEffectActive(false);
            _currentComboTimer = _defaultComboTimer;
            currentComboState = ComboState.NONE;
            currentSkillState = SkillState.NONE;
            if (player_Anim != null)
                player_Anim.Init();
            SetStatus();
            SetGameUI();
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
            base.Init();
        }

        private void SetGameUI()
        {
            if(_singleGameUI == null)
                _singleGameUI = UIManager.Instance.Get<SingleGameUI>(UIList.SingleGameUI);
            if(_bossStageUI == null)
                _bossStageUI = UIManager.Instance.Get<BossStageUI>(UIList.BossStageUI);
        }

        private void SetStatus()
        {
            CharacterID = DataManager.Instance.GetPlayer(1).PlayerInfo.SeleteCharacterID;
            _hp = Utils.SetHPAmount(CharacterID);
            _defence = Utils.SetDefenceAmount(CharacterID);
        }

        protected override void None()
        {
            if (State == CharecterState.DEATH)
                return;
            if (_aniStateInfo.IsTag(Tags.HIT_TAG))
                return;
            Jump();
            Idle();
        }

        protected override void Idle()
        {
            UpdateKey();
            CheckStateInput();
            ResetComboState();
        }

        private Vector3 CheckMoveVector()
        {
            float targetSpeed = MoveInput.SPRINT ? _sprintSpeed : _walkSpeed;
            X_Move = MoveInput.X_Axis;
            Z_Move = MoveInput.Z_Axis;
            Vector3 MoveVec = new Vector3(X_Move, 0, Z_Move);
            if (MoveVec == Vector3.zero)
                targetSpeed = 0;
            else
            {
                float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
                float inputVeclocity = MoveVec.magnitude;
                if (currentHorizontalSpeed < targetSpeed - 0.1f || currentHorizontalSpeed > targetSpeed + 0.1f)
                {
                    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputVeclocity, Time.deltaTime * SpeedChangeRate);
                    _speed = Mathf.Round(_speed * 100f) / 100f;
                }
                else
                {
                    _speed = targetSpeed;
                }
            }
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            player_Anim.Locomotion(_animationBlend);
            return ((MoveVec * -targetSpeed) + new Vector3(0.0f, _verticalVelocity, 0.0f));
        }

        protected override void Move()
        {
            if (State == CharecterState.DEFENCE || State == CharecterState.DEATH)
                return;
            if (_aniStateInfo.IsTag(Tags.ATTACK_TAG) || _aniStateInfo.IsTag(Tags.LANDING_TAG) || _aniStateInfo.IsTag(Tags.SKILLATTACK_TAG))
                return;
            {
                if (X_Move > 0)
                    transform.rotation = Quaternion.Euler(0, -Mathf.Abs(rotation_Y), 0);
                else if (X_Move < 0)
                    transform.rotation = Quaternion.Euler(0, Mathf.Abs(rotation_Y), 0);
                _controller.Move((CheckMoveVector() * Time.deltaTime));
            }
        }
        #region KeyFrame
        public enum EButtons { LEFT, RIGHT, UP, DOWN, PUNCH, KICK }
        Queue<EButtons> _buttonQueue = new Queue<EButtons>();
        EButtons[] _RightKickSkill1 = { EButtons.RIGHT, EButtons.KICK };
        EButtons[] _LeftKickSkill1 = { EButtons.LEFT, EButtons.KICK };
        EButtons[] _KickSkill2 = { EButtons.UP, EButtons.KICK };
        EButtons[] _PunchSkill1 = { EButtons.DOWN, EButtons.PUNCH };
        bool CheckKeyList(EButtons[] eInput, EButtons[] eTarget)
        {
            if (eInput.Length == eTarget.Length)
            {
                int idx = 0;
                bool isOk = true;
                foreach (EButtons tmp in eInput)
                {
                    if (tmp != eTarget[idx]) { isOk = false; break; }
                    ++idx;
                }
                if (isOk)
                    return true;
                return false;
            }
            return false;
        }

        private bool SetKeyAttack()
        {
            EButtons[] buttons = new EButtons[_buttonQueue.Count];
            int idx = 0;
            while (_buttonQueue.Count != 0)
            {
                buttons[idx] = _buttonQueue.Dequeue();
                ++idx;
            }
            if(Grounded)
            {
                if (CheckKeyList(buttons, _RightKickSkill1) || CheckKeyList(buttons,_LeftKickSkill1))
                {
                    currentSkillState = SkillState.KICKSKILL1;
                    CheckSkillAttackTime();
                    return true;
                }

                else if (CheckKeyList(buttons, _KickSkill2))
                {
                    currentSkillState = SkillState.KICKSKILL2;
                    CheckSkillAttackTime();
                    return true;
                }
                else if (CheckKeyList(buttons, _PunchSkill1))
                {
                    currentSkillState = SkillState.PUNCHSKILL1;
                    CheckSkillAttackTime();
                    return true;
                }
            }
            return false;
        }

        private void UpdateKey()
        {
            SetPunchPower();
            SetKickPower();
            if (State == CharecterState.DEFENCE || State == CharecterState.DEATH)
                return;
            if (Input.GetKeyDown(KeyCode.D))
                _buttonQueue.Enqueue(EButtons.RIGHT);
            else if (Input.GetKeyDown(KeyCode.A))
                _buttonQueue.Enqueue(EButtons.LEFT);
            else if (Input.GetKeyDown(KeyCode.W))
                _buttonQueue.Enqueue(EButtons.UP);
            else if (Input.GetKeyDown(KeyCode.S))
                _buttonQueue.Enqueue(EButtons.DOWN);
            else if (AttackInput.PUNCH)
            {
                _buttonQueue.Enqueue(EButtons.PUNCH);
                if (!SetKeyAttack())
                {
                    PunchPower = true;
                }
            }
            else if (AttackInput.KICK)
            {
                _buttonQueue.Enqueue(EButtons.KICK);
                if (!SetKeyAttack())
                {
                    KickPower = true;
                }
            }
            Move();
        }
        #endregion

        #region PlayerAttack
        private void CheckPunchAttackTime()
        {
            if (_aniStateInfo.IsTag(Tags.ATTACK_TAG) || _aniStateInfo.IsTag(Tags.SKILLATTACK_TAG))
            {
                if (_aniStateInfo.normalizedTime > nextAnitime)
                {
                    if (_animator.IsInTransition(0))
                        return;
                    PunchAttack();
                }
            }
            else
                PunchAttack();
        }

        private void CheckKickAttackTime()
        {
            if (_aniStateInfo.IsTag(Tags.ATTACK_TAG) || _aniStateInfo.IsTag(Tags.SKILLATTACK_TAG))
            {
                if (_aniStateInfo.normalizedTime > 0.3f)
                {
                    if (_animator.IsInTransition(0))
                        return;
                    KickAttack();
                    JumpKick();
                }
            }
            else
            {
                KickAttack();
                JumpKick();
            }
        }

        private void CheckSkillAttackTime()
        {
            if (_aniStateInfo.IsTag(Tags.ATTACK_TAG) || _aniStateInfo.IsTag(Tags.SKILLATTACK_TAG))
            {
                if (_aniStateInfo.normalizedTime > nextAnitime)
                {
                    if (_animator.IsInTransition(0))
                        return;
                    SetSkills();
                }
            }
            else
            {
                SetSkills();
            }
        }

        private void SetSkills()
        {
            switch (currentSkillState)
            {
                case SkillState.KICKSKILL1:
                    player_Anim.KickSkill_1();
                    activateTimerReset = true;
                    break;
                case SkillState.KICKSKILL2:
                    player_Anim.KickSkill_2();
                    activateTimerReset = true;
                    break;
                case SkillState.PUNCHSKILL1:
                    player_Anim.PunchSkill_1();
                    activateTimerReset = true;
                    break;
            }
            currentSkillState = SkillState.NONE;
        }

        private void SetPunchPower()
        {
            if (PunchPower)
            {
                SetGameUI();
                Utils.SetImpactPower();
                SetPowersUI();
                if (Utils.ImpactPower > 1.5f)
                    _Powers.SetEffectActive(true);
                if(Utils.ImpactPower >= 2.8f)
                {
                    if (AttackInput.PUNCHPOWER)
                    {
                        PunchPower = false;
                        player_Anim.PunchSkill_1();
                        activateTimerReset = true;
                        _Powers.SetEffectActive(false);
                        return;
                    }
                }
                if (AttackInput.PUNCHPOWER)
                {
                    PunchPower = false;
                    CheckPunchAttackTime();
                }
            }
        }

        private void SetKickPower()
        {
            if (KickPower)
            {
                SetGameUI();
                Utils.SetImpactPower();
                SetPowersUI();
                if (Utils.ImpactPower > 1.5f)
                    _Powers.SetEffectActive(true);
                if (Utils.ImpactPower >= 2.8f)
                {
                    if (AttackInput.KICKPOWER)
                    {
                        KickPower = false;
                        player_Anim.KickSkill_2();
                        activateTimerReset = true;
                        _Powers.SetEffectActive(false);
                        return;
                    }
                }
                if (AttackInput.KICKPOWER)
                {
                    KickPower = false;
                    CheckKickAttackTime();
                }
            }
        }

        private void SetPowersUI()
        {
            if (_singleGameUI != null)
                _singleGameUI.DisplayerPowers(Utils.ImpactPower);
            if(_bossStageUI != null)
                _bossStageUI.DisplayerPowers(Utils.ImpactPower);
        }

        private void PunchAttack()
        {
            if (Grounded == false)
                return;
            if (currentComboState == ComboState.PUNCH_3 || currentComboState == ComboState.KICK_1 || currentComboState == ComboState.KICK_2)
                return;

            currentComboState++;
            activateTimerReset = true;
            _currentComboTimer = _defaultComboTimer;
            if (currentComboState == ComboState.PUNCH_1)
            {
                player_Anim.Punch_1();
            }
            if (currentComboState == ComboState.PUNCH_2)
            {
                player_Anim.Punch_2();
            }
            if (currentComboState == ComboState.PUNCH_3)
            {
                player_Anim.Punch_3();
            }

        }

        private void KickAttack()
        {
            if (Grounded == false)
                return;
            if (currentComboState == ComboState.KICK_2 ||
                    currentComboState == ComboState.PUNCH_3)
                return;
            if (currentComboState == ComboState.NONE ||
                currentComboState == ComboState.PUNCH_1 ||
                currentComboState == ComboState.PUNCH_2)
                currentComboState = ComboState.KICK_1;
            else if (currentComboState == ComboState.KICK_1)
                currentComboState++;
            activateTimerReset = true;
            _currentComboTimer = _defaultComboTimer;
            if (currentComboState == ComboState.KICK_1)
                player_Anim.Kick_1();
            if (currentComboState == ComboState.KICK_2)
                player_Anim.Kick_2();
        }

        private void JumpKick()
        {
            if (Grounded == true)
                return;
            if (_aniStateInfo.IsTag("JUMPKICK") == false)
            {
                player_Anim.JumpAttack();
            }
        }

        private void ResetComboState()
        {
            if (activateTimerReset)
            {
                _currentComboTimer -= Time.deltaTime;
                if (_currentComboTimer <= 0f)
                {
                    currentComboState = ComboState.NONE;
                    activateTimerReset = false;
                    _currentComboTimer = _defaultComboTimer;
                    Utils.ReSetImpactPower();
                    SetPowersUI();
                    _Powers.SetEffectActive(false);
                }
            }
        }
        #endregion
        private void CheckStateInput()
        {
            if (StateInput.DEFENCE)
            {
                State = CharecterState.DEFENCE;
            }
        }

        protected override void Defence()
        {
            if (StateInput.DEFENCE == true)
            {
                if (_aniStateInfo.IsTag(Tags.DEFENCE_TAG) == false)
                {
                    player_Anim.Defence(true);
                }
            }
            else
            {
                player_Anim.Defence(false);
                State = CharecterState.NONE;
            }
        }
        protected override void Jump()
        {
            if (_aniStateInfo.IsTag(Tags.LANDING_TAG))
                return;
            if (Grounded)
            {
                _fallTimeoutDelta = FallTimeout;
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }
                if (StateInput.JUMP && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    player_Anim.Jump(true);
                }
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = JumpTimeout;
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    player_Anim.Jump(false);
                }
            }
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private void CheckGrounded()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, CheckLayer, QueryTriggerInteraction.Ignore);
        }

        private float CheckHP(float damage)
        {
            SetGameUI();
            float damagevalue = damage - _defence;
            if (damagevalue < 0)
                return _hp;
            else
            {
                float currentHp = _hp - damagevalue;
                _hp = currentHp;
                if (_singleGameUI != null)
                    _singleGameUI.DisplayHealth(currentHp);
                if (_bossStageUI != null)
                    _bossStageUI.DisplayHealth(currentHp);
                return currentHp;
            }
        }

        public override void ApplyDamage(float damage, bool knockDown = false)
        {
            if (State == CharecterState.DEATH)
                return;
            if (State == CharecterState.DEFENCE)
            {
                GameAudioManager.Instance.Play2DSound("Defence");
                return;
            }


            if (CheckHP(damage) <= 0)
            {
                OnDeath();
            }
            else
            {
                if (knockDown == true)
                    player_Anim.KnockDown();
                else
                {
                    player_Anim.Hit();
                    GameAudioManager.Instance.Play2DSound(GetHitSoundName(hitSound));
                    hitSound++;
                }

            }
        }

        string GetHitSoundName(PunchHitSound sound)
        {
            if (sound.GetHashCode() >= 5)
                sound = PunchHitSound.PunchHit1;
            string name = System.Enum.GetName(typeof(PunchHitSound), sound);
            return name;
        }

        private void OnDeath()
        {
            State = CharecterState.DEATH;
            player_Anim.Death();
            SetDefeateScene();
        }

        private void SetDefeateScene()
        {
            if (_singleGameUI != null)
                _singleGameUI.Defeate();
            if (_bossStageUI != null)
                _bossStageUI.Defeate();
        }

        protected override void Run()
        {
            _aniStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            CheckGrounded();
            ButtonQueueClear();
            base.Run();
        }
        private float prevtime = 0;
        private float exittime = 0.5f;
        private void ButtonQueueClear()
        {
            float elapesdtime = Time.time - prevtime;
            if(elapesdtime > exittime)
            {
                if (_buttonQueue.Count != 0)
                    _buttonQueue.Clear();
                prevtime = Time.time;
            }
        }


    }
}
