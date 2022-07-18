using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;

    public TMP_Text upgradeCost;
    public TMP_Text sellAmount;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        target = _target;

        //Positionne au dessus de la tourelle
        transform.position = target.transform.position + target.positionOffSet;

        if(!target.isUpgraded)
        {
            //On change le prix de l'amélioration
            upgradeCost.text = "-" + target.turretBlueprint.upgradeCost + "$";
            upgradeButton.interactable = true;
        }
        else
        {
            //On change le prix de l'amélioration
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "+" + target.turretBlueprint.GetSellAmount() + "$";

        ui.SetActive(true);
    }

    public void Hide()
    {
        // On cache
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();

        // Déselectionne la tourelle parès l'amélioration
        BuildManager.instance.DeSelectNode();
    }

    public void Sell()
    {
        target.SellTurret();

        // Déselectionne la tourelle parès l'amélioration
        BuildManager.instance.DeSelectNode();
    }
}
