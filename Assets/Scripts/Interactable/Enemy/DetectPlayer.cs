using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            enemy.TrySetTarget(other.gameObject, null);
        }
    }
}
