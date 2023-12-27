using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StaticTypes;

public class TowerSelectorManager : MonoBehaviour
{
    public static TowerSelectorManager shared;

    public GameObject Tower;
    public GameObject SelectionCanvas;
    public Button UpgradeButton;
    public TextMeshProUGUI UpgradeCostText;

    bool _upgradable;

    private void Awake()
    {
        if (shared == null)
            shared = this;
    }

    public void SelectTower(GameObject tower)
    {

        if (Equals(tower, Tower))
        {
            DeselectTower();
        }
        else
        {
            BuildManager.shared.DeselectAllBuildButtons();
            DeselectTower();
            CreateSelectMenuForTower(tower);
        }
    }

    private void CreateSelectMenuForTower(GameObject tower)
    {
        Tower = tower;

        transform.position = Tower.transform.position;
        SelectionCanvas.SetActive(true);

        UpdateUpgradeUI();
    }

    public void DeselectTower()
    {
        SelectionCanvas.SetActive(false);
        Tower = null;
        _upgradable = false;
    }

    public void SellTower()
    {
        BuildManager.shared.SellTower(Tower);
        DeselectTower();
    }

    public void UpdateUpgradeUI()
    {
        TowerProperties towerInfo = Tower.GetComponent<TowerProperties>();

        TowerInfo[] towerInfoLevels = BuildManager.shared.TowerUpgradeInfo.GetTowerInfo(towerInfo.Type);

        int level = towerInfo.Level;

        if (towerInfoLevels.Length - 1 > level)
        {
            _upgradable = true;

            int upgradeCost = towerInfoLevels[level + 1].UpgradeCost;
            UpgradeCostText.text = upgradeCost.ToString();

            UpdateUpgradeUIMoney();
        }
        else
        {
            UpgradeCostText.text = "";

            _upgradable = false;
            if (UpgradeButton.IsInteractable())
                UpgradeButton.interactable = false;
        }
    }
    void UpdateUpgradeUIMoney()
    {
        if (Tower != null)
        {
            TowerProperties towerInfo = Tower.GetComponent<TowerProperties>();

            TowerInfo[] towerInfoLevels = BuildManager.shared.TowerUpgradeInfo.GetTowerInfo(towerInfo.Type);

            int level = towerInfo.Level;

            int upgradeCost = towerInfoLevels[level + 1].UpgradeCost;

            if (PlayerData.shared.Money >= upgradeCost)
            {
                if (!UpgradeButton.IsInteractable())
                    UpgradeButton.interactable = true;
            }
            else
            {
                if (UpgradeButton.IsInteractable())
                    UpgradeButton.interactable = false;
            }
        }
    }

    private void Update()
    {
        if (_upgradable)
            UpdateUpgradeUIMoney();
    }

    public void UpgradeTower()
    {
        TowerProperties towerInfo = Tower.GetComponent<TowerProperties>();

        int level = towerInfo.Level;

        towerInfo.Tile.TowerAbove = null;
        Destroy(Tower);

        BuildManager.shared.UpgradeTower(towerInfo.Tile.gameObject, level + 1, towerInfo.Type);

        DeselectTower();
    }
}

