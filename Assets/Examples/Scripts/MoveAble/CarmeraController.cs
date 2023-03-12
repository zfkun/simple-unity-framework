using UnityEngine;
using UnityEngine.EventSystems;

public class CarmeraController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Transform cameraTransform;
    public Transform target;
    public float smooth = 2f;
    
    private Vector3 distance;

    private void Start()
    {
        // 相机 -> 玩家 方向向量
        distance = target.position - cameraTransform.position;
    }

    private void Update()
    {
        Scale();
    }

    void LateUpdate()
    {
        var position = target.position;
        
        // transform.position = Vector3.Lerp(position - distance, position, Time.deltaTime * smooth);
        // transform.LookAt(position);
        
        cameraTransform.position = position - distance;
    }

    private void Scale()
    {
        var d = distance.magnitude;
        d -= Input.GetAxis("Mouse ScrollWheel") * 5;
        if (d < 10 || d > 40) return;

        distance = distance.normalized * d;
    }

    

    #region 视角处理

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta.normalized;
        if (delta.x == 0 && delta.y == 0) return;
        
        var position = target.position;
        // cameraTransform.RotateAround(position, Vector3.up, 1 * delta.x);
        // cameraTransform.RotateAround(position, Vector3.left, 1 * delta.y);

        var x = cameraTransform.localEulerAngles.x - delta.y * 0.5f;
        var y = cameraTransform.localEulerAngles.y + delta.x * 0.5f;

        if (x <= 80 || x >= 315)
        {
            cameraTransform.rotation =  Quaternion.Euler(x, y,0);
        }
        else if (x > 80 && x < 315)
        {
            cameraTransform.rotation =  Quaternion.Euler(cameraTransform.localEulerAngles.x, y,0);
        }
        
        
        // 更新 跟随距离
        distance = position - cameraTransform.position;
    }
    
    #endregion
}
