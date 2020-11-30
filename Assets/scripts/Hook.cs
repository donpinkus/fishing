using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Hook : MonoBehaviour
{
    public Rigidbody2D rb;

    public float trip1YOffset;
    public float trip2YOffset;

    public GameObject mainController;
    public GameObject railCamera;
    public GameObject GyroManager;

    public Text debugText;

    private int stage; // Read from MainController.

    void Start()
    {
        // Move Hook to starting position, which is relative to the camera.
        float camY = railCamera.GetComponent<Transform>().position.y;

        transform.position = new Vector2(0, camY + trip1YOffset);
    }

    void Update() {

    }

    void FixedUpdate(){
        stage = mainController.GetComponent<MainController>().stage;

        float newY = GetYPositionRelativeToCamera();
        float newX = transform.position.x;

        if (stage == 1 || stage == 2) {
            newX = GetXPositionFromGyro();
        } 

        if (stage == 2) {
            if (transform.position.y >= 0) {
                mainController.SendMessage("BeginStage3");
            }
        }

        transform.position = new Vector2(newX, newY);
    }

    float GetYPositionRelativeToCamera(){
        float camY = railCamera.GetComponent<Transform>().position.y;
        float newY = 0;

        if (stage == 1) {
            newY = camY + trip1YOffset;
        } else if (stage == 2) {
            newY = camY + trip2YOffset;
        } else if (stage == 3 || stage == 4) {
            newY = 0;
        }

        return newY;
    }

    // Gets the gyroscope and sets the x position based on it. Between 330 degree (rotated right) and 30 degree (rotated left). 0 degree is straight up.
    // Includes smoothing.
    float GetXPositionFromGyro(){ 
        float yAngle = GyroManager.GetComponent<GyroManager>().angles.y;
        float x = 0;

        // Rotating left goes from 0 (straight up) to 30 deg.
        if (0 <= yAngle && yAngle < 90) {
            float clampedYAngle = Mathf.Clamp(yAngle, 0, 20);

            x = clampedYAngle / 20 * -2;
        }

        // Rotating right goes from 360 (straight up) to 330 deg.
        if (360 >= yAngle && yAngle > 270) {
            float clampedYAngle = Mathf.Clamp(yAngle, 340, 360) - 340;

            // Subtract yAngle from 30, otherwise this inverts (ie at 360 deg, it will be all the way to the right instead of in center).
            x = (20 - clampedYAngle) / 20 * 2; 
        }

        // Smooth the position
        Vector2 targetPosition = new Vector2(x, transform.position.y);
        Vector2 velocity = rb.velocity;
        Vector2 smoothedPos = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, 0.15f);

        debugText.text = "GyroYAng: " + yAngle + " \nGyroX: " + x + " \nsmX: " + transform.position.x;

        return smoothedPos.x;
    }

    void OnTriggerEnter2D(Collider2D other) { 
        if (other.CompareTag("fish")) {
            // If descending, stop
            if (mainController.GetComponent<MainController>().stage == 1) {
                mainController.SendMessage("BeginStage2");
            }

            // Catch the fish
            other.GetComponent<Fish>().isCaught = true;
            other.GetComponent<Transform>().transform.RotateAround(transform.position, new Vector3(0, 0, 1), Random.Range(55, 135));

        }
    }
}
