using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ennemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int waypointIndex = 0;
    public int health = 100;
    public int rewardKill = 50;
    public GameObject dieEffect;

    void Start()
    {
        target = WayPoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStat.money += rewardKill;
        GameObject death = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(death, 1f);
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
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
