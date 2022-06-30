using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Managers
{
    class UpdateManager : Manager<UpdateManager>
    {
        public delegate void DelegateUpdate();
        public event DelegateUpdate _onUpdate;
        public override void Init()
        {
            
        }
        //public void KnockBackListener(GameObject go)
        //{
        //    _onUpdate += go.GetComponent<IUpdate>().OnKnockBack;
        //}
        //public void KnockBackDeleteListener(GameObject go)
        //{
        //    _onUpdate -= go.GetComponent<IUpdate>().OnKnockBack;
        //}

        public void Listener(GameObject go)
        {
            _onUpdate += go.GetComponent<IUpdate>().OnUpdate;
        }

        public void DeleteListener(GameObject go)
        {
            _onUpdate -= go.GetComponent<IUpdate>().OnUpdate;
        }

        private void Update()
        {
           if(null != _onUpdate)
                _onUpdate();
        }
    }
}