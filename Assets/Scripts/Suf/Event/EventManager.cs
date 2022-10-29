using System;
using System.Collections.Generic;

namespace Suf.Event
{
    public class EventManager: Base.Singleton<EventManager>
    {
        private readonly Dictionary<Enum, List<Delegate>> _events = new Dictionary<Enum, List<Delegate>>();

        #region 订阅

        public void On(Enum type, Action action)
        {
            if (!_events.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _events.Add(type, list);
            }
            list.Add(action);
        }
        
        public void On<T>(Enum type, Action<T> action)
        {
            if (!_events.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _events.Add(type, list);
            }
            list.Add(action);
        }
        
        public void On<T1, T2>(Enum type, Action<T1, T2> action)
        {
            if (!_events.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _events.Add(type, list);
            }
            list.Add(action);
        }
        
        public void On<T1, T2, T3>(Enum type, Action<T1, T2, T3> action)
        {
            if (!_events.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _events.Add(type, list);
            }
            list.Add(action);
        }
        
        public void On<T1, T2, T3, T4>(Enum type, Action<T1, T2, T3, T4> action)
        {
            if (!_events.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _events.Add(type, list);
            }
            list.Add(action);
        }
        
        #endregion


        #region 取消订阅
        
        public void Off(Enum type)
        {
            if (_events.TryGetValue(type, out _))
            {
                _events.Remove(type);
            }
        }
        
        public void Off(Enum type, Action action)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            if (list.Contains(action))
            {
                list.Remove(action);
            }

            if (list.Count <= 0)
            {
                Off(type);
            }
        }
        
        public void Off<T>(Enum type, Action<T> action)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            if (list.Contains(action))
            {
                list.Remove(action);
            }

            if (list.Count <= 0)
            {
                Off(type);
            }
        }
        
        public void Off<T1, T2>(Enum type, Action<T1, T2> action)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            if (list.Contains(action))
            {
                list.Remove(action);
            }

            if (list.Count <= 0)
            {
                Off(type);
            }
        }
        
        public void Off<T1, T2, T3>(Enum type, Action<T1, T2, T3> action)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            if (list.Contains(action))
            {
                list.Remove(action);
            }

            if (list.Count <= 0)
            {
                Off(type);
            }
        }
        
        public void Off<T1, T2, T3, T4>(Enum type, Action<T1, T2, T3, T4> action)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            if (list.Contains(action))
            {
                list.Remove(action);
            }

            if (list.Count <= 0)
            {
                Off(type);
            }
        }
        
        #endregion


        #region 通知

        public void Emit(Enum type)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            foreach (var v in list)
            {
                (v as Action)?.Invoke();
            }
        }
        
        public void Emit<T>(Enum type, T arg)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            foreach (var v in list)
            {
                (v as Action<T>)?.Invoke(arg);
            }
        }
        
        public void Emit<T1, T2>(Enum type, T1 arg1, T2 arg2)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            foreach (var v in list)
            {
                (v as Action<T1, T2>)?.Invoke(arg1, arg2);
            }
        }
        
        public void Emit<T1, T2, T3>(Enum type, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            foreach (var v in list)
            {
                (v as Action<T1, T2, T3>)?.Invoke(arg1, arg2, arg3);
            }
        }
        
        public void Emit<T1, T2, T3, T4>(Enum type, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!_events.TryGetValue(type, out var list)) return;
            
            foreach (var v in list)
            {
                (v as Action<T1, T2, T3, T4>)?.Invoke(arg1, arg2, arg3, arg4);
            }
        }

        #endregion
    }
}