using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    //public DynamicJoystick dynamicJoystick;
    private Rigidbody rb;
    private PlayerState player;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerState>();
        speed = player.speed;
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        transform.position = transform.position + 0.05f*direction;
        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.forward);
            GetComponent<Animator>().SetBool("IsRunning", false);
        }
    }
}