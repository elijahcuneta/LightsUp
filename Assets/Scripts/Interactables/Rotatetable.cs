using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatetable : MonoBehaviour {

    [HideInInspector]
    public bool canRotate;

    [SerializeField]
    bool rotationLimit;

    [SerializeField]
    [Header("X = Minimum, Y = Maximum")]
    Vector2 rotationLimit_X, rotationLimit_Y;

    [SerializeField]
    float speed = 5;

    Vector3 orig_Rotation;
 

    void Start() {
        orig_Rotation = transform.localEulerAngles;
    }

    void Update () {
        if ((InputManager.MainRightJoystick().x != 0 || InputManager.MainRightJoystick().z != 0) && canRotate) {
            transform.localEulerAngles = new Vector3( rotationLimit ? AngleClamp(transform.localEulerAngles.x + -InputManager.MainRightJoystick().z * speed, rotationLimit_X.x, rotationLimit_X.y) : transform.localEulerAngles.x + -InputManager.MainRightJoystick().z * speed,
                                                      rotationLimit ? AngleClamp(transform.localEulerAngles.y + InputManager.MainRightJoystick().x * speed, rotationLimit_Y.x, rotationLimit_Y.y) : transform.localEulerAngles.y + InputManager.MainRightJoystick().x * speed,
                                                     transform.localEulerAngles.z);
        }
        if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) && canRotate) {
            transform.localEulerAngles = new Vector3(rotationLimit ? AngleClamp(transform.localEulerAngles.x + -Input.GetAxis("Mouse Y") * speed, rotationLimit_X.x, rotationLimit_X.y) : transform.localEulerAngles.x + -Input.GetAxis("Mouse Y") * speed,
                                                      rotationLimit ? AngleClamp(transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speed, rotationLimit_Y.x, rotationLimit_Y.y) : transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speed,
                                                     transform.localEulerAngles.z);
        }

        if (InputManager.Cross_ButtonDown() && canRotate) {
            transform.localEulerAngles = orig_Rotation;
        }
       
    }

    float AngleClamp(float angle, float minimum, float maximum) {
        if (angle < 90 || angle > 270) {
            if (angle > 180) {
                angle -= 360;
            } if (maximum > 180) {
                maximum -= 360;
            } if (minimum > 180) {
                minimum -= 360;
            }
        }
        angle = Mathf.Clamp(angle, minimum, maximum);
        if (angle < 0) {
            angle += 360;
        }
        return angle;
    }

}
