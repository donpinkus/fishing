using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroManager : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    public Vector3 angles;

    public Text GyroX;
    public Text GyroY;
    public Text GyroZ;

    // Start is called before the first frame update
    void Start(){
        gyroEnabled = EnableGyro();
    }

    // Update is called once per frame
    void Update(){
        Quaternion unityAttitude = GyroToUnity(Input.gyro.attitude);
        angles = unityAttitude.eulerAngles;

        // GyroX.text = "X: " + Mathf.Floor(angles.x).ToString();
        // GyroY.text = "Y: " + Mathf.Floor(angles.y).ToString();
        // GyroZ.text = "Z: " + Mathf.Floor(angles.z).ToString();
    }

    private static Quaternion GyroToUnity(Quaternion q){
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private bool EnableGyro(){
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;

            return true;
        }

        return false;
    }
}
