using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {


    #region Axis
    //- Raw

    public static Vector3 MainLeftJoystick_Raw() {
        return new Vector3(LeftHorizontalJoystick_Raw(), 0, LeftVerticalJoystick_Raw());
    }

    public static Vector3 MainRightJoystick_Raw() {
        return new Vector3(RightHorizontalJoystick_Raw(), 0, RightVerticalJoystick_Raw());
    }

    public static float LeftHorizontalJoystick_Raw() {
        float horizontal = 0f;
        horizontal += Input.GetAxisRaw("Horizontal");
        horizontal += Input.GetAxisRaw("Left X Joystick");
        return Mathf.Clamp(horizontal, -1f, 1f);
    }

    public static float LeftVerticalJoystick_Raw() {
        float vertical = 0f;
        vertical += Input.GetAxisRaw("Vertical");
        vertical += Input.GetAxisRaw("Left Y Joystick");
        return Mathf.Clamp(vertical, -1f, 1f);
    }

    public static float LeftTrigger_Raw() {
        float left = 0f;
        left += Input.GetAxisRaw("Left Trigger");
        return Mathf.Clamp(left, -1f, 1f);
    }

    public static float RightVerticalJoystick_Raw()  {
        float vertical = 0f;
        vertical += Input.GetAxisRaw("Right Y Joystick");
        return Mathf.Clamp(vertical, -1f, 1f);
    }

    public static float RightHorizontalJoystick_Raw() {
        float horizontal = 0f;
        horizontal += Input.GetAxisRaw("Right X Joystick");
        return Mathf.Clamp(horizontal, -1f, 1f);
    }

    public static float RightTrigger_Raw() {
        float right = 0f;
        right += Input.GetAxisRaw("Right Trigger");
        return Mathf.Clamp(right, -1f, 1f);
    }

    public static float DPad_Horizontal_Raw() {
        float horizontal = 0f;
        horizontal += Input.GetAxisRaw("D-Pad X");
        return Mathf.Clamp(horizontal, -1f, 1f);
    }

    public static float DPad_Vertical_Raw() {
        float vertical = 0f;
        vertical += Input.GetAxisRaw("D-Pad Y");
        return Mathf.Clamp(vertical, -1f, 1f);
    }

    // - Interpolate

    public static Vector3 MainLeftJoystick() {
        return new Vector3(LeftHorizontalJoystick(), 0, LeftVerticalJoystick());
    }

    public static Vector3 MainRightJoystick() {
        return new Vector3(RightHorizontalJoystick(), 0, RightVerticalJoystick());
    }

    public static float LeftHorizontalJoystick() {
        float horizontal = 0f;
        horizontal += Input.GetAxis("Horizontal");
        horizontal += Input.GetAxis("Left X Joystick");
        return Mathf.Clamp(horizontal, -1f, 1f);
    }

    public static float LeftVerticalJoystick() {
        float vertical = 0f;
        vertical += Input.GetAxis("Vertical");
        vertical += Input.GetAxis("Left Y Joystick");
        return Mathf.Clamp(vertical, -1f, 1f);
    }

    public static float LeftTrigger() {
        float left = 0f;
        left += Input.GetAxis("Left Trigger");
        return Mathf.Clamp(left, -1f, 1f);
    }

    public static float RightVerticalJoystick() {
        float vertical = 0f;
        vertical += Input.GetAxis("Mouse Y");
        vertical += Input.GetAxis("Right Y Joystick");
        return Mathf.Clamp(vertical, -1f, 1f);
    }

    public static float RightHorizontalJoystick() {
        float horizontal = 0f;
        horizontal += Input.GetAxis("Mouse X");
        horizontal += Input.GetAxis("Right X Joystick");
        return Mathf.Clamp(horizontal, -1f, 1f);
    }

    public static float RightTrigger() {
        float right = 0f;
        right += Input.GetAxis("Right Trigger");
        return Mathf.Clamp(right, -1f, 1f);
    }

    public static float DPad_Horizontal() {
        float horizontal = 0f;
        horizontal += Input.GetAxis("D-Pad X");
        return Mathf.Clamp(horizontal, -1f, 1f);
    }

    public static float DPad_Vertical() {
        float vertical = 0f;
        vertical += Input.GetAxis("D-Pad Y");
        return Mathf.Clamp(vertical, -1f, 1f);
    }

    #endregion

    #region Button

    // -- Down
    public static bool Cross_ButtonDown() {
        return Input.GetButtonDown("Joystick Cross");
    }

    public static bool Square_ButtonDown() {
        return Input.GetButtonDown("Joystick Square");
    }

    public static bool Circle_ButtonDown() {
        return Input.GetButtonDown("Joystick Circle");
    }

    public static bool Triangle_ButtonDown() {
        return Input.GetButtonDown("Joystick Triangle");
    }

    public static bool Start_ButtonDown() {
        return Input.GetButtonDown("Start");
    }

    public static bool Select_ButtonDown() {
        return Input.GetButtonDown("Select");
    }

    public static bool LeftBumper_ButtonDown() {
        return Input.GetButton("Left Bumper");
    }

    public static bool RightBumper_ButtonDown() {
        return Input.GetButtonDown("Right Bumper");
    }

    public static bool LeftJoystick_ButtonDown() {
        return Input.GetButtonDown("Left Joystick Button");
    }

    public static bool RightJoystick_ButtonDown() {
        return Input.GetButtonDown("Right Joystick Button");
    }

    // -- Hold
    public static bool Cross_Button() {
        return Input.GetButton("Joystick Cross");
    }

    public static bool Square_Button() {
        return Input.GetButton("Joystick Square");
    }

    public static bool Circle_Button() {
        return Input.GetButton("Joystick Circle");
    }

    public static bool Triangle_Button() {
        return Input.GetButton("Joystick Triangle");
    }

    public static bool Start_Button() {
        return Input.GetButton("Start");
    }

    public static bool Select_Button() {
        return Input.GetButton("Select");
    }

    public static bool LeftBumper_Button() {
        return Input.GetButton("Left Bumper");
    }

    public static bool RightBumper_Button() {
        return Input.GetButton("Right Bumper");
    }

    public static bool LeftJoystick_Button() {
        return Input.GetButton("Left Joystick Button");
    }

    public static bool RightJoystick_Button() {
        return Input.GetButton("Right Joystick Button");
    }

    // -- Up

    public static bool Cross_ButtonUp() {
        return Input.GetButtonUp("Joystick Cross");
    }

    public static bool Square_ButtonUp() {
        return Input.GetButtonUp("Joystick Square");
    }

    public static bool Circle_ButtonUp() {
        return Input.GetButtonUp("Joystick Circle");
    }

    public static bool Triangle_ButtonUp() {
        return Input.GetButtonUp("Joystick Triangle");
    }

    public static bool Start_ButtonUp() {
        return Input.GetButtonUp("Start");
    }

    public static bool Select_ButtonUp() {
        return Input.GetButtonUp("Select");
    }

    public static bool LeftBumper_ButtonUp() {
        return Input.GetButtonUp("Left Bumper");
    }

    public static bool RightBumper_ButtonUp() {
        return Input.GetButtonUp("Right Bumper");
    }

    public static bool LeftJoystick_ButtonUp() {
        return Input.GetButtonUp("Left Joystick Button");
    }

    public static bool RightJoystick_ButtonUp() {
        return Input.GetButtonUp("Right Joystick Button");
    }

    #endregion

}
