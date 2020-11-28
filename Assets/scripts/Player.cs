using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float damage;

    public int levelMoney;
    public int bankMoney;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RecieveMoney(int moneyValue){
        Debug.Log("RECIEVED MONEY: " + moneyValue);
        levelMoney += moneyValue;
    }

    void TransferMoneyToBank(){
        bankMoney += levelMoney;
    }
}
