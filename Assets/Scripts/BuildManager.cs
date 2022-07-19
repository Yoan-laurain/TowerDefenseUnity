using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;
    public static BuildManager instance;

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStat.money >= turretToBuild.cost; } }
    public GameObject buildEffect;

    private void Awake()
    {
        if(instance != null) return;

        instance = this;
    }

    public void SetToTurretToBuild(TurretBlueprint myTurret)
    {
        turretToBuild = myTurret;
        DeSelectNode();
    }    

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    
    public void SelectNode(Node node)
    {
        turretToBuild = null;

        if(node == selectedNode)
        {
            DeSelectNode();
            return;
        }

        selectedNode = node;
        nodeUI.SetTarget(node); 
    }

    public void DeSelectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
