using UnityEngine;

[RequireComponent(typeof(Ennemy))]
public class EnnemyMovement : MonoBehaviour
{
    [Header("References")]
    private Transform target;
    private Ennemy ennemy;

    [Header("Properties")]
    private int waypointIndex = 0;

    void Start()
    {
        target = WayPoints.points[0];
        ennemy = GetComponent<Ennemy>();
    }

    private void Update()
    {
        //Deplacement
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * ennemy.speed * Time.deltaTime, Space.World);

        //Si on est pr�s du checkPoitn On passe au suivant
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        //On reset la vitesse au cas ou on a �t� touch� par un laser avant
        ennemy.speed = ennemy.Initialspeed;
    }

    private void GetNextWaypoint()
    {
        //Si on est arriv� au bout
        if (waypointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        //Change de checkPoint
        waypointIndex++;
        target = WayPoints.points[waypointIndex];
    }

    private void EndPath()
    {
        //Destruction de l'ennemi
        Destroy(gameObject);

        // On enl�ve une vie
        PlayerStat.lives--;

        //Enl�ve l'ennemi du compteur d'ennemis vivant
        WavesSpooner.ennemiesAlive--;
    }
}
