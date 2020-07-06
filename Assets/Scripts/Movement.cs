using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum StatusConditions
{
    Slow,
    Conveyor,
    Stunned,
    Slippery
}


public class Movement : MonoBehaviour
{
    public int items = 0;

    public Vector2 inputVector;
    public float grab = 0;

    [SerializeField]
    float Drag;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed = 10f;
    [SerializeField]
    float minSpeed;    
    [SerializeField]
    float turnAcceleration;

    float speed;
    float turnSpeed;
    float velocity;

    //condition stuff   
    [HideInInspector]
    public float temp_drag = 1f;   
    [HideInInspector]
    public float temp_speed = 1f;
    [HideInInspector]
    public Vector2 temp_translate = Vector2.zero;

    private void Start()
    {
        temp_drag = 1f;
        temp_speed = 1f;
    }

    private void FixedUpdate()
    {
        Debug.Log("drag: " + temp_drag+", speed"+temp_speed);
        //gives the speed 
        speed += inputVector.y * (acceleration * Time.deltaTime) * temp_speed;

        if (Mathf.Abs(speed) > maxSpeed)
            speed = maxSpeed;
        transform.Translate(Vector2.up * speed, Space.Self);

        //gives it the drag
        speed = speed * Drag * temp_drag;
        if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
            speed = 0;

        turnSpeed += inputVector.x * turnAcceleration * Time.deltaTime * temp_speed;

        transform.Rotate(Vector3.back * turnSpeed);
        turnSpeed *= Drag * temp_drag;
        if (speed < 0 && turnSpeed < 0)
            turnSpeed = Mathf.Abs(turnSpeed) * -1;
        else if (speed < 0 && turnSpeed > 0)
            turnSpeed = Mathf.Abs(turnSpeed);
        if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
            speed = 0;
    }
}
