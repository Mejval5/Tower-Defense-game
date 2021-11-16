using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticTypes;

[CreateAssetMenu(menuName = "Datascripts/TowerUpgradeInformation")]
public class TowerUpgradeInformation : ScriptableObject
{
    public TowerInfo[] BallistaTower;
    public TowerInfo[] CannonTower;
    public TowerInfo[] CrystalTower;
    public TowerInfo[] LaserTower;


    public TowerInfo[] GetTowerInfo(TowerTypes towerType)
    {
        switch (towerType)
        {
            case TowerTypes.Null:
                return null;
            case TowerTypes.Ballista:
                return BallistaTower;
            case TowerTypes.Cannon:
                return CannonTower;
            case TowerTypes.Crystal:
                return CrystalTower;
            case TowerTypes.Laser:
                return LaserTower;
            default:
                return null;
        }
    }
}
