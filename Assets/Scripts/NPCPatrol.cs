using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    private int currentWaypointIndex = 0;
    private LineOfSight lineOfSight;

    void Start()
    {
        lineOfSight = GetComponent<LineOfSight>();
    }

    void Update()
    {
        if (lineOfSight.CanSeePlayer())
        {
            MoveTo(lineOfSight.lastKnownPlayerPosition);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0)
            return;

        Transform currentWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0;
        }
    }

    void MoveTo(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
    }
}
