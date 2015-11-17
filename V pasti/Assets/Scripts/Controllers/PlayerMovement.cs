using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;
    public float rotateSpeed = 90f;
    public float minX = 350f;
    public float maxX = 70f;
    private Vector3 movementVector;
    private Vector3 rotateVector;
    private float horizontal;
    private float vertical;

    private CharacterController control;
    private Animator animator;
    private Rigidbody rigid;

    void Start ()
    {
        //Priradenie a kontrola komponent
        control = GetComponent<CharacterController>();
        if (!control)
        {
            Debug.LogError("Missing controller!");
        }

        animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing animator!");
        }

        rigid = GetComponent<Rigidbody>();
        if (!rigid)
        {
            Debug.LogError("Missing rigidbody!");
        }
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (GetComponent<BasePlayer>().health > 0 && !GetComponent<BasePlayer>().pause)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            //Movement
            movementVector = new Vector3(horizontal, 0f, vertical);
            movementVector.Normalize();
            movementVector = transform.TransformDirection(movementVector);
            control.SimpleMove(movementVector * movementSpeed);


            //Rotation
            rotateVector = new Vector3(0f, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0f);
            transform.Rotate(rotateVector);
            rotateVector = new Vector3(-(Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime), 0f, 0f);

            float actualAngle = transform.Find("Camera Target").rotation.eulerAngles.x;
            float newAngle = actualAngle - (Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
            if(newAngle > 180)
            {
                newAngle -= 360;
            }

            if (newAngle > minX && newAngle < maxX)
                transform.Find("Camera Target").Rotate(rotateVector);

            Animate(horizontal, vertical);
        }
    }

    //Pohybove animacie
    void Animate (float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("runningForward", false);
            animator.SetBool("runningBack", false);
            animator.SetBool("runningLeft", false);
            animator.SetBool("runningRight", false);
            animator.SetBool("runningForwardLeft", false);
            animator.SetBool("runningForwardRight", false);
            animator.SetBool("runningBackLeft", false);
            animator.SetBool("runningBackRight", false);
        }
        else
        {
            animator.SetInteger("idleState", Random.Range(1, 30));
            animator.SetBool("isRunning", false);
            animator.SetBool("runningForward", false);
            animator.SetBool("runningBack", false);
            animator.SetBool("runningLeft", false);
            animator.SetBool("runningRight", false);
            animator.SetBool("runningForwardLeft", false);
            animator.SetBool("runningForwardRight", false);
            animator.SetBool("runningBackLeft", false);
            animator.SetBool("runningBackRight", false);
            return;
        }

        if(vertical > 0)
        {
            if(horizontal == 0)
            {
                animator.SetBool("runningForward", true);
            }
            else if(horizontal < 0)
            {
                animator.SetBool("runningForwardLeft", true);
            }
            else if (horizontal > 0)
            {
                animator.SetBool("runningForwardRight", true);
            }
        }
        else if(vertical < 0)
        {
            if (horizontal == 0)
            {
                animator.SetBool("runningBack", true);
            }
            else if (horizontal < 0)
            {
                animator.SetBool("runningBackLeft", true);
            }
            else if (horizontal > 0)
            {
                animator.SetBool("runningBackRight", true);
            }
        }
        else if(horizontal < 0)
        {
            animator.SetBool("runningLeft", true);
        }
        else if(horizontal > 0)
        {
            animator.SetBool("runningRight", true);
        }
    }
}
