using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Rigidbody2D rb;

    public float hSpeed;

    public float trip1XOffset;
    public float trip1YOffset;

    public float trip2XOffset;
    public float trip2YOffset;

    private float vertExtent;
    private float horizExtent;
    private float padding;

    public GameObject mainController;
    public GameObject railCamera;
    
    void Start()
    {
        // Move Hook to starting position
        float camY = railCamera.GetComponent<Transform>().position.y;
        transform.position = new Vector2(trip1XOffset, camY + trip1YOffset);
    }

    void Update() {
        float camY = railCamera.GetComponent<Transform>().position.y;

        int stage = mainController.GetComponent<MainController>().stage;

        if (stage == 1) {
            transform.position = new Vector2(transform.position.x, camY + trip1YOffset);
        } else if (stage == 2) {
            transform.position = new Vector2(transform.position.x, camY + trip2YOffset);

            if (transform.position.y >= 0) {
                mainController.SendMessage("BeginStage3");
            }
        } else if (stage == 3) {
            transform.position = new Vector3(transform.position.x, 0);
        }
    }

    void FixedUpdate()
    {
        float hInput = Input.GetAxis("Horizontal");

        if (hInput != 0) {
            rb.AddRelativeForce(Vector2.right * hInput * hSpeed);
        }

        if (transform.position.x > 2 || transform.position.x < -2) {
            rb.velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x > 2 ? 2 : -2, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other) { 
        if (other.CompareTag("fish")) {
            // If descending, stop
            if (mainController.GetComponent<MainController>().stage == 1) {
                mainController.SendMessage("BeginStage2");
            }

            // Catch the fish
            other.GetComponent<Fish>().isCaught = true;
        }
    }
}
