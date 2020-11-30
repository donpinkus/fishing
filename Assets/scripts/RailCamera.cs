using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCamera : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private float cameraZ = -10;
    public GameObject Hook;
    public GameObject LevelController;

    void Start(){}

    void Update(){
        int stage = LevelController.GetComponent<LevelController>().stage;

        if (stage == 1) {
            float newY = Hook.GetComponent<Transform>().position.y - 3.33f;
            transform.position = new Vector3(0, newY, cameraZ);
        } else if (stage == 2) {
            float newY = Hook.GetComponent<Transform>().position.y + 3.33f;

            Vector3 targetPos = new Vector3(transform.position.x, newY, cameraZ);
            Vector3 velocity = rb.velocity;

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);

            // transform.position = new Vector3(0, Hook.GetComponent<Transform>().position.y + 3.33f, cameraZ);
        } else if (stage == 3) {
            // If stage 3, follow lowest fish pos
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("fish");

            float? lowestY = null;
            foreach(GameObject fish in fishes) {
                float y = fish.GetComponent<Transform>().position.y;

                // We check if y > 0, so we still allow fish below the camera to be focused.
                // Fish below the water we're ok to move away from.
                if ((!lowestY.HasValue || y < lowestY) && y > 0) {
                    lowestY = y;
                }
            }

            // The fish will launch when camera is at 3.33, so avoid chopping back down to 0.
            if (lowestY < 3.33 || !lowestY.HasValue) {
                lowestY = 3.33f;
            }

            Vector3 targetPos = new Vector3(transform.position.x, (float)lowestY, cameraZ);
            Vector3 velocity = rb.velocity;

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.075f); 
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
