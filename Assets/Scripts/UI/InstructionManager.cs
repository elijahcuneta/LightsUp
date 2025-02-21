using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {

    [SerializeField]
    Image instructionText;

    Animator instructionAnimator;

    void Start() {
        instructionAnimator = GetComponent<Animator>();
    }

    public void setInstruction(Sprite newInstruction) {
        if (newInstruction != null) {
            instructionText.sprite = newInstruction;
        }
        instructionAnimator.SetBool("InstructionShow", true);
    }

    public void setInstruction(Sprite newInstruction, float timer) {
        if(newInstruction != null) {
            instructionText.sprite = newInstruction;
        }
        instructionAnimator.SetBool("InstructionShow", true);
        Invoke("hideInstruction", timer);
    }

    public void hideInstruction() {
        instructionAnimator.SetBool("InstructionShow", false);
    }

    public void ChangeOriginFill() {
        GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
    }
}
