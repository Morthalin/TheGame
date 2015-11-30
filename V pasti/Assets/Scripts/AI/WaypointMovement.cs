using UnityEngine;
using System.Collections;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float[] movementSpeeds;
    public float[] rotationSpeeds;
    public float distanceToNext = 10f;
    private bool moving;
    private bool turning;
    public bool singleMovementSpeed = true;
    public bool singleRotationSpeed = true;
    public bool stepEnter = false;
    public bool smooth = true;
    private int position;
    private bool wait;

    void Awake()
    {
        moving = false;
        turning = false;
        wait = true;
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
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || !wait)
            {
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
        if ((transform.position - waypoints[position].position).sqrMagnitude < distanceToNext)
        {
            position++;
            moving = false;
            turning = false;
        }
        else
        {
            if (singleMovementSpeed)
            {
                transform.position = transform.position + transform.TransformDirection(0f, 0f, movementSpeeds[0] * Time.deltaTime);
            }
            else
            {
                transform.position = transform.position + transform.TransformDirection(0f, 0f, movementSpeeds[position] * Time.deltaTime);
            }
        }
    }

    void Turn()
    {
        float angle = Vector3.Angle(waypoints[position].position - transform.position, transform.forward);
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
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(waypoints[position].position - transform.position), Time.deltaTime * rotationSpeeds[0]);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(waypoints[position].position - transform.position), Time.deltaTime * rotationSpeeds[position]);
            }
        }
    }

    void nextWaypoint()
    {
        if (transform.parent.name == "Camera Target" && position == 0)
        {
            transform.parent.parent.GetComponent<BasePlayer>().pause++;
        }

        if (position == waypoints.Length)
        {
            if (transform.parent.name == "Camera Target")
            {
                transform.parent.parent.GetComponent<BasePlayer>().pause--;
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
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
