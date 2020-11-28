using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject railCamera;
    public GameObject hook;
    
    public GameObject explosion; 

    public GameObject fishA;
    public GameObject[] fishes;

    public int stage;

    public float minDepth;
    public float maxDepth;

    private float fishSpawnedDepth;

    // Start is called before the first frame update
    void Start(){
        minDepth = 0;
        maxDepth = 100; // TODO: update as fishing line gets longer

        stage = 1;

        SpawnFish();

        fishes = GameObject.FindGameObjectsWithTag("fish");
    }

    // Update is called once per frame
    void Update(){
        if (stage == 3 && Input.GetButtonDown("Fire1")) {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject newExplosion = Instantiate(explosion, new Vector3(point.x, point.y, 0f), transform.rotation);
            
            Destroy(newExplosion, 20f);
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

