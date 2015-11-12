using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BasicAttack : MonoBehaviour
{
    private Animator animator;
    private float timer;
    private BasePlayer basePlayer;
    private bool hitted;

    void Start ()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        if(!animator)
        {
            Debug.LogError("Missing animator!");
        }

        basePlayer = GameObject.Find("Player").GetComponent<BasePlayer>();
        if (!basePlayer)
        {
            Debug.LogError("Missing BasePlayer script!");
        }

        timer = 0f;
        hitted = false;
	}
	
	void Update ()
    {
        if (basePlayer.health > 0)
        {
            RaycastHit hit;

            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    animator.SetTrigger("Attack1");
                    hitted = false;
                    timer = 1.5f;
                }
            }
            int layerMask = 1 << 9;
            if (Physics.Linecast(transform.position, transform.parent.position, out hit, layerMask))
            {
                if (!hitted)
                {
                    hit.transform.gameObject.GetComponent<BaseNPC>().health -= DamageCalculation();
                    hit.transform.gameObject.GetComponent<Animator>().SetTrigger("damage");
                    HPBarChange(hit.transform.gameObject);
                    hitted = true;
                }
            }
        }
    }

    int DamageCalculation ()
    {
        int damage = 100;

        //TODO chybajuci prepocet statou na damage

        return damage;
    }

    void HPBarChange(GameObject target)
    {
        float percentage = (float)target.GetComponent<BaseNPC>().health / (float)target.GetComponent<BaseNPC>().healthMax;

        target.transform.Find("HPFrame").transform.Find("HPBar").GetComponent<Image>().fillAmount = percentage;
        target.transform.Find("HPFrame").transform.Find("HPBar").transform.Find("Text").GetComponent<Text>().text = target.GetComponent<BaseNPC>().health.ToString() + "/" + target.GetComponent<BaseNPC>().healthMax.ToString();
    }
}
