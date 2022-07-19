using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10f;


    public float Initialspeed = 10f;
    public float starthealth = 100f;
    private float currentHealth;
    public int rewardKill = 50;

    public GameObject dieEffect;
    public Image healthBar;

    void Start()
    {
        speed = Initialspeed;
        currentHealth = starthealth;
    }

    public void TakeDamage(float amount)
    {
        //Soustraction des pv
        currentHealth -= amount;

        //Modification de la barre de pv
        healthBar.fillAmount = currentHealth / starthealth;

        //Check si il est mort
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        //Ajout de l'argent pour le joueur
        PlayerStat.money += rewardKill;
        
        //Effet de mort
        GameObject death = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(death, 1f);

        //Destruction de l'ennemi
        Destroy(gameObject);
    }

    public void Slow(float amount)
    {
        speed = Initialspeed * (1f - amount);
    }

}
