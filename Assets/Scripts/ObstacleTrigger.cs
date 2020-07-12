using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    [SerializeField]
    StatusConditions ObstacleType;
    [SerializeField]
    float ObstacleModifier = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Movement mover = collision.GetComponent<Movement>();
            switch (ObstacleType)
            {
                case StatusConditions.Slow:
                    mover.temp_speed*= ObstacleModifier;
                    break;
                case StatusConditions.Slippery:
                    mover.temp_drag *= ObstacleModifier;
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {//conveyor needs to be in stay as it acts on the object every frame
        if (collision.tag == "Player" && ObstacleType == StatusConditions.Conveyor)
        {
            collision.transform.Translate(transform.up * ObstacleModifier * Time.deltaTime, Space.World);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Movement mover = collision.GetComponent<Movement>();
            switch (ObstacleType)
            {
                case StatusConditions.Slow:
                    mover.temp_speed /= ObstacleModifier;
                    break;
                case StatusConditions.Slippery:
                    mover.temp_drag /= ObstacleModifier;
                    break;
                case StatusConditions.Conveyor:
                  //  collision.attachedRigidbody.AddForce(transform.up * ObstacleModifier*2);
                    break;
            }
        }
    }
}
