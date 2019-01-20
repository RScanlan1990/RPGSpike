using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 position)
    {
        var direction = DirectionToClick(position);
        if (NavMesh.SamplePosition(transform.position + direction, out var navMeshHit, 5.0f, NavMesh.AllAreas))
        {
            Debug.DrawRay(navMeshHit.position, Vector3.up, Color.red);
            _navMeshAgent.SetDestination(navMeshHit.position);
        }  
    }

    private Vector3 DirectionToClick(Vector3 clickPosition)
    {
        var heading = clickPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance * 0.4f;
        return direction;
    }

    public void LookAtPosition(Vector3 position)
    {
        transform.LookAt(position);
    }
}
