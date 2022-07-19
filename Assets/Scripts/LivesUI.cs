using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LivesUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text livesText;

    void Update()
    {
        // On met à jour le nombre de vies
        livesText.text = "Lives : " + PlayerStat.lives ;
    }
}
