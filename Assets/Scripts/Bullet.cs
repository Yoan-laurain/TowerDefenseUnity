using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame,Space.World);
        transform.LookAt(target);
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
    }

    void HitTarget()
    {

        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect,5f);

        if(explosionRadius > 0 )
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var collider in colliders)
        {
            if(collider.tag == "Ennemy")
            {
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
