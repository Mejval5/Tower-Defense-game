using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTopPanel : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI HealthText;

    int _money;
    int _health;

    void Start()
    {
        UpdateMoneyUI();
        UpdateHealthUI();
    }

    void Update()
    {
        if (PlayerData.shared.Money != _money)
        {
            UpdateMoneyUI();
        }

        if (PlayerData.shared.Health != _health)
        {
            UpdateHealthUI();
        }
    }

    void UpdateMoneyUI()
    {
        _money = PlayerData.shared.Money;
        MoneyText.text = _money.ToString();
    }
    void UpdateHealthUI()
    {
        _health = PlayerData.shared.Health;
        HealthText.text = _health.ToString();
    }
}
