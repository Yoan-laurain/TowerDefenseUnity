using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WaveUI : MonoBehaviour
{

    public TMP_Text wavesCountText;

    void Update()
    {
        wavesCountText.text = "Wave " + PlayerStat.rounds.ToString();
    }
}
