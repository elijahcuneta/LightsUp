using System.Collections;
using UnityEngine;

public class JoystickChecker : MonoBehaviour {

    [SerializeField]
    Sprite joystickConnected, joystickDisconnected;

    public static bool joystickMode;

    void Start() {
        StartCoroutine(joystickCheck());
    }

    IEnumerator joystickCheck() {
        yield return new WaitForSecondsRealtime(1f);
        bool previousJoyStickStatus = joystickMode;
        joystickMode = false;
        for (int i = 0; i < Input.GetJoystickNames().Length; i++) {
            if (!string.IsNullOrEmpty(Input.GetJoystickNames()[i])) {
                joystickMode = true;
                i = Input.GetJoystickNames().Length;
            }
        }
        if (previousJoyStickStatus != joystickMode) {
            if (joystickMode) {
                FindObjectOfType<InstructionManager>().setInstruction(joystickConnected, 3f);
            } else {
                FindObjectOfType<InstructionManager>().setInstruction(joystickDisconnected, 3f);
            }
        }
        StopCoroutine(joystickCheck());
        StartCoroutine(joystickCheck());
    }

}
