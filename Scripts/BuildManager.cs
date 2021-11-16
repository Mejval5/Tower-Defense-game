using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticTypes;

public class BuildManager : MonoBehaviour
{
    public static BuildManager shared;

    public TowerTypes SelectedTower;

    [Header("Unity setup")]
    public TowerUpgradeInformation TowerUpgradeInfo;
    public Transform TowerHolder;

    public GameObject ConstructionEffect;
    public GameObject SellEffect;
    public GameObject UpgradeEffect;

    public AudioClip BuySound;
    public AudioClip UpgradeSound;
    public AudioClip SellSound;

    AudioSource _audioSource;

    void Awake()
    {
        if (shared == null)
            shared = this;

        _audioSource = GetComponent<AudioSource>();
    }

    public void SellTower(GameObject tower)
    {
        _audioSource.PlayOneShot(SellSound);
        TowerProperties towerProps = tower.GetComponent<TowerProperties>();
        TowerInfo[] towerLevelInfo = TowerUpgradeInfo.GetTowerInfo(towerProps.Type);

        int sellMoney = towerLevelInfo[towerProps.Level].UpgradeCost;

        PlayerData.shared.AddMoney(sellMoney);

        towerProps.Tile.TowerAbove = null;
        Destroy(tower);

        GameObject sellEffect = Instantiate(SellEffect, tower.transform.position, Quaternion.identity, null);

        Destroy(sellEffect, 2f);
    }

    public void DeselectAllBuildButtons()
    {
        ShopButtonLogic[] buttons = FindObjectsOfType<ShopButtonLogic>();

        foreach (ShopButtonLogic button in buttons)
        {
            button.UnselectThis();
        }
    }

    public void TryToBuildHereBasic(GameObject tileObject, int level)
    {
        TowerInfo[] towerInfoArray = TowerUpgradeInfo.GetTowerInfo(SelectedTower);

        if (towerInfoArray == null)
            return;

        if (towerInfoArray[level].UpgradeCost > PlayerData.shared.Money)
            return;

        _audioSource.PlayOneShot(BuySound);
        TowerTypes towerType = SelectedTower;
        TryToBuildHere(tileObject, level, towerType);

        GameObject constructionEffect = Instantiate(ConstructionEffect, tileObject.transform.position, Quaternion.identity, null);

        Destroy(constructionEffect, 2f);
    }

    public void UpgradeTower(GameObject tileObject, int level, TowerTypes towerType)
    {
        TowerInfo[] towerInfoArray = TowerUpgradeInfo.GetTowerInfo(towerType);

        if (towerInfoArray == null)
            return;

        if (towerInfoArray[level].UpgradeCost > PlayerData.shared.Money)
            return;

        _audioSource.PlayOneShot(UpgradeSound);
        TryToBuildHere(tileObject, level, towerType);

        GameObject upgradeEffect = Instantiate(UpgradeEffect, tileObject.transform.position, Quaternion.identity, null);

        Destroy(upgradeEffect, 2f);
    }

    public void TryToBuildHere(GameObject tileObject, int level, TowerTypes towerType)
    {
        TowerInfo[] towerInfoArray = TowerUpgradeInfo.GetTowerInfo(towerType);

        PlayerData.shared.SubtractMoney(towerInfoArray[level].UpgradeCost);

        BuildHere(tileObject, towerInfoArray[level].TowerPrefab, level, towerType);
    }

    void BuildHere(GameObject tileObject, GameObject towerPrefab, int level, TowerTypes towerType)
    {
        GameObject towerBuilt = Instantiate(towerPrefab, tileObject.transform.position, Quaternion.identity, TowerHolder);

        TowerProperties towerProps = towerBuilt.GetComponent<TowerProperties>();

        towerProps.Level = level;
        towerProps.Type = towerType;
        towerProps.Tile = tileObject.GetComponent<ConstructionTileLogic>();

        tileObject.GetComponent<ConstructionTileLogic>().TowerAbove = towerBuilt;
    }

}
