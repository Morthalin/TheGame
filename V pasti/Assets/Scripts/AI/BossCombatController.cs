using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class BossCombatController : MonoBehaviour
{
    public float agroReset = 3000;
    private GameObject target;
    private BasePlayer targetScript;
    private BaseNPC baseNPC;
    private CharacterController controller;
    private Rigidbody rigid;
    private Animator animator;
    private Animator targetAnimator;
    private bool deadNPC;
    private bool goingHome;
    private Vector3 initPosition;
    private Quaternion initRotation;
    private float initDistance;
    private float targetDistance;
    private Vector3 movementVector;
    private float speed;
    private float minAgro;

    void Awake ()
    {
        //Korenova pozicia a rotacia
        initPosition = transform.position;
        initRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        deadNPC = false;
        goingHome = false;
        speed = 8;
        minAgro = 800;

        if (GameObject.Find("Player"))
        {
            target = GameObject.Find("Player");
            targetAnimator = target.GetComponent<Animator>();
            if (!targetAnimator)
            {
                Debug.LogError("Missing player animator!");
            }

            targetScript = target.GetComponent<BasePlayer>();
            if (!targetScript)
            {
                Debug.LogError("Missing BasePlayer script on player character!");
            }
        }
        else
        {
            Debug.LogError("No player founded!");
        }

        //Priradenie a kontrola komponent
        controller = GetComponent<CharacterController>();
        if (!controller)
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
        rigid.constraints = RigidbodyConstraints.FreezeAll;

        baseNPC = GetComponent<BaseNPC>();
        if (!baseNPC)
        {
            Debug.LogError("Missing BaseNPC script!");
        }
    }
	
	void LateUpdate ()
    {
        if (baseNPC.health > 0)
        {
            deadNPC = false;
        }

        if (baseNPC.health <= 0)
        {
            //smrt NPC
            if (baseNPC.health <= 0 && !deadNPC)
            {
                deadNPC = true;
                animator.SetBool("death", true);
                animator.SetTrigger("die");
                animator.SetBool("combat", false);
                baseNPC.inCombat = false;
                targetAnimator.SetBool("isCombat", false);
                transform.Find("HPFrame").gameObject.SetActive(false);

                // pridani mrtvoli
                Loot.corpseList.Add(new Corpse(baseNPC.creatureName, baseNPC.transform.position));

                //vypnuti collideru
                gameObject.GetComponent<CharacterController>().enabled = false;
            }
        }
        else if (targetScript.health <= 0 && !targetScript.dead)
        {
            //smrt hraca
            targetScript.dead = true;
            goingHome = true;
            targetAnimator.SetBool("death", true);
            targetAnimator.SetTrigger("die");
            animator.SetBool("combat", false);
            baseNPC.inCombat = false;
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
                    
                    animator.SetBool("running", true);
                    controller.SimpleMove(movementVector * speed);
                    initDistance = (initPosition - transform.position).sqrMagnitude;
                }
            }
            else if (initDistance > agroReset)
            {
                //Koniec Combatu
                goingHome = true;
                baseNPC.health = baseNPC.healthMax;
                animator.SetBool("combat", false);
                baseNPC.inCombat = false;
                targetAnimator.SetBool("isCombat", false);
                transform.Find("HPFrame").gameObject.SetActive(false);
            }
            else if (targetDistance < minAgro && !targetScript.dead)
            {
                //Beh ku hracovy
                transform.LookAt(target.transform);
                transform.Find("HPFrame").gameObject.SetActive(true);

                animator.SetBool("combat", true);
                baseNPC.inCombat = true;
                targetAnimator.SetBool("isCombat", true);
                initDistance = (initPosition - transform.position).sqrMagnitude;
            }
            else
            {
                //Idle
                animator.SetBool("running", false);
            }
        }
    }
}
