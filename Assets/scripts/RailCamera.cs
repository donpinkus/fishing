using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCamera : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Hook;
    public GameObject MainController;

    public float speed;

    private float cameraZ = -10;

    // Start is called before the first frame update
    void Start(){
        MoveToStartPosition();
    }

    // Update is called once per frame
    void Update(){
        int stage = MainController.GetComponent<MainController>().stage;

        if (stage == 1) {

        } else if (stage == 2) {
            
        } else if (stage == 3) {
            // If stage 3, follow lowest fish pos
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("fish");

            float lowestY = -1f;
            foreach(GameObject fish in fishes) {
                float y = fish.GetComponent<Transform>().position.y;

                if (lowestY == -1 || y < lowestY) {
                    lowestY = y;
                }
            }

            // The fish will launch when camera is at 3.33, so avoid chopping back down to 0.
            if (lowestY < 3.33) {
                lowestY = 3.33f;
            }

            transform.position = new Vector3(transform.position.x, lowestY, transform.position.z);

            // If no fish remain, show score screen
            if (fishes.Length == 0) {
                MainController.SendMessage("BeginStage4");
            }
            
        } else if (stage == 4) {
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate(){
        
    }

    void BeginStage1(){
        MoveToStartPosition();

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, -speed));
    }

    void MoveToStartPosition(){
        float cameraOffset = -1 * Hook.GetComponent<Hook>().trip1YOffset;
        transform.position = new Vector3(0, cameraOffset, cameraZ);
    }

    void BeginStage2(){
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, speed));
    }

    void BeginStage3(){
        // Pin camera to lowest fish's height.
        rb.velocity = Vector2.zero;
    }
}
