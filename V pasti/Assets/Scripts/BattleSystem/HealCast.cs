using UnityEngine;
using System.Collections;

public class HealCast : MonoBehaviour
{
    public GameObject particle;
    private GameObject localParticle;
    public GameObject krigel;
    private GameObject localKrigel;
    private Animator animator;
    private float timer;
    private BasePlayer basePlayer;
    private bool healing;

    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing animator!");
        }

        basePlayer = GameObject.Find("Player").GetComponent<BasePlayer>();
        if (!basePlayer)
        {
            Debug.LogError("Missing BasePlayer script!");
        }

        if(!particle)
        {
            Debug.LogError("Missing particle system!");
        }

        if(!krigel)
        {
            Debug.LogError("Missing efect object!");
        }

        timer = 0f;
        healing = false;
    }
    
    void Update ()
    {
        if (localParticle && !localParticle.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(localParticle);
            Destroy(localKrigel);
        }

        if (basePlayer.health > 0 && basePlayer.pause == 0 && !(basePlayer.attacking && !healing))
        {
            if (timer > 0f)
            {
                if (timer < 2.5f && basePlayer.attacking)
                {
                    if (basePlayer.healthMax < basePlayer.health + HealCalculation(basePlayer))
                    {
                        basePlayer.health = basePlayer.healthMax;
                    }
                    else
                    {
                        basePlayer.health += HealCalculation(basePlayer);
                    }

                    Vector3 eAngle = new Vector3(180f, 0f, 90f) + GameObject.Find("Player").transform.rotation.eulerAngles;
                    Vector3 pos = GameObject.Find("Player").transform.TransformPoint(new Vector3(10f, 20f, 0f));
                    Quaternion rot = Quaternion.Euler(eAngle);
                    localKrigel = (GameObject)Instantiate(krigel, pos, rot);
                    localKrigel.transform.SetParent(GameObject.Find("Player").transform);
                    eAngle = new Vector3(0f, 270f, 0f) + GameObject.Find("Player").transform.rotation.eulerAngles;
                    rot = Quaternion.Euler(eAngle);
                    pos = GameObject.Find("Player").transform.TransformPoint(new Vector3(8f, 20f, 0f));
                    localParticle = (GameObject)Instantiate(particle, pos, rot);
                    localParticle.transform.SetParent(GameObject.Find("Player").transform);
                    basePlayer.attacking = false;
                    healing = false;
                    basePlayer.activeArmor = basePlayer.armor;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    animator.SetTrigger("HealCast");
                    basePlayer.attacking = true;
                    healing = true;
                    basePlayer.activeArmor /= 2;
                    timer = 5f;
                }
            }
        }
	}

    int HealCalculation(BasePlayer player)
    {
        int heal = player.intellect * 20;
        return heal;
    }
}
