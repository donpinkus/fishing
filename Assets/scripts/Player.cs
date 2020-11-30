using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float damage;

    public int levelMoney;
    public int bankMoney;

    public int maxDepth;

    void Start(){}

    void Update(){}

    void RecieveMoney(int moneyValue){
        levelMoney += moneyValue;
    }

    void TransferMoneyToBank(){
        bankMoney += levelMoney;
        levelMoney = 0;
    }
}
