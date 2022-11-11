using UnityEngine;

namespace Suf.UI.Filter
{
    public class UIAlwaysRaycast: MonoBehaviour, ICanvasRaycastFilter
    {
        
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
                return true;
        }
    }
}