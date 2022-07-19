using UnityEngine;
using TMPro;
using System.Collections;

public class RoundsSurvived : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text roundsText;

    void OnEnable()
    {
        StartCoroutine( AnimateText() );
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        while (round < PlayerStat.rounds)
        {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
