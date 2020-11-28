using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScreen : MonoBehaviour
{
    public Text catchText;
    public Text bankText;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        
        // Render at 0,0 relative to camera.
        float y = Camera.main.GetComponent<Transform>().position.y;

        transform.position = new Vector2(0, y);

        // Update text.
        int bankMoney = Player.GetComponent<Player>().bankMoney;
        int levelMoney = Player.GetComponent<Player>().levelMoney;

        catchText.text = "+ $" + levelMoney;
        bankText.text = (bankMoney + levelMoney).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
