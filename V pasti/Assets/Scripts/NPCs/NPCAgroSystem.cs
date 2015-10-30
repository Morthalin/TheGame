using UnityEngine;
using System.Collections;

public class NPCAgroSystem : BaseNPC
{
    public float speed = 8;
    public float minAgro = 800;
    public float agroReset = 2400;
    private Vector3 initPosition;
    private Transform target;
    private GameObject[] inRangeObjects;
    private CharacterController controller;
    private Animator animator;
    private Animator targetAnimator;

    private bool goingHome;
    private Vector3 movementVector;
    private float initDistance;
    private float targetDistance;

    void Start ()
    {
        //Korenova pozicia
        initPosition = transform.position;
        goingHome = false;

        //Vyhladanie hraca
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0)
        {
            target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            targetAnimator = target.GetComponent<Animator>();
        } else
        {
            Debug.LogError("No player founded!");
        }

        //Priradenie a kontrola komponent
        controller = GetComponent<CharacterController>();
        if(!controller)
        {
            Debug.LogError("Missing controller!");
        }

        animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing animator!");
        }
    }
	
	void LateUpdate ()
    {
        //Vzdialenost od korenovej pozicie
        initDistance = (initPosition - transform.position).sqrMagnitude;
        //Vzdialenost od hraca
        targetDistance = (target.position - transform.position).sqrMagnitude;

        if (goingHome == true)
        {
            GetComponent<Rigidbody>().useGravity = false;
            if (initDistance < 1f)
            {
                goingHome = false;
            }
            else
            {
                transform.LookAt(initPosition);
                movementVector = new Vector3(0f, 0f, 1f);
                movementVector = transform.TransformDirection(movementVector);
                
                animator.SetBool("isRunning", true);
                animator.SetBool("runningForward", true);
                controller.SimpleMove(movementVector * speed);
                initDistance = (initPosition - transform.position).sqrMagnitude;
            }
        }
        else  if (initDistance > agroReset)
        {
            //Koniec Combatu
            goingHome = true;
            animator.SetBool("isCombat", false);
            targetAnimator.SetBool("isCombat", false);
        }
        else if (targetDistance < minAgro && targetDistance >= 10)
        {
            //Beh ku hracovy
            transform.LookAt(target);
            movementVector = new Vector3(0f, 0f, 1f);
            movementVector = transform.TransformDirection(movementVector);

            animator.SetBool("isCombat", true);
            targetAnimator.SetBool("isCombat", true);
            animator.SetBool("isRunning", true);
            animator.SetBool("runningForward", true);
            controller.SimpleMove(movementVector * speed);
            initDistance = (initPosition - transform.position).sqrMagnitude;
        } else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("runningForward", false);
        }
    }
}
