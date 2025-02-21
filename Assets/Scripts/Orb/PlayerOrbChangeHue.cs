using System;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerOrbChangeHue : MonoBehaviour {

    public PostProcessingProfile[] hues;

    [SerializeField]
    GameObject lightModeWindowPicker;

    PostProcessingBehaviour hueHolder;

    PlayerOrbBehavior playerOrbBehavior;
    PlayerCamera playerCamera;
    PlayerMovement playerMovement;

    [HideInInspector]
    public enum HueMode { Normal, Hidden, Move, ReverseHidden, Blind };
    HueMode hueMode = HueMode.Normal;

    bool changedSettings;
    bool canChangeHue;

    void Awake() {
        canChangeHue = true;
        hueHolder = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerOrbBehavior = GetComponent<PlayerOrbBehavior>();
        lightModeWindowPicker.SetActive(false);
    }   

    void Update() {
        if (!changedSettings) {
            changeSettings();
            changedSettings = true;
        }

        if (!PauseManager.pausePanelStatus && canChangeHue) {
            if ((Input.GetKey(KeyCode.Tab) || (/*(InputManager.MainRightJoystick_Raw().x != 0 || InputManager.MainRightJoystick_Raw().z != 0) && */InputManager.LeftTrigger() == 1)) && playerCamera.cameraMode != PlayerCamera.CameraMode.FirstPerson && !playerMovement.willRotate) {
                if (FindObjectOfType<TimeEventManager>() != null) {
                    FindObjectOfType<TimeEventManager>().SlowMotion();
                }
                Cursor.lockState = CursorLockMode.None;
                changedSettings = false;
                lightModeWindowPicker.SetActive(true);
            } else if (Input.GetKeyUp(KeyCode.Tab) || InputManager.MainRightJoystick_Raw().x == 0 || InputManager.MainRightJoystick_Raw().z == 0) {
                if(FindObjectOfType<TimeEventManager>() != null) {
                    FindObjectOfType<TimeEventManager>().StartCoroutine("NormalMotion");
                }
                Cursor.lockState = CursorLockMode.Locked;
                lightModeWindowPicker.SetActive(false);
            }
        }
    }

    void changeSettings() {
        changeHue();
    }

    void changeHue() {
        if(hueHolder != null) {
        if (hueMode == HueMode.Normal) {
            hueHolder.profile = hues[0];
            hueMode_Normal();
        }

        if (hueMode == HueMode.Hidden) {
            hueHolder.profile = hues[1];
            hueMode_Hidden();
        }

        if (hueMode == HueMode.Move) {
            hueHolder.profile = hues[2];
            hueMode_Move();
            }
        }

        if(hueMode == HueMode.Blind) {
            hueHolder.profile = hues[3];
            hueMode_Blind();
        }
        //Camera.main.cullingMask |= 1 << LayerMask.NameToLayer(h.ToString()); // turn on
        //Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer(h.ToString())); // turn off
    }

    void hueMode_Normal() {
        foreach (HueMode h in Enum.GetValues(typeof(HueMode))) {
            if (h == HueMode.Hidden) {
                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer(h.ToString())); // turn off
            } else {
                Camera.main.cullingMask |= 1 << LayerMask.NameToLayer(h.ToString()); // turn on
            }
        }
    }

    void hueMode_Hidden() {
        foreach (HueMode h in Enum.GetValues(typeof(HueMode))) {
            if (h == HueMode.Normal || h == HueMode.Move || h == HueMode.ReverseHidden) {
                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer(h.ToString())); // turn off
            } else {
                Camera.main.cullingMask |= 1 << LayerMask.NameToLayer(h.ToString()); // turn on
            }
        }
    }

    void hueMode_Move() {
        hueMode_Normal();
    }

    void hueMode_Blind() {
        canChangeHue = false;
    }

    public void blindMode(float blindDuration) {
        hueMode = HueMode.Blind;
        changedSettings = false;
        Invoke("BackToNormal", blindDuration);
    }

    void BackToNormal() {
        hueMode = HueMode.Normal;
        changedSettings = false;
        canChangeHue = true;
    }

    public string getHueMode() {
        return hueMode.ToString();
    }

    public void setHueMode(string newHueMode) {
        foreach(HueMode h in Enum.GetValues(typeof(HueMode))) {
            if(h.ToString() == newHueMode) {
                hueMode = h;
            }
        }
    }
}
