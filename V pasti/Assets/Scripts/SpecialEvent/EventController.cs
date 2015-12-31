using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public Transform player;
    public Transform goblin;
    public Transform goblin1;
    public Transform mainCamera;
    public Transform bridge;

    private bool cameraMove;
    private bool secondTarget;
    private BasePlayer basePlayer;

	void Awake ()
    {
        cameraMove = false;
        secondTarget = false;

        if (!player)
        {
            Debug.LogError("Missing player!");
        }
        else
        {
            basePlayer = player.GetComponent<BasePlayer>();
            if(!basePlayer)
            {
                Debug.LogError("Missing base player script!");
            }
        }

        if (!goblin)
        {
            Debug.LogError("Missing goblin!");
        }

        if (!goblin1)
        {
            Debug.LogError("Missing goblin1!");
        }

        if (!mainCamera)
        {
            Debug.LogError("Missing camera!");
        }
    }
	
	void Update ()
    {
	    switch(basePlayer.storyCheckpoint)
        {
            case 10:
                if (player.GetComponent<BasePlayer>().pause == 0)
                {
                    player.GetComponent<BasePlayer>().pause++;
                    mainCamera.GetComponent<FollowTrackingCamera>().enabled = false;
                }
                else
                {
                    if (!cameraMove)
                    {
                        if (Vector3.Angle((goblin.position + new Vector3(0f,3f, 0f)) - mainCamera.position, mainCamera.forward) > 1f)
                        {
                            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin.position + new Vector3(0f, 3f, 0f)) - mainCamera.position), Time.deltaTime * 2f);
                        }
                        else
                        {
                            cameraMove = true;
                        }
                    }
                    else
                    {
                        if((mainCamera.position - (goblin.position + new Vector3(0f, 3f, 0f))).sqrMagnitude > 65f)
                        {
                            if (Vector3.Angle((goblin.position + new Vector3(0f, 3f, 0f)) - mainCamera.position, mainCamera.forward) > 0f)
                            {
                                mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin.position + new Vector3(0f, 3f, 0f)) - mainCamera.position), Time.deltaTime * 20f);
                            }
                            mainCamera.position = mainCamera.position + mainCamera.TransformDirection(0f, 0f, Time.deltaTime * 40);
                        }
                        else
                        {
                            StartCoroutine(cameraWait1());
                            cameraMove = false;
                            basePlayer.storyCheckpoint++;
                        }
                    }
                }
                break;
            case 13:
                if (player.GetComponent<BasePlayer>().pause == 0)
                {
                    player.GetComponent<BasePlayer>().pause++;
                    mainCamera.GetComponent<FollowTrackingCamera>().enabled = false;
                }
                else
                {
                    if (!cameraMove)
                    {
                        if (Vector3.Angle((goblin1.position + new Vector3(0f, 2f, 0f)) - mainCamera.position, mainCamera.forward) > 1f)
                        {
                            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin1.position + new Vector3(0f, 2f, 0f)) - mainCamera.position), Time.deltaTime * 2f);
                        }
                        else
                        {
                            cameraMove = true;
                        }
                    }
                    else
                    {
                        if ((mainCamera.position - (goblin1.position + new Vector3(0f, 2f, 0f))).sqrMagnitude > 65f)
                        {
                            if (Vector3.Angle((goblin1.position + new Vector3(0f, 2f, 0f)) - mainCamera.position, mainCamera.forward) > 0f)
                            {
                                mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin1.position + new Vector3(0f, 2f, 0f)) - mainCamera.position), Time.deltaTime * 20f);
                            }
                            if ((mainCamera.position - (goblin1.position + new Vector3(0f, 2f, 0f))).sqrMagnitude > 150f)
                            {
                                mainCamera.position += mainCamera.TransformDirection(0f, 0f, Time.deltaTime * 100);
                            }
                            else
                            {
                                mainCamera.position += mainCamera.TransformDirection(0f, 0f, Time.deltaTime * 30);
                            }
                        }
                        else
                        {
                            StartCoroutine(cameraWait2());
                            cameraMove = false;
                            basePlayer.storyCheckpoint++;
                        }
                    }
                }
                break;
            case 15:
                if (player.GetComponent<BasePlayer>().pause == 0)
                {
                    player.GetComponent<BasePlayer>().pause++;
                    mainCamera.GetComponent<FollowTrackingCamera>().enabled = false;
                    cameraMove = true;
                    secondTarget = false;
                    goblin1.GetComponent<Animator>().SetBool("Attacking", false);
                    destroyBridge();
                }
                else
                {
                    if (cameraMove)
                    {
                        if ((mainCamera.position - transform.FindChild("BridgeCameraTarget").position).sqrMagnitude > 20f && !secondTarget)
                        {
                            mainCamera.LookAt(transform.FindChild("BridgeLookAt"));
                            mainCamera.position += (transform.FindChild("BridgeCameraTarget").position - mainCamera.position).normalized * Time.deltaTime * 30;
                        }
                        else
                        {
                            if(!secondTarget)
                                secondTarget = true;

                            if((mainCamera.position - goblin1.position).sqrMagnitude > 65f)
                            {
                                if (Vector3.Angle((goblin1.position + new Vector3(0f, 2f, 0f)) - mainCamera.position, mainCamera.forward) > 0f)
                                {
                                    mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, Quaternion.LookRotation((goblin1.position + new Vector3(0f, 2f, 0f)) - mainCamera.position), Time.deltaTime * 10f);
                                }
                                mainCamera.position += mainCamera.TransformDirection(0f, 0f, Time.deltaTime * 50);
                            }
                            else
                            {
                                goblin1.GetComponent<Animator>().SetTrigger("dance");
                                cameraMove = false;
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(cameraWait3());
                        cameraMove = false;
                        basePlayer.storyCheckpoint = 20;
                    }
                }
                break;
            default:
                break;
        }
	}

    IEnumerator cameraWait1()
    {
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

    IEnumerator cameraWait2()
    {
        goblin1.FindChild("FloatingDamageText").FindChild("Text").GetComponent<Text>().text = "!";
        goblin1.FindChild("FloatingDamageText").FindChild("Text").GetComponent<Animator>().SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);
        goblin1.GetComponent<Animator>().SetBool("Attacking", true);
        yield return new WaitForSeconds(3f);
        mainCamera.position = mainCamera.parent.position;
        mainCamera.rotation = mainCamera.parent.rotation;
        mainCamera.GetComponent<FollowTrackingCamera>().enabled = true;
        player.GetComponent<BasePlayer>().pause--;
    }

    IEnumerator cameraWait3()
    {
        yield return new WaitForSeconds(10f);
        freezeBridge();
        mainCamera.position = mainCamera.parent.position;
        mainCamera.rotation = mainCamera.parent.rotation;
        mainCamera.GetComponent<FollowTrackingCamera>().enabled = true;
        player.GetComponent<BasePlayer>().pause--;
    }

    private void destroyBridge()
    {
        Transform bone;
        bone = bridge.FindChild("Bone001");

        while (bone.childCount != 0)
        {
            bone = bone.GetChild(0);
            bone.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void freezeBridge()
    {
        Transform bone;
        bone = bridge.FindChild("Bone001");

        while (bone.childCount != 0)
        {
            bone = bone.GetChild(0);
            bone.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
