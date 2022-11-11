using UnityEngine;

namespace Suf.Utils
{
    public static class GameObjectUtils
    {
        public static void SetParent(GameObject obj, GameObject parent, bool worldPositionStays = false, bool withReset = false)
        {
            obj.transform.SetParent(parent.transform, worldPositionStays);
            if (withReset) ResetTransform(obj, worldPositionStays);
        }

        public static void ResetTransform(GameObject obj, bool isGlobal = false)
        {
            if (isGlobal)
            {
                obj.transform.position = Vector3.zero;
                obj.transform.eulerAngles = Vector3.zero;
            }
            else
            {
                obj.transform.localPosition =  Vector3.zero;
                obj.transform.localEulerAngles = Vector3.zero;
            }

            obj.transform.localScale = Vector3.one;
        }
    }
}