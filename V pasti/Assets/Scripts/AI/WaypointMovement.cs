using UnityEngine;
using System.Collections;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float[] movementSpeeds;
    public float[] rotationSpeeds;
    public Transform[] targets;
    public float distanceToNext = 10f;
    private bool moving;
    private bool turning;
    public bool singleMovementSpeed = true;
    public bool singleRotationSpeed = true;
    public bool stepEnter = false;
    public bool smooth = true;
    private int position;
    public bool wait;
    public bool start;

    void Awake()
    {
        moving = false;
        turning = false;
        wait = true;
        start = false;
        position = 0; 

        if(waypoints.Length != movementSpeeds.Length && !singleMovementSpeed)
        {
            Debug.LogError("Wrong count of movementSpeeds!");
        }

        if (waypoints.Length != rotationSpeeds.Length && !singleRotationSpeed)
        {
            Debug.LogError("Wrong count of movementSpeeds!");
        }
    }

    void Update()
    {
        if (!moving && !turning)
        {
            if (start || !wait || Input.GetKeyDown(KeyCode.T))
            {
                start = false;
                wait = stepEnter;
                nextWaypoint();
            }
        }

        if(turning)
        {
            Turn();
        }

        if (moving)
        {
            Move();
        }
    }

    void Move()
    {
        if ((transform.parent.position - waypoints[position].position).sqrMagnitude < distanceToNext)
        {
            position++;
            moving = false;
            turning = false;
        }
        else
        {
            if (targets[position])
            {
                transform/*.parent*/.LookAt(targets[position]);
            }
            else
            {
                transform/*.parent*/.LookAt(waypoints[position]);
            }

            if (singleMovementSpeed)
            {
                transform.parent.position = transform.parent.position + transform.parent.TransformDirection(0f, 0f, movementSpeeds[0] * Time.deltaTime);
            }
            else
            {
                transform.parent.position = transform.parent.position + transform.parent.TransformDirection(0f, 0f, movementSpeeds[position] * Time.deltaTime);
            }
        }
    }

    void Turn()
    {
        float angle = Vector3.Angle(waypoints[position].position - transform.parent.position, transform.parent.forward);
        if (angle < 1f)
        {
            if (smooth)
            {
                turning = true;
                moving = true;
            }
            else
            {
                turning = false;
                moving = true;
            }
        }
        else
        {
            if (singleRotationSpeed)
            {
                transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.LookRotation(waypoints[position].position - transform.parent.position), Time.deltaTime * rotationSpeeds[0]);
            }
            else
            {
                transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.LookRotation(waypoints[position].position - transform.parent.position), Time.deltaTime * rotationSpeeds[position]);
            }
        }
    }

    void nextWaypoint()
    {
        //if (transform.parent.parent.name == "Camera Target" && position == 0)
        //{
        //    transform.parent.parent.parent.GetComponent<BasePlayer>().pause++;
        //}

        if (position == waypoints.Length)
        {
            //if (transform.parent.parent.name == "Camera Target")
            //{
            //    transform.parent.parent.parent.GetComponent<BasePlayer>().pause--;
            //    transform.parent.localEulerAngles = new Vector3(0f, 0f, 0f);
            //}
            wait = true;
            enabled = false;
        }
        else
        {
            if (smooth)
            {
                turning = true;
                moving = true;
            }
            else
            {
                turning = true;
                moving = false;
            }
        }
    }
}
