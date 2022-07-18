using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret()
    {
        buildManager.SetToTurretToBuild(standardTurret);
    }    
    public void PurchaseMissileTurret()
    {
        buildManager.SetToTurretToBuild(missileTurret);
    } 
    public void PurchaseLaserTurret()
    {
        buildManager.SetToTurretToBuild(laserTurret);
    }
}
