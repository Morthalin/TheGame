using UnityEngine;
using System.Collections;

public class HealCast : MonoBehaviour
{
    public GameObject particle;
    private GameObject localParticle;
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

        timer = 0f;
        healing = false;
    }
    
    void Update ()
    {
        if (localParticle && !localParticle.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(localParticle);
        }

        if (basePlayer.health > 0 && !basePlayer.pause && !(basePlayer.attacking && !healing))
        {
            if (timer > 0f)
            {
                if (timer < 6.7f && basePlayer.attacking)
                {
                    if (basePlayer.healthMax < basePlayer.health + HealCalculation(basePlayer))
                    {
                        basePlayer.health = basePlayer.healthMax;
                    }
                    else
                    {
                        basePlayer.health += HealCalculation(basePlayer);
                    }
                    
                    localParticle = (GameObject)Instantiate(particle, GameObject.Find("Player").transform.position + new Vector3(0f, 8f, 0f), GameObject.Find("Player").transform.rotation);
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
                    timer = 10f;
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
