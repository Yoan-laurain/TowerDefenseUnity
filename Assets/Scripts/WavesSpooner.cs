using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class WavesSpooner : MonoBehaviour
{
    [SerializeField]
    private Transform ennemyPreFab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    [SerializeField]
    private TMP_Text waveCountDownTimer;

    private float countDown = 5f;

    private int wavesIndex = 0;


    void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine( SpawnWave() );
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCountDownTimer.text = "Next wave " + String.Format("{0:00.00}",countDown);
    }

    IEnumerator SpawnWave()
    {
        wavesIndex++;
        PlayerStat.rounds++;

        for (int i = 0; i <= wavesIndex; i++)
        {
            SpawnEnnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnnemy()
    {
        Instantiate(ennemyPreFab, spawnPoint.position, spawnPoint.rotation);
    }

}
