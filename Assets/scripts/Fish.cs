using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float v;
    // public float minV;
    public float maxV;

    public float d;
    public float minD;
    public float maxD;

    public float rarity;
    public int moneyValue;
    public float health;

    public bool isCaught;

    public Rigidbody2D rb;

    public GameObject hook;
    public GameObject mainController;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.Find("Hook");
        mainController = GameObject.Find("MainController");
        player = GameObject.Find("Player");

        float startX = Random.Range(-5, 5);
        float startY = Random.Range(minD, maxD);
        transform.position = new Vector2(startX, startY);

        v = Random.Range(-maxV, maxV);
        rb.velocity = new Vector2(v, 0);

        if (v < 0) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        // Wrap the fishies!
        // TODO: magic number 5 for x values, by trying on 13:6 display.
        if (transform.position.x > 5) {
            transform.position = new Vector2(-5, transform.position.y);
        }

        if (transform.position.x < -5) {
            transform.position = new Vector2(5, transform.position.y);
        }

        

        int stage = mainController.GetComponent<MainController>().stage;

        if (stage == 2) {
            if (isCaught) {
                transform.position = hook.GetComponent<Transform>().position;
            }
        }
    }

    void HandleClick(){
        Debug.Log("CLICK!");

        health -= player.GetComponent<Player>().damage;

        if (health <= 0) {
            player.SendMessage("RecieveMoney", moneyValue);
            
            Destroy(gameObject);
        }
    }

    void BeginStage3(){
        if (isCaught) {
            Launch();
        } else {            
            Destroy(gameObject);
        }
    }

    // Launches fish out of water at stage 3
    void Launch(){
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, 400));
        rb.gravityScale = 0.33f;
    }
}
