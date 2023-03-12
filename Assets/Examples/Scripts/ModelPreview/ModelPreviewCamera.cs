using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPreviewCamera : MonoBehaviour
{
    public Transform model; //当前需要展示的模型

    public float distance = 10.0f; //模型距离相机的距离

    public float minDistance = 3f; //最小距离

    public float maxDistance = 18f; //最大距离

    public float x_Speed = 200.0f; //X轴方向的旋转速度

    public float y_Speed = 200.0f; //Y轴方向的旋转速度

    public float zoomSpeed = 1f; //缩放速度

    public bool isAllowYTilt = true; //是否允许Y轴倾斜

    public float minYLimit = -90f; //Y轴最小倾斜角度

    public float maxYLimit = 90f; //Y轴最大倾斜角度

    private float xPoint = 0.0f; //X轴坐标

    private float yPoint = 0.0f; //Y轴坐标

    private float xTargetPoint = 0f; //目标X轴坐标

    private float yTargetPoint = 0f; //目标Y轴坐标

    private float xVelocityPoint = 1f; //X水平轴坐标

    private float yVelocityPoint = 1f; //Y水平轴坐标

    private float zoomVelocity = 1f;

    public Vector3 pivotOffset = Vector3.zero; //位置偏移

    public float targetDistance;

    private void Start()

    {
        xTargetPoint = xPoint = transform.eulerAngles.x; //赋值

        yTargetPoint = yPoint = ClampAngle(transform.eulerAngles.y, minYLimit, maxYLimit);

        targetDistance = distance;
    }


    private void LateUpdate()

    {
        if (!model) return;

        var scroll = Input.GetAxis("Mouse ScrollWheel"); //获取鼠标滚轮值

        //根据鼠标滚轮的方向来确定模型于相机的距离

        if (scroll > 0.0f) targetDistance -= zoomSpeed;

        else if (scroll < 0.0f)

            targetDistance += zoomSpeed;

        //使用这个数学函数可以限制最大最小距离

        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        if (Input.GetMouseButton(0))

        {
            xTargetPoint += Input.GetAxis("Mouse X") * x_Speed * 0.025f;

            if (isAllowYTilt) //Y轴是否可以旋转

            {
                yTargetPoint -= Input.GetAxis("Mouse Y") * y_Speed * 0.025f;

                yTargetPoint = ClampAngle(yTargetPoint, minYLimit, maxYLimit);
            }
        }

        //Mathf.SmoothDampAngle函数可以起到一个缓冲作用，好处就是拖动旋转的时候可以更加平滑

        xPoint = Mathf.SmoothDampAngle(xPoint, xTargetPoint, ref xVelocityPoint, 0.3f);

        yPoint = isAllowYTilt ? Mathf.SmoothDampAngle(yPoint, yTargetPoint, ref yVelocityPoint, 0.3f) : yTargetPoint;

        //旋转角度设置

        Quaternion rotation = Quaternion.Euler(yPoint, xPoint, 0);

        distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.4f);

        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + model.position + pivotOffset;

        //坐标赋值

        transform.position = position;

        transform.rotation = rotation;
    }

    ///获取旋转角度
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;

        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}