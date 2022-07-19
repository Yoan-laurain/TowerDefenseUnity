using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public Transform target;
    public float range = 15f;
    private Ennemy targetEnnemy;

    [Header("Laser")]
    public bool useLaser;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    public float slowAmount = 0.5f;

    [Header("Bullet")]
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    public GameObject bulletPrefab;


    public string ennemyTag = "Ennemy";
    public Transform partToRotate;
    private float turnSpeed = 5f;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);   
    }

    /*
     * Récupère l'ennemi le plus proche
     */
    void UpdateTarget()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(ennemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnnemy = null;

        foreach (var ennemy in ennemies)
        {
            float distanceToEnnemy = Vector3.Distance(ennemy.transform.position,transform.position);
            if(distanceToEnnemy < shortestDistance)
            {
                shortestDistance = distanceToEnnemy;
                nearestEnnemy = ennemy;
            }
        }

        if(nearestEnnemy != null && shortestDistance <= range)
        {
            //Ennemi le plus proche
            target = nearestEnnemy.transform;
            targetEnnemy = target.GetComponent<Ennemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if(useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }
            return;
        }

        LockOnTarget();

        if(useLaser)
        {
            Laser();
        }
        else
        {
            //Tir
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1 / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    void Laser()
    {
        //Dégats
        targetEnnemy.TakeDamage(damageOverTime * Time.deltaTime);

        //Ralentissement
        targetEnnemy.Slow(slowAmount);

        //Activation du laser
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            impactEffect.Play();
        }

        //Depart et arrivée du laser
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        //Fait rebondir les particules contre l'ennemi
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);


        impactEffect.transform.position = target.position + dir.normalized * 1.5f;
    }

    private void LockOnTarget()
    {
        //  On vise
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
