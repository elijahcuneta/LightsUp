using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour {


    public List<Dialogue> scripts = new List<Dialogue>();

    public int indexOfScript;

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(scripts[indexOfScript]);
    }

    public void TriggerNextDialogue() {
        FindObjectOfType<DialogueManager>().NextDialogue();
    }

}
