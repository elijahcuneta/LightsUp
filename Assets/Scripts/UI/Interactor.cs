using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Scripts))]
public class Interactor : MonoBehaviour {

    Scripts myScripts;
    DialogueManager diaManager;
    PlayerMovement playerMovement;

    bool canTalk;
    float timer;

    void Start() {
        myScripts = GetComponent<Scripts>();
        diaManager = FindObjectOfType<DialogueManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update() {
        if (canTalk && playerMovement.isGrounded && (InputManager.Cross_ButtonDown() || (Input.GetKeyDown(KeyCode.E)))) {
            if (!diaManager.startConversation) {
                myScripts.TriggerDialogue();
            } else {
                myScripts.TriggerNextDialogue();
            }
        }

        if (!JoystickChecker.joystickMode && !playerMovement.playerCanJump) {
            playerMovement.playerCanJump = true;
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<PlayerOrbColorIndicator>().lightMode_Interactable();
            canTalk = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<PlayerOrbColorIndicator>().lightMode_Original();
            canTalk = false;
            if (JoystickChecker.joystickMode) {
                playerMovement.playerCanJump = true;
            }
        }
    }

    void OnTriggerStay(Collider col) {
        if(col.tag == "Player") {
            canTalk = true;
            if (JoystickChecker.joystickMode && playerMovement.playerCanJump) {
                playerMovement.playerCanJump = false;
            }
        }
    }

}
