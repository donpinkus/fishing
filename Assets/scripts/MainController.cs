﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject railCamera;
    public GameObject hook;
    
    public GameObject explosion; 

    public GameObject fishA;
    public GameObject fishB;

    public GameObject[] fishes;

    public GameObject scoreScreen;
    private GameObject scoreScreenInstance;

    public int stage;

    public float minDepth;
    public float maxDepth;

    // Start is called before the first frame update
    void Start(){
        StartLevel();
    }

    // Update is called once per frame
    void Update(){
        if (stage == 3 && Input.GetButtonDown("Fire1")) {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject newExplosion = Instantiate(explosion, new Vector3(point.x, point.y, 0f), transform.rotation);
            
            Destroy(newExplosion, 20f);
        }
    }

    void ClearLevel(){
        if (scoreScreenInstance) {
            Destroy(scoreScreenInstance);
        }

        // Get rid of fish.
        fishes = GameObject.FindGameObjectsWithTag("fish");
        
        foreach (GameObject fish in fishes) {
            Destroy(fish);
        }
    }

    public void StartLevel(){
        ClearLevel();

        minDepth = 0;
        maxDepth = 100; // TODO: update as fishing line gets longer

        // Camera.main.GetComponent<Transform>().position = new Vector2(0, 0);

        SpawnFish();
        fishes = GameObject.FindGameObjectsWithTag("fish");

        stage = 1;

        railCamera.SendMessage("BeginStage1");
    }

    void SpawnFish(){
        int maxFish = 300;
        int curFish = 0;

        // Loop through fish
        RecursiveSpawn(fishA, maxFish, curFish);
        RecursiveSpawn(fishB, maxFish, curFish);
    }

    void RecursiveSpawn(GameObject fish, int maxFish, int curFish) {
        // If the range value is LESS than the rarity, spawn again.
        // So a rarity of 0.9 gives a 90% chance of spawning again. 
        float val = (float)Random.Range(0, 100) / 100f;
        float rarity = fish.GetComponent<Fish>().rarity;

        Debug.Log("V: " + val + " R: " + rarity);
        Debug.Log("curfish: " + curFish);

        if (val < rarity && curFish < maxFish) {
            curFish++;

            Instantiate(fish);
            RecursiveSpawn(fish, maxFish, curFish);
        }     
    }

    void BeginStage2(){
        stage = 2;

        railCamera.SendMessage("BeginStage2");
    }

    void BeginStage3(){
        stage = 3;

        railCamera.SendMessage("BeginStage3");

        fishes = GameObject.FindGameObjectsWithTag("fish");

        foreach (GameObject fish in fishes) {
            fish.SendMessage("BeginStage3");
        }
    }

    // Score screen
    void BeginStage4(){
        stage = 4;

        scoreScreenInstance = Instantiate(scoreScreen);
    }
}

