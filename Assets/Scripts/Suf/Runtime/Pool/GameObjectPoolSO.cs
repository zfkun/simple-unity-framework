using UnityEngine;

namespace Suf.Pool
{
    public abstract class GameObjectPoolSO<T> : PoolSO<GameObject>
    {
        private Transform _poolRoot;
        private Transform PoolRoot
        {
            get
            {
                if (_poolRoot == null)
                {
                    _poolRoot = new GameObject(typeof(T).Name).transform;
                    _poolRoot.SetParent(_parent);
                }
                return _poolRoot;
            }
        }

        private Transform _parent;

        public void SetParent(Transform t)
        {
            _parent = t;
            PoolRoot.SetParent(_parent);
        }

        public override GameObject Request()
        {
            var member = base.Request();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(GameObject member)
        {
            member.transform.SetParent(PoolRoot.transform);
            member.gameObject.SetActive(false);
            base.Return(member);
        }

        protected override GameObject Create()
        {
            var newMember = base.Create();
            newMember.transform.SetParent(PoolRoot.transform);
            newMember.gameObject.SetActive(false);
            return newMember;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (_poolRoot != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(_poolRoot.gameObject);
#else
				Destroy(_poolRoot.gameObject);
#endif
            }
        }
    }
}