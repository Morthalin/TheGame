using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 100f;
    public float rotateSpeed = 50f;
    private Vector3 movementVector;
    private Vector3 rotateVector;
    private float cameraRotX = 0f;
    public float cameraPitchMax = 45f;

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
        control.SimpleMove(movementVector * movementSpeed * Time.deltaTime);


        //Rotation
        rotateVector = new Vector3(0f, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0f);
        if(Input.GetAxis("Mouse X") != 0f)
            Debug.LogError(Input.GetAxis("Mouse X").ToString());
        transform.Rotate(rotateVector);
        //cameraRotX -= Input.GetAxis("Mouse Y");

        //cameraRotX = Mathf.Clamp(cameraRotX, -cameraPitchMax, cameraPitchMax);

        //Camera.main.transform.forward = transform.forward;
        //Camera.main.transform.Rotate(cameraRotX, 0f, 0f);

    }
}
