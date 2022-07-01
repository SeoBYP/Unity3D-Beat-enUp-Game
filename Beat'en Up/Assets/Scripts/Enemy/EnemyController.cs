using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

namespace Charecters 
{
    class EnemyController : BaseCharecterController
    {
        private CharactorAnimation enemy_Ani;
        private CharacterController _controller;
        private PlayerController _player;
        private Animator _animator;
        private AnimatorStateInfo _aniStateInfo;

        private float _attackDistance = 1.4f;
        private float _speed = 2f;
        private float SpeedChangeRate = 10.0f;
        private float targetSpeed = 0;
        private float _animationBlend = 0;

        private float _attackTime = 2f;
        private float _currentAttackTime = 0;
        private bool _isAttack = false;

        private float _hp;
        private float _defence;
        private int _enemyID;

        //public bool IsKnockDown { get; private set; }
        float prevTime = 0;
        float nexttime = 1;
        Vector3 targetPos;

        SingleGame game;
        EnemyHPBar _enemyHP;
        protected override void Init()
        {
            enemy_Ani = GetComponentInChildren<CharactorAnimation>();
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            game = FindObjectOfType<SingleGame>();
            if (enemy_Ani != null)
                enemy_Ani.Init();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            _controller.enabled = true;
            base.Init();
        }

        public void SetStat(int charID)
        {
            _enemyID = charID;
            _hp = CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.HP);
            _defence = CharacterStatManager.Instance.GetFloat(charID, CharacterIndex.DEFENCE);
            SetEnemyAttackPoint(charID);
        }

        private void SetEnemyAttackPoint(int charID)
        {
            EnemyAttackPoint[] attackPoints = GetComponentsInChildren<EnemyAttackPoint>();
            for(int i = 0; i < attackPoints.Length; i++)
            {
                attackPoints[i].SetDamage(charID);
            }
        }

        protected override void None()
        {
            if (State == CharecterState.DEATH)
                return;
            if (_aniStateInfo.IsTag("KNOCKDOWN") || _aniStateInfo.IsTag("KNOCKBACK"))
            {
                IsKnockDown = true;
                return;
            }
            else
            {
                IsKnockDown = false;
            }

            Move();
            Attack();
        }

        private float CheckDistance()
        {
            if (_player == null)
            {
                Debug.Log("Player can't Founded");
                return 0;
            }
            return Vector3.Distance(transform.position, _player.transform.position);
        }

        private Vector3 CheckMoveVector()
        {
            Vector3 targetPos = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            if (CheckDistance() <= _attackDistance)
            {
                _isAttack = true;
                targetSpeed = 0;
            }

            else
            {
                _isAttack = false;
                targetSpeed = 2;
            }
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
            if (currentHorizontalSpeed < targetSpeed - 0.1 || currentHorizontalSpeed > targetSpeed + 0.1f)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
                _speed = Mathf.Round(_speed * 100f) / 100f;
            }
            else
            {
                _speed = targetSpeed;
            }
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            enemy_Ani.EnemyLocomotion(_animationBlend);
            return targetPos;
        }

        protected override void Move()
        {
            if (_aniStateInfo.IsTag("ATTACK") || _aniStateInfo.IsTag("SKILLATTACK"))
                return;

            transform.LookAt(CheckMoveVector()); ;
            _controller.Move(transform.forward * targetSpeed * Time.deltaTime);
        }

        protected override void Attack()
        {
            if (_isAttack == false)
                return;

            _currentAttackTime += Time.deltaTime;
            if (_currentAttackTime > _attackTime)
            {
                enemy_Ani.EnemyAttack(Random.Range(0, 3));
                _currentAttackTime = 0;
            }
        }

        private float CheckHP(float damage)
        {
            float currentHp;
            float damagevalue = damage - _defence;
            if (damagevalue < 0)
                return _hp;
            else
            {
                currentHp = _hp - damagevalue;
                _hp = currentHp;
                _enemyHP.DisplayHealth(_hp);
                return _hp;
            }
        }

        public override void ApplyDamage(float damage, bool knockDown)
        {
            if (State == CharecterState.DEATH)
                return;
            if (_aniStateInfo.IsTag("KNOCKDOWN"))
                return;
            if (_enemyHP == null)
            {
                _enemyHP = UIManager.Instance.Get<SingleGameUI>(UIList.SingleGameUI).SetEnemyHPBar(_enemyID);
            }

            if (CheckHP(damage) <= 0)
            {
                OnDeath();
            }
            else
            {
                if (knockDown)
                {
                    IsKnockDown = knockDown;
                    enemy_Ani.KnockDown();
                    targetPos = SetKnockBackVec();
                }
                else
                {
                    IsKnockDown = knockDown;
                    enemy_Ani.Hit();
                }
            }
        }

        public override void ApplyKnockBack(float damage, bool knockBack)
        {
            if (State == CharecterState.DEATH)
                return;
            if (_aniStateInfo.IsTag("KNOCKDOWN") || _aniStateInfo.IsTag("KNOCKBACK"))
                return;
            if (_enemyHP == null)
                _enemyHP = UIManager.Instance.Get<SingleGameUI>(UIList.SingleGameUI).SetEnemyHPBar(_enemyID);
            if (CheckHP(damage) <= 0)
            {
                OnDeath();
            }
            else
            {
                if (knockBack)
                {
                    IsKnockBack = knockBack;
                    enemy_Ani.EnemyKnockBack();
                    targetPos = SetKnockBackVec();
                }
                else
                {
                    IsKnockBack = knockBack;
                    enemy_Ani.Hit();
                }
            }
        }

        private Vector3 SetKnockBackVec()
        {
            float x = transform.position.x + (transform.forward.x * -2);
            float y = transform.position.y;
            float z = transform.position.z;
            return new Vector3(x,y,z);
        }

        private void OnDeath()
        {
            State = CharecterState.DEATH;
            enemy_Ani.EnemyDeath();
            _enemyHP.ClosePopupUI();
            _controller.enabled = false;
            GameData.CurrentEnemyCount--;
            game.m_SpawnEnemy();
            base.Clear();
        }

        private void OnKnockBack()
        {
            if (IsKnockBack == false)
                return;
            else
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
        }

        private void OnKnockDown()
        {
            if (IsKnockDown == false)
                return;
            else
                transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 1.5f);
        }
        private void ReSetEventState()
        {
            float elapedTime = Time.time - prevTime;
            if (elapedTime >= nexttime)
            {
                IsKnockBack = false;
                IsKnockDown = false;
                prevTime = Time.time;
            }
        }
        protected override void Run()
        {
            ReSetEventState();
            OnKnockBack();
            OnKnockDown();
            _aniStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            base.Run();
        }


    }
}


