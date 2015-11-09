using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class NPCAgroSystem : BaseNPC
{
    public float speed = 8;
    public float minAgro = 800;
    public float agroReset = 2400;
    private Vector3 initPosition;
    private Quaternion initRotation;
    private GameObject target;
    private BasePlayer targetScript;
    private BaseNPC baseNPC;
    private GameObject[] inRangeObjects;
    private CharacterController controller;
    private Rigidbody rigidbody;
    private Animator animator;
    private Animator targetAnimator;

    private bool goingHome;
    private bool deadNPC;
    private bool deadPlayer;
    private Vector3 movementVector;
    private float initDistance;
    private float targetDistance;
    private float timer;

    void Start ()
    {
        //Korenova pozicia a rotacia
        initPosition = transform.position;
        initRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        goingHome = false;
        deadNPC = false;
        deadPlayer = false;
        timer = 0f;

        //Vyhladanie hraca
        if (GameObject.Find("Player"))
        {
            target = GameObject.Find("Player");
            targetAnimator = target.GetComponent<Animator>();
            if(!targetAnimator)
            {
                Debug.LogError("Missing player animator!");
            }

            targetScript = target.GetComponent<BasePlayer>();
            if (!targetScript)
            {
                Debug.LogError("Missing BasePlayer script on player character!");
            }
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

        rigidbody = GetComponent<Rigidbody>();
        if(!rigidbody)
        {
            Debug.LogError("Missing rigidbody!");
        }
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        baseNPC = GetComponent<BaseNPC>();
        if (!baseNPC)
        {
            Debug.LogError("Missing BaseNPC script!");
        }
    }
	
	void LateUpdate ()
    {
        if (baseNPC.health <= 0)
        {
            //smrt NPC
            if (baseNPC.health <= 0 && !deadNPC)
            {
                deadNPC = true;
                animator.SetTrigger("death");
                animator.SetBool("isCombat", false);
                targetAnimator.SetBool("isCombat", false);
            }
        }
        else if (targetScript.health <= 0 && !deadPlayer)
        {
            deadPlayer = true;
            goingHome = true;
            targetAnimator.SetTrigger("death");
            animator.SetBool("isCombat", false);
            targetAnimator.SetBool("isCombat", false);
        }
        else
        {
            //Vzdialenost od korenovej pozicie
            initDistance = (initPosition - transform.position).sqrMagnitude;
            //Vzdialenost od hraca
            targetDistance = (target.transform.position - transform.position).sqrMagnitude;

            if (goingHome == true)
            {
                GetComponent<Rigidbody>().useGravity = false;
                if (initDistance < 5f)
                {
                    transform.rotation = initRotation;
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
            else if (initDistance > agroReset)
            {
                //Koniec Combatu
                goingHome = true;
                animator.SetBool("isCombat", false);
                targetAnimator.SetBool("isCombat", false);
            }
            else if (targetDistance < minAgro && targetDistance > 10f && !deadPlayer)
            {
                //Beh ku hracovy
                transform.LookAt(target.transform);
                movementVector = new Vector3(0f, 0f, 1f);
                movementVector = transform.TransformDirection(movementVector);

                animator.SetBool("isCombat", true);
                targetAnimator.SetBool("isCombat", true);
                animator.SetBool("isRunning", true);
                animator.SetBool("runningForward", true);
                controller.SimpleMove(movementVector * speed);
                initDistance = (initPosition - transform.position).sqrMagnitude;
            }
            else
            {
                //Idle
                animator.SetBool("isRunning", false);
                animator.SetBool("runningForward", false);
            }

            if (targetDistance <= 15f && !deadPlayer)
            {
                //Atack
                Attack1();
            }
        }
    }

    void Attack1 ()
    {
        transform.LookAt(target.transform);
        
        if (targetScript.health > 0)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                //perioda utoku
                timer = 2f;
                targetScript.health -= Random.Range(baseNPC.attackMin, baseNPC.attackMax);
                animator.SetTrigger("Attack1");
            }
        }
        else
        {
            //hrac je mrtvy
            goingHome = true;
            animator.SetBool("isCombat", false);
            targetAnimator.SetBool("isCombat", false);
        }
    }
}
