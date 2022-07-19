using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    private Transform target;
    public GameObject impactEffect;

    [Header("Properties")]
    public float explosionRadius = 0f;
    public float speed = 70f;
    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        // Destruction de la balle si la cible est morte
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Distance entre la balle et la cible
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Si on touche
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Modication de la trahjectoire de la balle
        transform.Translate(dir.normalized * distanceThisFrame,Space.World);
        transform.LookAt(target);
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
    }

    void HitTarget()
    {
        // Effet visuel
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect,5f);

        //Balle explosive
        if(explosionRadius > 0 )
        {
            Explode();
        }
        else // Balle normal
        {
            Damage(target);
        }
        
        // Detruit la balle
        Destroy(gameObject);
    }

    void Explode()
    {
        // Crée une sphère autour de la balle qui sera la zone d'explosion
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Parcours tout les elements dans la sphere
        foreach (var collider in colliders)
        {
            //Si c'est un ennemi
            if(collider.tag == "Ennemy")
            {
                //On lui fait les dégats
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform ennemy)
    {
        Ennemy e = ennemy.GetComponent<Ennemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }

    }
}
