using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionUI : MonoBehaviour {

    [SerializeField]
    Sprite instTextForJS_Peter, instTextForKB_Peter;

    [SerializeField]
    Sprite instTextForJS_Mint, instTextForKB_Mint;

    bool showMode, peterPicked;

    void Start() {
        if (GameObject.Find("Peter") != null) {
            peterPicked = true;
        } else if (GameObject.Find("Mint") != null) {
            peterPicked = false;
        }
    }

    void Update() {
        if (showMode) {
            if (JoystickChecker.joystickMode) {
                setInstruction(peterPicked ? instTextForJS_Peter : instTextForJS_Mint);
            } else {
                setInstruction(peterPicked ? instTextForKB_Peter : instTextForKB_Mint);
            }
            showMode = false;
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            showMode = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<InstructionManager>().hideInstruction();
        }
    }

    public void setInstruction(Sprite instruction) {
        FindObjectOfType<InstructionManager>().setInstruction(instruction);
    }
}
