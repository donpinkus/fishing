using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoFishBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        
        btn.onClick.AddListener(() => {
            GameObject.Find("LevelController").SendMessage("ChangeStage", 1);
            GameObject.Find("Player").SendMessage("TransferMoneyToBank");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
