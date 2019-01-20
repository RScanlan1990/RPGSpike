using System.Collections;
using UnityEngine;

public class DecalDestroyer : MonoBehaviour {

    private float lifeTime = 5.0f;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}
}
