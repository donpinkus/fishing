using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCamera : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Hook;
    public GameObject MainController;

    public float speed;

    private float cameraZ;

    // Start is called before the first frame update
    void Start(){
        cameraZ = -10;

        BeginStage1();
    }

    // Update is called once per frame
    void Update(){
        int stage = MainController.GetComponent<MainController>().stage;

        if (stage == 2) {
            
        } else if (stage == 3) {
            // If stage 3, follow lowest fish pos
            GameObject[] fishes = MainController.GetComponent<MainController>().fishes;

            float lowestY = -1f;
            foreach(GameObject fish in fishes) {
                if (fish) { // TODO: we do this since uncaught fish will be Destroy()'d.
                    float y = fish.GetComponent<Transform>().position.y;

                    if (lowestY == -1 || y < lowestY) {
                        lowestY = y;
                    }
                }
            }

            // The fish will launch when camera is at 3.33, so avoid chopping back down to 0.
            if (lowestY < 3.33) {
                lowestY = 3.33f;
            }

            transform.position = new Vector3(transform.position.x, lowestY, transform.position.z);
        }
    }

    void FixedUpdate(){
        
    }

    void BeginStage1(){
        float cameraOffset = -1 * Hook.GetComponent<Hook>().trip1YOffset;
        transform.position = new Vector3(0, cameraOffset, cameraZ);

        rb.AddForce(new Vector2(0, -speed));
    }

    void BeginStage2(){
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, speed));
    }

    void BeginStage3(){
        // Pin camera to lowest fish's height.
        rb.velocity = new Vector2(0, 0);
    }
}
