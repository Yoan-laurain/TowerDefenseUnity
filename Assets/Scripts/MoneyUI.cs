using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text moneyText;

    void Update()
    {
        // On affiche l'argent du joueur
        moneyText.text = "Money $" + PlayerStat.money.ToString();
    }
}
