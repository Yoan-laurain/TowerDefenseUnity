using UnityEngine;

[RequireComponent(typeof(Ennemy))]
public class EnnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    private Ennemy ennemy;

    void Start()
    {
        target = WayPoints.points[0];
        ennemy = GetComponent<Ennemy>();
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * ennemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        ennemy.speed = ennemy.Initialspeed;
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = WayPoints.points[waypointIndex];
    }

    private void EndPath()
    {
        //Destruction de l'ennemi
        Destroy(gameObject);

        // On enlève une vie
        PlayerStat.lives--;
    }
}
