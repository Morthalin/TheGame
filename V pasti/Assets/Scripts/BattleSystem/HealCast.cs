using UnityEngine;
using UnityEngine.UI;
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
    public float cooldown = 30f;
    public Button cooldownIndicator;
    public int energy = 50;
    public GameObject healText;
    private GameObject localText;

    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing animator!");
        }

        if(!healText)
        {
            Debug.LogError("Missing healText!");
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
        
        if (!cooldownIndicator)
        {
            Debug.LogError("Missing cooldown button!");
        }

        if (!krigel)
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

        if (basePlayer.health > 0)
        {
            if (timer > 0f)
            {
                cooldownIndicator.GetComponent<Image>().fillAmount = timer / cooldown;
                if (timer < cooldown - 3f && healing)
                {
                    if (basePlayer.healthMax < basePlayer.health + HealCalculation(basePlayer))
                    {
                        basePlayer.health = basePlayer.healthMax;
                    }
                    else
                    {
                        basePlayer.health += HealCalculation(basePlayer);
                    }
                    localText = (GameObject)Instantiate(healText, GameObject.Find("Interface").transform.FindChild("HPBar").FindChild("HealPosition").position, GameObject.Find("Interface").transform.FindChild("HPBar").FindChild("HealPosition").rotation);
                    localText.transform.SetParent(GameObject.Find("Interface").transform.FindChild("HPBar").FindChild("HealPosition"));
                    localText.GetComponent<Text>().text = HealCalculation(basePlayer).ToString();
                    localText.GetComponent<Animator>().SetTrigger("Hit");

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
                cooldownIndicator.GetComponent<Image>().fillAmount = 0;
                if (Input.GetKeyDown(KeyCode.Q) && basePlayer.energy >= energy && basePlayer.pause == 0 && !basePlayer.attacking && !healing)
                {
                    basePlayer.energy -= energy;
                    animator.SetTrigger("HealCast");
                    basePlayer.attacking = true;
                    healing = true;
                    basePlayer.activeArmor /= 2;
                    timer = cooldown;
                    cooldownIndicator.GetComponent<Image>().fillAmount = 1;
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
