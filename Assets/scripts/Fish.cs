using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
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

    public GameObject explosion;

    // Start is called before the first frame update
    void Start() {
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
    void Update() {
        int stage = mainController.GetComponent<MainController>().stage;

        if (stage == 1 || stage == 2) {
            // Wrap the fishies!
            // TODO: magic number 5 for x values, by trying on 13:6 display.
            if (transform.position.x > 5) {
                transform.position = new Vector2(-5, transform.position.y);
            }

            if (transform.position.x < -5) {
                transform.position = new Vector2(5, transform.position.y);
            }
     
            // Stick fish to the hook.
            if (isCaught) {
                transform.position = hook.GetComponent<Transform>().position;
            }
        } else if (stage == 3) {
            // If the fish is going to go off screen while flying, bounce it backwards.
            // 2.5 is magic value for edge of screen.
            if (transform.position.x > 2.2 || transform.position.x < -2.2) {
                float newX = transform.position.x > 2.2 ? 2.2f : -2.2f;

                transform.position = new Vector2(newX, transform.position.y);
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            }

            // Stop fish from falling to fast
            if (rb.velocity.y < -4) {
                rb.velocity = new Vector2(rb.velocity.x, -4);
            }

            // Remove fish after they go below -5
            if (transform.position.y < -5) {
                Destroy(gameObject);
            }
        }
    }

    void HandleClick(){
        // Decrease health by player damage
        health -= player.GetComponent<Player>().damage;

        if (health <= 0) {
            player.SendMessage("RecieveMoney", moneyValue);
            Destroy(gameObject);
        }

        // Bounce the fish upwards
        rb.AddForce(new Vector2(0, Random.Range(130, 180)));

        GameObject newExplosion = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);
        Destroy(newExplosion, 20f);
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

        float forceX = Random.Range(0, 100);
        float forceY = Random.Range(300, 500);
        rb.AddForce(new Vector2(forceX, forceY));
        
        rb.gravityScale = 0.2f;
    }
}
