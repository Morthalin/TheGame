using UnityEngine;
using System.Collections;

public class Events: MonoBehaviour
{
    int ticks = 0;
    bool casting = false;
    public GameObject fireball;

    public void CastFireball()
    {
        if (!casting)
        {
            casting = true;
            
            Animator animator = transform.GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("Missing mage Animator!");
            }

            animator.SetTrigger("castFireball");
            StartCoroutine(Fireball());
        }
    }

    public void FireballTick()
    {
        if(ticks != 0)
        {
            Transform player = GameObject.Find("Player").transform;
            if (!player)
            {
                Debug.LogError("Missing Player!");
            }

            Animator playerAnimator = player.GetComponent<Animator>();
            if (!playerAnimator)
            {
                Debug.LogError("Missing player Animator!");
            }

            playerAnimator.SetTrigger("damage");
            player.GetComponent<BasePlayer>().health -= 20;
            ticks--;
        }
    }

    public void CastBlink()
    {
        if (!casting)
        {
            casting = true;
            StartCoroutine(Blink());
        }
    }



    //Casove funkcie


    IEnumerator Fireball()
    {
        GameObject localFireball;
        Vector3 movementVector;
        Transform player = GameObject.Find("Player").transform;
        if (!player)
        {
            Debug.LogError("Missing Player!");
        }
        Transform initFireball = transform.FindChild("InitFireball");
        if (!initFireball)
        {
            Debug.LogError("Missing Fireball prefab!");
        }
        Animator playerAnimator = player.GetComponent<Animator>();
        if (!playerAnimator)
        {
            Debug.LogError("Missing player Animator!");
        }

        yield return new WaitForSeconds(2);
        localFireball = (GameObject)Instantiate(fireball, initFireball.position, initFireball.rotation);
        while ((localFireball.transform.position - player.position).sqrMagnitude > 5f)
        {
            localFireball.transform.rotation = Quaternion.Lerp(localFireball.transform.rotation, Quaternion.LookRotation(player.position - localFireball.transform.position), 1f);
            movementVector = localFireball.transform.TransformDirection(0f, 0f, 2f);
            localFireball.transform.position += movementVector;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(localFireball);
        playerAnimator.SetTrigger("damage");
        player.GetComponent<BasePlayer>().health -= 200;
        ticks = 10;
        casting = false;
    }

    IEnumerator Blink()
    {
        Transform[] blinkTargets = GameObject.Find("Mages_cave").transform.FindChild("BlinkTargets").GetComponentsInChildren<Transform>();
        Transform player = GameObject.Find("Player").transform;
        if (!player)
        {
            Debug.LogError("Missing Player!");
        }
        Animator animator = transform.GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing Animator!");
        }

        if ((transform.position - player.position).sqrMagnitude < 50)
        {
            animator.SetTrigger("castBlink");
            yield return new WaitForSeconds(2);
            transform.FindChild("BlinkEffect").GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(0.3f);
            transform.position = blinkTargets[Random.Range(0, blinkTargets.Length - 1)].position;
        }
        casting = false;
    }
}
