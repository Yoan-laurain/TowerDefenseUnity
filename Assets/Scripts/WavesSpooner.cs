using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class WavesSpooner : MonoBehaviour
{
    public Waves[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    [SerializeField]
    private TMP_Text waveCountDownTimer;

    private float countDown = 5f;

    private int wavesIndex = 0;
    public static int ennemiesAlive = 0;

    public GameManager gameManager;

    private void Start()
    {
        ennemiesAlive = 0;

        Waves wave = waves[wavesIndex];
        wave.init();

        for (int i = 0; i < wave.countFast; i++)
        {
            wave.ennemies[i] = wave.ennemyFast;
        }

        for (int i = wave.countFast; i < wave.countMedium + wave.countFast; i++)
        {
            wave.ennemies[i] = wave.ennemyMedium;
        }

        for (int i = wave.countFast + wave.countMedium; i < wave.NbEnnemy(); i++)
        {
            wave.ennemies[i] = wave.ennemySlow;
        }
    }

    void Update()
    {
        // Si la vague n'est pas fini
        if (ennemiesAlive > 0) return;

        if (wavesIndex == waves.Length)
        {
            gameManager.WinLevel();
            enabled = false;
        }

        // Si compteur à 0
        if (countDown <= 0f)
        {
            //Spawn
            StartCoroutine( SpawnWave() );

            //Reset Timer
            countDown = timeBetweenWaves;
            return;
        }

        //Maj du temps
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        //Affichage du compte à rebours
        waveCountDownTimer.text = "Next wave " + String.Format("{0:00.00}",countDown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStat.rounds++;
        Waves wave = waves[wavesIndex];

        ennemiesAlive = wave.NbEnnemy();

        for (int i = 0; i < wave.NbEnnemy() ; i++)
        {
            System.Random random = new System.Random();

            int index = random.Next(0, wave.ennemies.Length );

            GameObject ennemyToSpawn = wave.ennemies[index];

            SpawnEnnemy(ennemyToSpawn);

            wave.RemoveElement(index);

            //Espacement entre les spawns d'ennemi
            yield return new WaitForSeconds(1f / wave.rate);
        }

        wavesIndex++;
    }

    void SpawnEnnemy(GameObject ennemy)
    {
        Instantiate(ennemy, spawnPoint.position, spawnPoint.rotation);
    }

}
