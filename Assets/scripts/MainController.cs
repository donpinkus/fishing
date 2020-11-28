using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject railCamera;
    public GameObject hook;
    
    public GameObject fishA;

    public int stage;

    public float minDepth;
    public float maxDepth;

    private float fishSpawnedDepth;

    public GameObject[] fishes;

    // Start is called before the first frame update
    void Start(){
        minDepth = 0;
        maxDepth = 100; // TODO: update as line gets longer

        stage = 1;

        SpawnFish();

        fishes = GameObject.FindGameObjectsWithTag("fish");
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("FIRE");
        }
    }

    void SpawnFish(){
        for (int i = 0; i < 200; i++) {
            Instantiate(fishA);
        }

        // Alternatives
        // Depth bands - Spawn at a depth range, roll the dice to continue spawning fish, where more fish less likely to continue spawning. Move to next depth. 
    }

    void BeginStage2(){
        stage = 2;

        railCamera.SendMessage("BeginStage2");
    }

    void BeginStage3(){
        stage = 3;

        railCamera.SendMessage("BeginStage3");

        foreach (GameObject fish in fishes) {
            fish.SendMessage("BeginStage3");
        }
    }
}

