using UnityEngine;
using UnityEngine.AI;

public class NavMeshHelper
{
    public Vector3 GetRandomPositionWithArea(Vector3 origin, Vector2 area)
    {
        var randomPosition = origin + RandomPositionWithinArea(area);
        var navMeshHit = new NavMeshHit();
        if (NavMesh.SamplePosition(randomPosition, out navMeshHit, 0.5f, NavMesh.AllAreas))
        {
            return randomPosition;
        }
        return Vector3.zero;
    }

    private Vector3 RandomPositionWithinArea(Vector2 area)
    {
        var modifierX = Random.Range(0, 2) * 2 - 1;
        var modifierZ = Random.Range(0, 2) * 2 - 1;
        return new Vector3(Random.Range(0.0f, area.x) * modifierX, 0, Random.Range(0.0f, area.y) * modifierZ);
    }
}
