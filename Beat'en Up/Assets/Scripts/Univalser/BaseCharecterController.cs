using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
namespace Charecters
{
    public class BaseCharecterController : MonoBehaviour, IUpdate
    {
        protected enum CharecterState
        {
            NONE,
            DEFENCE,
            DEATH,
        }
        private CharecterState _state = CharecterState.NONE;
        protected CharecterState State { get { return _state; } set { _state = value; } }

        public bool IsKnockDown { get; protected set; }
        public bool IsKnockBack { get; protected set; }
        private void Awake()
        {
            Init();
        }
        protected virtual void None()
        {

        }
        protected virtual void Init()
        {
            Managers.UpdateManager.Instance.Listener(this.gameObject);
        }
        protected virtual void Idle()
        {

        }
        protected virtual void Move()
        {

        }
        protected virtual void Attack()
        {

        }
        protected virtual void Jump()
        {

        }
        protected virtual void Defence()
        {

        }
        protected virtual void Run()
        {
            ChangeState();
        }

        public virtual void ApplyDamage(float damage, bool knockDown)
        {

        }

        public virtual void ApplyKnockBack(float damage, bool knockDown)
        {

        }

        private void ChangeState()
        {
            if (_state == CharecterState.DEATH)
                return;

            switch (_state)
            {
                case CharecterState.NONE:
                    None();
                    break;
                case CharecterState.DEFENCE:
                    Defence();
                    break;
            }
        }

        public void Clear()
        {
            Managers.UpdateManager.Instance.DeleteListener(this.gameObject);
        }

        public void OnUpdate()
        {
            Run();
        }
    }
}

