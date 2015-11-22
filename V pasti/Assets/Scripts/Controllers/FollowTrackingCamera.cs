using UnityEngine;
using System.Collections;

public class FollowTrackingCamera : MonoBehaviour
{
    public Transform target;
    public float distanceWanted = 20f;
    public float minZoom = 0f;
    public float maxZoom = 30f;
    public float zoomStep = 15f;
    public float zoomSpeed = 5f;
    private float distance;

    private Vector3 zoomResult;
    private Vector3 targetAdjustedPosition;
    private Transform cameraCollider;

    void Start()
    {
        distance = distanceWanted;
        if (!target)
        {
            Debug.LogError("Nie je nastaveny target kamery.");
        }

        cameraCollider = transform.parent.Find("Camera Collider");
        if(!cameraCollider)
        {
            Debug.LogError("Nie je nastaveny Camera Collider");
        }
    }

    void FixedUpdate()
    {
        if (!GameObject.Find("Player").GetComponent<BasePlayer>().pause)
        {
            // Nacitanie polohy mysky
            distanceWanted -= Input.GetAxis("Mouse ScrollWheel") * zoomStep;

            // Orezanie na limit

            distanceWanted = Mathf.Clamp(distanceWanted, minZoom, maxZoom);
            cameraCollider.localPosition = new Vector3(0f, 0f, -distanceWanted);
            distance = Mathf.Lerp(distance, distanceWanted, Time.deltaTime * zoomSpeed);

            // Zoom na kolizii
            RaycastHit hit;
            int layerMask = 1 << 8;
            if (Physics.Linecast(target.position, cameraCollider.position, out hit, layerMask))
            {
                distance = hit.distance;
            }
            zoomResult = new Vector3(0f, 0f, -distance);
            
            // Nastavenie posunu
            transform.localPosition = zoomResult;
            //transform.LookAt(target);
        }
    }
}
