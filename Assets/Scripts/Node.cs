using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color baseColor;
    public Color notEnoughMoneyColor;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    public Vector3 positionOffSet;
    private BuildManager buildManager;
    private Renderer rend;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
        hoverColor = Color.white;
        notEnoughMoneyColor = Color.red;
        buildManager = BuildManager.instance;
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStat.money < blueprint.cost)
        {
            return;
        }

        PlayerStat.money -= blueprint.cost;

        turretBlueprint = blueprint;

        GameObject _turret = Instantiate(blueprint.prefab, transform.position + positionOffSet, Quaternion.identity);
        turret = _turret;
        GameObject effect = Instantiate(buildManager.buildEffect, transform.position + positionOffSet, Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    public void UpgradeTurret( )
    {
        // Pas assez d'argent pour upgrade
        if (PlayerStat.money < turretBlueprint.upgradeCost)
        {
            return;
        }

        PlayerStat.money -= turretBlueprint.upgradeCost;

        // On supprime la tourelle présente 
        Destroy(turret);

        // On crée la nouvelle tourelle améliorée
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, transform.position + positionOffSet, Quaternion.identity);
        turret = _turret;

        isUpgraded = true;

        // Effet de spawn
        GameObject effect = Instantiate(buildManager.buildEffect, transform.position + positionOffSet, Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    public void SellTurret()
    {
        PlayerStat.money += turretBlueprint.GetSellAmount();

        GameObject effect = Instantiate(buildManager.buildEffect, transform.position + positionOffSet, Quaternion.identity);
        Destroy(effect, 0.5f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }

    private void OnMouseDown()
    {
        //Si un element UI est par dessus la node
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Si une tourelle selectionée
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        //Si pas assez d'argent
        if (!buildManager.canBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }


    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return ;
        }
        if (!buildManager.canBuild)
        {
            return;
        }

        if(buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }

    void OnMouseExit()
    {
        rend.material.color = baseColor;
    }
}
