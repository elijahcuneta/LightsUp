using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInteract : MonoBehaviour {

    [SerializeField]
    GameObject buttonShow_JS, buttonShow_KB;

    void Start () {
        buttonShow_JS.SetActive(false);
        buttonShow_KB.SetActive(false);
    }

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            if (JoystickChecker.joystickMode) {
                buttonShow_JS.SetActive(true);
            } else {
                buttonShow_KB.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
             buttonShow_JS.SetActive(false);
             buttonShow_KB.SetActive(false);
        }
    }
}
