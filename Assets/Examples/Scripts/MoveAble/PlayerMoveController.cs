using System;
using UnityEngine;

public class PlayerMoveController: MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    public float moveSpeed = 5.0f;
    
    public VariableJoystick variableJoystick;
    private CharacterController _cc;

    protected virtual void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (!_cc) return;
        
        var h = variableJoystick.Horizontal;
        var v = variableJoystick.Vertical;
        if (h == 0 && v == 0) return;
        
        // 1. 旋转
        var dir = new Vector3(h, 0, v);
        var target = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp( transform.rotation,target, Time.deltaTime * rotationSpeed);
        
        // Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        // rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        _cc.SimpleMove(transform.forward * moveSpeed);
    }
}