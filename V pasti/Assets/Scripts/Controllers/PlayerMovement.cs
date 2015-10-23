using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    private Vector3 movementVector;
    private Vector3 rotateVector;
    private float horizontal;
    private float vertical;

    private CharacterController control;
    private Animator animator;

    void Start ()
    {
        control = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
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

        Animate(horizontal, vertical);
    }

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
