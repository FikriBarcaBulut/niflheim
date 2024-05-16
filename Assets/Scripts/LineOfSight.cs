using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public float visionRange = 10f;
    private Transform player;
    public bool canSeePlayer { get; private set; }
    public Vector3 lastKnownPlayerPosition { get; private set; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player tag is "Player"
    }

    void Update()
    {
        canSeePlayer = CheckLineOfSight();
    }

    bool CheckLineOfSight()
    {
        if (player == null)
            return false;

        Vector3 direction = player.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, visionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                lastKnownPlayerPosition = player.position;
                return true;
            }
        }
        return false;
    }

    public bool CanSeePlayer()
    {
        return canSeePlayer;
    }
}
