using UnityEngine;
using System.Collections;

public class DestroyAfterPlayed : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(waiting());
	}

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
