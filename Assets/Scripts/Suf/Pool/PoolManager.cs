using System.Collections.Generic;

using UnityEngine;

using Suf.Base;
using Suf.Resource;
using Suf.Utils;

namespace Suf.Pool
{
    public class PoolManager: UnitySingletonAuto<PoolManager>
    {
        private readonly Dictionary<string, Stack<GameObject>> m_Pool = new Dictionary<string, Stack<GameObject>>();
        private readonly Dictionary<string, bool> m_IsPrewarm = new Dictionary<string, bool>();

        private Transform m_PoolRoot;
        private Transform poolRoot
        {
            get
            {
                if (m_PoolRoot != null) return m_PoolRoot;
                m_PoolRoot = new GameObject($"{name}Root").transform;
                if (m_Parent != null) m_PoolRoot.SetParent(m_Parent);
                return m_PoolRoot;
            }
        }
    
        private Transform m_Parent;
        public Transform parent
        {
            get => m_Parent;
            set
            {
                m_Parent = value;
                poolRoot.SetParent(m_Parent);
            }
        }

        private GameObject Create(string key)
        {
            var newMember = Instantiate(AddressableManager.Instance.LoadWait<GameObject>(key));

            GameObjectUtils.SetParent(newMember, poolRoot.gameObject);
            newMember.name = key;
            newMember.gameObject.SetActive(false);
        
            return newMember;
        }
    
        public virtual void Prewarm(string key, int num)
        {
            if (!m_IsPrewarm.ContainsKey(key)) m_IsPrewarm[key] = false;

            if (m_IsPrewarm[key])
            {
                Debug.LogWarning($"Pool {name} with key {key} has already been prewarmed.");
                return;
            }

            if (!m_Pool.ContainsKey(key)) m_Pool[key] = new Stack<GameObject>();

            for (var i = 0; i < num; i++) m_Pool[key].Push(Create(key));

            m_IsPrewarm[key] = true;
        }

        public virtual GameObject Request(string key)
        {
            GameObject member;
            if (m_Pool.ContainsKey(key) && m_Pool[key].Count > 0)
            {
                member = m_Pool[key].Pop();
            }
            else
            {
                member = Create(key);
            }

            member.SetActive(true);
            return member;
        }

        public virtual IEnumerable<GameObject> Request(string key, int num)
        {
            num = num > 1 ? num : 1;

            var members = new Stack<GameObject>(num);
            for (var i = 0; i < num; i++) members.Push(Request(key));
            return members;
        }
    
        public virtual void Return(string key, GameObject member)
        {
            GameObjectUtils.SetParent(member, poolRoot.gameObject);
            member.gameObject.SetActive(false);

            if (!m_Pool.ContainsKey(key)) m_Pool.Add(key, new Stack<GameObject>());
            m_Pool[key].Push(member);
        }
    
        public virtual void Return(string key, IEnumerable<GameObject> members)
        {
            foreach (var member in members) Return(key, member);
        }

        public virtual void OnDisable()
        {
            m_Pool.Clear();

            if (m_PoolRoot != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(m_PoolRoot.gameObject);
#else
                Destroy(m_PoolRoot.gameObject);
#endif
            }
        }
    }
}
