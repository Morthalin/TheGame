using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public int checkpoint;
    public Transform player;
    public Transform goblin;
    public Transform mainCamera;

    private bool cameraMove;

	void Awake ()
    {
        checkpoint = 0;
        cameraMove = false;

        if (!player)
        {
            Debug.LogError("Missing player!");
        }

        if (!goblin)
        {
            Debug.LogError("Missing goblin!");
        }

        if (!mainCamera)
        {
            Debug.LogError("Missing camera!");
        }
    }
	
	void Update ()
    {
	    switch(checkpoint)
        {
            case 1:
                if (player.GetComponent<BasePlayer>().pause == 0)
                {
                    player.GetComponent<BasePlayer>().pause++;
                    mainCamera.GetComponent<FollowTrackingCamera>().enabled = false;
                }
                else
                {
                    if (!cameraMove)
                    {
                        if (Vector3.Angle((goblin.position + new Vector3(0f, 5f, 0f)) - mainCamera.position, mainCamera.forward) > 1f)
                        {
                            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin.position + new Vector3(0f, 5f, 0f)) - mainCamera.position), Time.deltaTime * 2f);
                        }
                        else
                        {
                            cameraMove = true;
                        }
                    }
                    else
                    {
                        if((mainCamera.position - (goblin.position + new Vector3(0f, 5f, 0f))).sqrMagnitude > 65f)
                        {
                            if (Vector3.Angle((goblin.position + new Vector3(0f, 5f, 0f)) - mainCamera.position, mainCamera.forward) > 0f)
                            {
                                mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin.position + new Vector3(0f, 5f, 0f)) - mainCamera.position), Time.deltaTime * 20f);
                            }
                            mainCamera.position = mainCamera.position + mainCamera.TransformDirection(0f, 0f, Time.deltaTime * 20);
                        }
                        else
                        {
                            StartCoroutine(cameraWait());
                            cameraMove = false;
                            checkpoint++;
                        }
                    }
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
	}

    IEnumerator cameraWait()
    {
        //mainCamera.LookAt(goblin);
        yield return new WaitForSeconds(1f);
        goblin.LookAt(player);
        goblin.FindChild("FloatingDamageText").FindChild("Text").GetComponent<Text>().text = "!";
        goblin.FindChild("FloatingDamageText").FindChild("Text").GetComponent<Animator>().SetTrigger("Hit");
        goblin.GetComponent<NavMeshAgent>().SetDestination(transform.FindChild("GoblinCheckpoint1").position);
        yield return new WaitForSeconds(5f);
        mainCamera.position = mainCamera.parent.position;
        mainCamera.rotation = mainCamera.parent.rotation;
        mainCamera.GetComponent<FollowTrackingCamera>().enabled = true;
        player.GetComponent<BasePlayer>().pause--;

    }
}
