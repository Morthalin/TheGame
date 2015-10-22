using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    private Vector3 movementVector;
    private Vector3 rotateVector;

    private CharacterController control;

    void Start ()
    {
        control = GetComponent<CharacterController>();
    }

	void Update ()
    {
        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementVector = new Vector3(horizontal, 0f, vertical);
        movementVector.Normalize();
        movementVector = transform.TransformDirection(movementVector);
        control.SimpleMove(movementVector * movementSpeed);


        //Rotation
        rotateVector = new Vector3(0f, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0f);
        transform.Rotate(rotateVector);
    }
}
