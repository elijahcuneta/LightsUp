using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField]
    Text interactorName, interactorText, continueText;

    [SerializeField]
    public Queue<string> sentences;

    [HideInInspector]
    public bool startConversation;

    Animator dialogueBox;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        sentences = new Queue<string>();
        dialogueBox = GameObject.Find("MainCanvas").GetComponent<Animator>();
    }
    
    public void StartDialogue(Dialogue dialogue) {
        continueText.text = JoystickChecker.joystickMode ? "X" : "E";
        sentences.Clear();
        startConversation = true;
        foreach (string sentence in GameObject.Find("Peter") != null ? dialogue.peterSentences : dialogue.mintSentences) {
            sentences.Enqueue(sentence);
        }
        interactorName.text = dialogue.myName;
        FindObjectOfType<PlayerMovement>().playerCanDoAnything = false;
        dialogueBox.SetBool("OpenDialogueBox", startConversation);
        NextDialogue();
    }

    public void NextDialogue() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(animationText(sentence));
    }

    IEnumerator animationText(string sentences) {
        interactorText.text = "";
        foreach (char letter in sentences.ToCharArray()) {
            if(letter != ' ') {
                interactorText.text += letter;
            } else {
                interactorText.text += letter + " ";
            }
            yield return new WaitForEndOfFrame();
        }
    }

   
    void EndDialogue() {
        startConversation = false;
        dialogueBox.SetBool("OpenDialogueBox", startConversation);
        FindObjectOfType<PlayerMovement>().playerCanDoAnything = true;
    }


}
