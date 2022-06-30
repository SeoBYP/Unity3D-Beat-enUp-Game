using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    class PoolingManager : Manager<PoolingManager>
    {
        #region Pool
        class Pooling
        {
            public GameObject Original { get; private set; }
            public Transform Root { get; set; }

            //Stack<Poolable> _poolStack = new Stack<Poolable>();
            List<Poolable> _poolList = new List<Poolable>();
            public void Init(GameObject original, int count = 10)
            {
                Original = original;
                Root = new GameObject().transform;
                Root.name = $"{original.name}_Root";
                for (int i = 0; i < count; i++)
                {
                    Push(Create());
                }
            }

            Poolable Create()
            {
                GameObject go = Object.Instantiate<GameObject>(Original);
                go.name = Original.name;
                return go.GetOrAddComponent<Poolable>();
            }

            public void Push(Poolable poolable)
            {
                if (poolable == null)
                    return;

                poolable.transform.parent = Root;
                poolable.gameObject.SetActive(false);
                poolable.IsUsing = false;
                _poolList.Add(poolable);
            }
            public Poolable Pop(Transform parent)
            {
                Poolable poolable = null;
                for (int i = 0; i < _poolList.Count; i++)
                {
                    if (_poolList[i].IsUsing == false)
                    {
                        poolable = _poolList[i];
                        poolable.Active();
                        break;
                    }
                }
                if (poolable == null)
                {
                    Poolable newpoolable = Create();
                    if (newpoolable != null)
                    {
                        _poolList.Add(newpoolable);
                        poolable = newpoolable;
                    }
                }
                return poolable;
            }
        }
        #endregion

        Dictionary<string, Pooling> _pooling = new Dictionary<string, Pooling>();
        Transform _root;

        public override void Init()
        {
            if (_root == null)
            {
                _root = new GameObject { name = "@Pool_Root" }.transform;
                Object.DontDestroyOnLoad(_root);
            }
        }

        public void CreatePool(GameObject original, int count = 10)
        {
            Pooling pooling = new Pooling();
            pooling.Init(original, count);
            pooling.Root.parent = _root;

            _pooling.Add(original.name, pooling);
        }

        public void Push(Poolable poolable)
        {
            string name = poolable.gameObject.name;
            if (_pooling.ContainsKey(name) == false)
            {
                GameObject.Destroy(poolable.gameObject);
                return;
            }
            _pooling[name].Push(poolable);
        }

        public Poolable Pop(GameObject original, Transform parent = null)
        {
            if (_pooling.ContainsKey(original.name) == false)
            {
                CreatePool(original);
            }
            return _pooling[original.name].Pop(parent);
        }

        public GameObject GetOriginal(string name)
        {
            if (_pooling.ContainsKey(name) == false)
                return null;
            return _pooling[name].Original;
        }

        public void Clear()
        {
            foreach (Transform child in _root)
            {
                GameObject.Destroy(child.gameObject);
            }
            _pooling.Clear();
        }
    }
}

