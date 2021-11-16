using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static StaticTypes;

public class ShopButtonLogic : MonoBehaviour
{
    public GameObject SelectionBox;
    public TextMeshProUGUI CostText;

    public TowerTypes TowerType;

    bool _isSelected;


    void Start()
    {
        TowerInfo[] towerInfo = BuildManager.shared.TowerUpgradeInfo.GetTowerInfo(TowerType);

        if (towerInfo.Length > 0)
            CostText.text = towerInfo[0].UpgradeCost.ToString();
        else
            gameObject.SetActive(false);
    }


    public void ClickedOn()
    {
        if (_isSelected)
        {
            UnselectThis();
        }
        else
        {
            UnselectedSelected();
            SelectThis();
        }
    }

    void UnselectedSelected()
    {
        BuildManager.shared.DeselectAllBuildButtons();
    }


    void SelectThis()
    {
        TowerSelectorManager.shared.DeselectTower();
        BuildManager.shared.SelectedTower = TowerType;

        _isSelected = true;

        SelectionBoxUpdate();
    }


    public void UnselectThis()
    {
        BuildManager.shared.SelectedTower = TowerTypes.Null;

        _isSelected = false;
        SelectionBoxUpdate();
    }

    void SelectionBoxUpdate()
    {
        SelectionBox.SetActive(_isSelected);
    }

}
