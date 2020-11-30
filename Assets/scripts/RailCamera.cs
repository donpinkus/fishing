using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCamera : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private float cameraZ = -10;
    public GameObject Hook;
    public GameObject MainController;

    void Start(){}

    void Update(){
        int stage = MainController.GetComponent<MainController>().stage;

        if (stage == 1) {
            transform.position = new Vector3(0, Hook.GetComponent<Transform>().position.y - 3.33f, cameraZ);
        } else if (stage == 2) {
            transform.position = new Vector3(0, Hook.GetComponent<Transform>().position.y + 3.33f, cameraZ);
        } else if (stage == 3) {
            // If stage 3, follow lowest fish pos
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("fish");

            float? lowestY = null;
            foreach(GameObject fish in fishes) {
                float y = fish.GetComponent<Transform>().position.y;

                if ((!lowestY.HasValue || y < lowestY) && y > 3.33) {
                    lowestY = y;
                }
            }

            // The fish will launch when camera is at 3.33, so avoid chopping back down to 0.
            if (lowestY < 3.33 || !lowestY.HasValue) {
                lowestY = 3.33f;
            }

            transform.position = new Vector3(transform.position.x, (float)lowestY, cameraZ);
        } else if (stage == 4) {
            rb.velocity = Vector2.zero;
        }
    }

    void HandleStageChange(int stage) {
        switch (stage) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }
}
