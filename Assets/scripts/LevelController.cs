using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{    
    public GameObject explosion; 

    public GameObject fishA;
    public GameObject fishB;

    public GameObject scoreScreen;
    private GameObject scoreScreenInstance;

    public int stage = 0;

    void Start(){
        ChangeStage(1);
    }

    void Update(){
        if (stage == 3) {
            if (Input.GetButtonDown("Fire1")) {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                GameObject newExplosion = Instantiate(explosion, new Vector3(point.x, point.y, 0f), transform.rotation);
                
                Destroy(newExplosion, 20f);
            }

            // If no fish remain, show score screen
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("fish");
            
            if (fishes.Length == 0) ChangeStage(4);
        }
    }

    void ChangeStage(int newStage) {
        Debug.Log("ChangeStage: " + newStage);

        stage = newStage;

        HandleStageChange(stage);

        BroadcastMessage("HandleStageChange", stage);
    }

    // Stage 1 - comes from this controller's Start.
    // Stage 2 - comes from Hook, when it detects collision with a fish.
    // Stage 3 - comes from Hook, when it reaches the surface.
    // Stage 4 - comes from this controller's Update, when no fishes remain. 
    void HandleStageChange(int stage){
        switch (stage) {
            case 1:
                StartLevel();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                scoreScreenInstance = Instantiate(scoreScreen);
                break;
            default:
                break;
        }
    }

    public void StartLevel(){
        ClearLevel();
        SpawnFish();
    }

    void ClearLevel(){
        if (scoreScreenInstance) {
            Destroy(scoreScreenInstance);
        }

        // Get rid of fish.
        GameObject[] fishes = GameObject.FindGameObjectsWithTag("fish");
        
        foreach (GameObject fish in fishes) {
            Destroy(fish);
        }
    }

    void SpawnFish(){
        int maxFish = 100;
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

        // Debug.Log("V: " + val + " R: " + rarity);
        // Debug.Log("curfish: " + curFish);

        if (val < rarity && curFish < maxFish) {
            curFish++;

            Instantiate(fish, transform);
            RecursiveSpawn(fish, maxFish, curFish);
        }     
    }
}

