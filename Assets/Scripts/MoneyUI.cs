using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyUI : MonoBehaviour
{

    public TMP_Text moneyText;

    void Update()
    {
        moneyText.text = "Money $" + PlayerStat.money.ToString();
    }
}
