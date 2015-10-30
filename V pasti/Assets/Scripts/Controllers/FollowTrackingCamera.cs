using UnityEngine;
using System.Collections;

public class FollowTrackingCamera : MonoBehaviour
{
    public Transform target;
    
    public float height = 20f;
    public float distance = 20f;
    public float minZoom = 0f;
    public float maxZoom = 30f;
    public float minHeight = -2f;
    public float maxHeight = 15f;
    public float rotateSpeedX = 1f;
    public float rotateSpeedY = 100f;
    public bool doRotate;
    public bool doZoom;
    public float zoomStep = 15f;
    public float zoomSpeed = 5f;
    private float heightWanted;
    private float distanceWanted;

    private Vector3 zoomResult;
    private Quaternion rotationResult;
    private Vector3 targetAdjustedPosition;

    void Start()
    {
        heightWanted = height;
        distanceWanted = distance;
        zoomResult = new Vector3(0f, height, -distance);
    }

    void LateUpdate()
    {
        if (!target)
        {
            Debug.LogError("Nie je nastaveny target kamery.");
            return;
        }

        // Zoom a posun po Y ose
        if (doZoom)
        {
            // Nacitanie polohy mysky
            heightWanted -= Input.GetAxis("Mouse Y") * rotateSpeedY * 0.01f;
            distanceWanted -= Input.GetAxis("Mouse ScrollWheel") * zoomStep;

            // Orezanie na limit
            
            heightWanted = Mathf.Clamp(heightWanted, minHeight, maxHeight);
            distanceWanted = Mathf.Clamp(distanceWanted, minZoom, maxZoom);
            height = Mathf.Lerp(height, heightWanted, Time.deltaTime * zoomSpeed);
            distance = Mathf.Lerp(distance, distanceWanted, Time.deltaTime * zoomSpeed);

            // Zoom na kolizii
            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                if (!hit.collider.CompareTag("Player") || !hit.collider.CompareTag("Enemy"))
                {
                    zoomResult = new Vector3(0f, height, -(hit.distance));
                }
            }
            else
            {
                zoomResult = new Vector3(0f, height, -distance);
            }
        }

        // Rotacia po X
        if (doRotate)
        {
            float currentRotationAngle = transform.eulerAngles.y;
            float wantedRotationAngle = target.eulerAngles.y;

            // Zjemnenie rotacie
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotateSpeedX * Time.deltaTime);

            // Poslanie vysledku
            rotationResult = Quaternion.Euler(0f, currentRotationAngle, 0f);
        }

        // Nastavenie posunu
        targetAdjustedPosition = rotationResult * zoomResult;
        transform.position = target.position + targetAdjustedPosition;
        
        
        // Pohlad na target
        transform.LookAt(target);
    }
}
