using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDepthText : MonoBehaviour
{
    public Text depthText;
    public GameObject Hook;

    private float maxAchievedDepth;

    // Start is called before the first frame update
    void Start()
    {
        maxAchievedDepth = 0;
    }

    // Update is called once per frame
    void Update(){
        float currentDepth = Hook.GetComponent<Hook>().currentDepth;

        if (currentDepth > maxAchievedDepth) {
            maxAchievedDepth = currentDepth;
        }

        depthText.text = maxAchievedDepth + " m";
    }
}
