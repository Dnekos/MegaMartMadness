using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pathfinding;

public enum StatusConditions
{
    Slow,
    Conveyor,
    Stunned,
    Slippery
}


public class Movement : MonoBehaviour
{
   
    public Vector2 inputVector;

    [Header("Adjustables")]
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

    [Header("Alternate Movement")]
    [SerializeField]
    bool TankControls;
    [SerializeField]
    bool ReverseControls;

    Vector2 newspeed;
    float newturnspeed;
    float target_degree;
    [SerializeField]
    int reverse_allowance = 25;
    public float reverse;

    private void Start()
    {
        temp_drag = 1f;
        temp_speed = 1f;
    }

    private void FixedUpdate()
    {
        //sets the area just a bit bigger than the collider to default tag
        GraphUpdateObject bigguo = new GraphUpdateObject(new Bounds(GetComponent<Collider2D>().bounds.center, 
            GetComponent<Collider2D>().bounds.extents*1.1f));
        bigguo.modifyTag = true;
        bigguo.setTag = 0;
        AstarPath.active.UpdateGraphs(bigguo);

        //Debug.Log(GetComponent<Collider2D>().bounds.center + " || " + GetComponent<Collider2D>().bounds.extents * 1.1f);

        //sets the area in the collider to P1 tag
        GraphUpdateObject guo = new GraphUpdateObject(new Bounds(GetComponent<Collider2D>().bounds.center,
            GetComponent<Collider2D>().bounds.extents));
        guo.modifyTag = true;
        guo.setTag = 1;
        AstarPath.active.UpdateGraphs(guo);

        if (TankControls)
        {
            //gives the speed 
            speed += inputVector.y * (acceleration * Time.deltaTime) * temp_speed;

            speed = Mathf.Clamp(speed, maxSpeed * -1, maxSpeed);
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
        else if (!ReverseControls)
        {
            //gives the speed 
            newspeed += inputVector * (acceleration * Time.deltaTime) * temp_speed;

            Vector2.ClampMagnitude(newspeed, maxSpeed);

            target_degree = (Mathf.Atan2(-newspeed.x, newspeed.y) * Mathf.Rad2Deg + 360) % 360;
            //if (currkey)
             bool fob = !(Mathf.DeltaAngle(transform.eulerAngles.z, target_degree) > 180-reverse_allowance);
            Debug.Log(Mathf.DeltaAngle(transform.eulerAngles.z + 180, target_degree));
            Debug.Log(fob);
            if (fob)
                transform.Translate(Vector2.up * newspeed.magnitude, Space.Self);
            else
                transform.Translate(Vector2.down * newspeed.magnitude, Space.Self);

            //gives it the drag
            newspeed = newspeed * Drag * temp_drag;

            if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
                speed = 0;

            float angle;
            newturnspeed += inputVector.magnitude * turnAcceleration * Time.deltaTime * temp_speed;

            if (fob)
                angle = Mathf.LerpAngle(transform.eulerAngles.z, target_degree, newturnspeed);
            else
                angle = Mathf.LerpAngle(transform.eulerAngles.z, target_degree + 180, newturnspeed);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newturnspeed *= Drag * temp_drag;
        }
        else
        {
            //gives the speed 
            newspeed += inputVector * (acceleration * Time.deltaTime) * temp_speed;

            Vector2.ClampMagnitude(newspeed, maxSpeed);

            target_degree = (Mathf.Atan2(-newspeed.x, newspeed.y) * Mathf.Rad2Deg + 360) % 360;
            Debug.Log(Mathf.DeltaAngle(transform.eulerAngles.z + 180, target_degree));


            if (reverse == 0)
                transform.Translate(Vector2.up * newspeed.magnitude, Space.Self);
            else
                transform.Translate(Vector2.down * newspeed.magnitude, Space.Self);


            //gives it the drag
            newspeed = newspeed * Drag * temp_drag;

            if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
                speed = 0;

            float angle;
            newturnspeed += inputVector.magnitude * turnAcceleration * Time.deltaTime * temp_speed;

            angle = Mathf.LerpAngle(transform.eulerAngles.z, target_degree, newturnspeed);
            if (inputVector != Vector2.zero)
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newturnspeed *= Drag * temp_drag;
        }
    }
}
