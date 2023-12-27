using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData shared;

    public int Money;
    public int Health;

    void Awake()
    {
        if (shared == null)
            shared = this;
    }

    public void AddMoney(int addMoney)
    {
        Money += addMoney;
    }
    public void SubtractMoney(int subtractMoney)
    {
        Money -= subtractMoney;
    }

    public void HurtPlayer(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            GameOverScreen.shared.EnableGameOver();
        }
    }
}
