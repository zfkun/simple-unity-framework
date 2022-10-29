using Suf.Utils;
using UnityEngine;

/// <summary>
/// 移动类型
/// </summary>
public enum MoveType
{
    Mathf,
    CharacterController,
    Rigidbody,
}


/// <summary>
/// 数学移动算法
/// </summary>
public enum MathfMoveType
{
    Mathf,
    Translate,
    MoveTowards,
}

/// <summary>
/// 角色控制器移动算法
/// </summary>
public enum CharacterControllerMoveType
{
    Move,
    SimpleMove,
}

/// <summary>
/// 刚体移动算法
/// </summary>
public enum RigidbodyMoveType
{
    Velocity,
    AddForce,
    MovePosition,
}

public class MoveAble : MonoBehaviour
{
    [Header("移动方式")]
    public MoveType moveType;

    [Header("移动方式算法")]
    public MathfMoveType mathfMoveType;
    public CharacterControllerMoveType characterControllerMoveType;
    public RigidbodyMoveType rigidbodyMoveType;
    
    [Header("速度配置")]
    public float rotationSpeed;
    public float moveSpeed;
    
    [Header("外部组件")]
    public CharacterController cc;
    public Rigidbody rb;
    // public CapsuleCollider cd;

    private void Start()
    {
        // moveType = MoveType.Mathf;
        mathfMoveType = MathfMoveType.Mathf;
        characterControllerMoveType = CharacterControllerMoveType.Move;
        rigidbodyMoveType = RigidbodyMoveType.Velocity;
    }

    private void Awake()
    {
        if (moveType == MoveType.CharacterController)
        {
            if (cc == null) cc = GetComponent<CharacterController>();
            if (cc == null)
            {
                cc = gameObject.AddComponent<CharacterController>();
                cc.center = new Vector3(0f, 0f, 0);
                cc.radius = 0.86f;
                cc.height = 2.2f;
            }
        }
        else if (moveType == MoveType.Rigidbody)
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY
                                 | RigidbodyConstraints.FreezeRotationX
                                 | RigidbodyConstraints.FreezeRotationY
                                 | RigidbodyConstraints.FreezeRotationZ;
                // rb.drag = 10f;
            }
            
            var cd = GetComponent<CapsuleCollider>();
            if (cd == null)
            {
                cd = gameObject.AddComponent<CapsuleCollider>();
                cd.center = new Vector3(0, 0, 0);
                cd.radius = 0.5f;
                cd.height = 2f;
            }
        }
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (h == 0 && v == 0) return;

        LogUtils.InfoFormat("axis: h={0}, v={1}", h, v);

        // 1. 旋转
        var dir = new Vector3(h, 0, v);
        var target = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp( transform.rotation,target, Time.deltaTime * rotationSpeed);

        // 2. 移动
        switch (moveType)
        {
            case MoveType.Mathf:
                MoveByMathf();
                break;
            case MoveType.CharacterController:
                MoveByCharacterController();
                break;
            case MoveType.Rigidbody:
                MoveByRigidbody();
                break;
        }
    }

    // 简单
    //
    // 仅需数学位移, 无视障碍物
    private void MoveByMathf()
    {
        switch (mathfMoveType)
        {
            case MathfMoveType.Mathf:
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
                break;
            case MathfMoveType.Translate:
                transform.Translate(transform.forward * Time.deltaTime * moveSpeed, Space.World);
                break;
            case MathfMoveType.MoveTowards:
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    transform.position + transform.forward * Time.deltaTime * moveSpeed, 
                    Time.deltaTime * moveSpeed);
                break;
        }
    }

    // 较常用
    //
    // 有阻挡障碍物需求
    // 有触发器接收事件需求
    private void MoveByCharacterController()
    {
        // 建议每帧只调用一次 Move 或 SimpleMove
        switch (characterControllerMoveType)
        {
            // 1. 无限制移动 (X轴、Y轴、Z轴)
            // 2. 不带重力效果 (需要自己实现) (可自行增加重力效果)
            // 3. 速度以 帧 计算, 需乘以 deltaTime
            // 4. 返回 CollisionFlags 对象 (角色与任何物体的碰撞信息)
            // cc.Move(dir.normalized * Time.deltaTime * speed);
            case CharacterControllerMoveType.Move:
                cc.Move(-transform.up * Time.deltaTime * moveSpeed); // 模拟重力效果
                cc.Move(transform.forward * Time.deltaTime * moveSpeed);
                break;

            // 1. 只能在平面移动, 忽略Y轴移动 (只沿 X轴、Z轴 移动)
            // 2. 自带重力效果 (重力自动施加)
            // 3. 速度以 米/秒 计算, 不需要乘以 deltaTime
            // 4. 角色接触地面, 则返回 (接触地面返回 true, 否则返回 false)
            // cc.SimpleMove(dir.normalized * speed);
            case CharacterControllerMoveType.SimpleMove:
                cc.SimpleMove(transform.forward * moveSpeed);
                break;
        }
    }

    // 较少使用
    //
    // 有阻挡障碍物需求
    // 有施力效果需求
    // 有碰撞器接收事件需求
    private void MoveByRigidbody()
    {
        switch (rigidbodyMoveType)
        {
            // 按秒移动
            case RigidbodyMoveType.Velocity:
                rb.velocity = transform.forward * moveSpeed;
                break;
            // 按秒移动
            case RigidbodyMoveType.AddForce:
                rb.AddForce(transform.forward * moveSpeed);
                break;
            // 按帧移动
            case RigidbodyMoveType.MovePosition:
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed);
                break;
        }
    }
}
