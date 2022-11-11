using UnityEngine;

using Suf.Base;
using Suf.Event;

namespace Suf.Input
{
    public class InputManager: UnitySingletonAuto<InputManager>
    {
        private bool _isStart;

        public void ToggleCheck(bool start)
        {
            _isStart = start;
        }

        private void CheckKeyCode(KeyCode key)
        {
            if (UnityEngine.Input.GetKeyDown(key))
            {
                EventManager.Instance.Emit(InputType.KeyDown, key);
            }

            if (UnityEngine.Input.GetKeyUp(key))
            {
                EventManager.Instance.Emit(InputType.KeyUp, key);
            }
        }

        private void CheckAxisRaw(string axisName)
        {
            EventManager.Instance.Emit(InputType.AxisRaw, axisName, UnityEngine.Input.GetAxisRaw(axisName));
        }

        private void Update()
        {
            if (!_isStart) return;
            
            // CheckKeyCode(KeyCode.W);
            // CheckKeyCode(KeyCode.A);
            // CheckKeyCode(KeyCode.S);
            // CheckKeyCode(KeyCode.D);

            CheckAxisRaw("Horizontal");
            CheckAxisRaw("Vertical");
            
            CheckKeyCode(KeyCode.U);
            CheckKeyCode(KeyCode.I);
            CheckKeyCode(KeyCode.J);
            CheckKeyCode(KeyCode.K);
        }
    }
}