using UnityEngine;
using System.Collections;

public class Events: MonoBehaviour
{
    public void moveForward()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (!controller)
        {
            Debug.LogError("Missing controller!");
        }
        controller.SimpleMove(new Vector3(0f, 0f, 1f) * 2f);
    }
}
