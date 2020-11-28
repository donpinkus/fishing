using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
    public Text moneyText;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        moneyText.text = "$ " + Player.GetComponent<Player>().levelMoney.ToString();
    }
}
