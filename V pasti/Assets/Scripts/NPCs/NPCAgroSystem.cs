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
    private Rigidbody rigid;
    private Animator animator;
    private Animator targetAnimator;

    private bool goingHome;
    private bool deadNPC;
    private bool deadPlayer;
    private Vector3 movementVector;
    private float initDistance;
    private float targetDistance;
    private float timer;
    private bool hitted;

    void Start ()
    {
        //Korenova pozicia a rotacia
        initPosition = transform.position;
        initRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        goingHome = false;
        deadNPC = false;
        deadPlayer = false;
        timer = 0f;
        hitted = false;

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

        rigid = GetComponent<Rigidbody>();
        if(!rigid)
        {
            Debug.LogError("Missing rigidbody!");
        }
        rigid.constraints = RigidbodyConstraints.FreezeAll;

        baseNPC = GetComponent<BaseNPC>();
        if (!baseNPC)
        {
            Debug.LogError("Missing BaseNPC script!");
        }
    }
	
	void FixedUpdate ()
    {
        if (baseNPC.health <= 0)
        {
            //smrt NPC
            if (baseNPC.health <= 0 && !deadNPC)
            {
                deadNPC = true;
                animator.SetTrigger("die");
                animator.SetBool("death", true);
                animator.SetBool("isCombat", false);
                targetAnimator.SetBool("isCombat", false);
                transform.Find("HPFrame").gameObject.SetActive(false);
            }
        }
        else if (targetScript.health <= 0 && !deadPlayer)
        {
            //smrt hraca
            deadPlayer = true;
            goingHome = true;
            targetAnimator.SetTrigger("die");
            targetAnimator.SetBool("death", true);
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
                baseNPC.health = baseNPC.healthMax;
                animator.SetBool("isCombat", false);
                targetAnimator.SetBool("isCombat", false);
                transform.Find("HPFrame").gameObject.SetActive(false);
            }
            else if (targetDistance < minAgro && targetDistance > 20f && !deadPlayer)
            {
                //Beh ku hracovy
                transform.LookAt(target.transform);
                transform.Find("HPFrame").gameObject.SetActive(true);
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

            if (targetDistance <= 25f && !deadPlayer)
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
                if(timer <= 1f && !hitted)
                {
                    hitted = true;
                    targetAnimator.SetTrigger("damage");
                }
            }
            else
            {
                //perioda utoku
                timer = 2f;
                hitted = false;
                int damage = (Random.Range(baseNPC.attackMin, baseNPC.attackMax) - targetScript.armor);
                if(damage > 0)
                    targetScript.health -= damage;
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
