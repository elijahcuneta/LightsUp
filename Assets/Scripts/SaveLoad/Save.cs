using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour {

    [SerializeField]
    Sprite savingText;

    public delegate void SaveGame();
    public event SaveGame saveMyGame;

    Scene myScene;

    bool saving;

    void Start() {
        myScene = SceneManager.GetActiveScene();
    }

    void LateUpdate() {
        if (saving) {
            Time.timeScale = 0;
        }
    }

    void OnTriggerStay(Collider col) {
        if (InputManager.Triangle_ButtonDown() && col.tag == "Player") {
            StartCoroutine(SaveGameFX());
            SaveMyGame(col);
            saving = true;
            Level1EventManager.newGame = false;
        }
    }

    void SaveMyGame(Collider col) {
        myScene = SceneManager.GetActiveScene();
        SaveManager.Instance.SavePlayer(col.transform.position, col.name, myScene.name, PlayerHealth.currentHealth, PlayerHealth.currentMana, FindObjectOfType<PlayerMovement>().pickedUpWeapon);
        FindObjectOfType<InstructionManager>().setInstruction(savingText, 3);
    }

    IEnumerator SaveGameFX() {
        yield return new WaitForSecondsRealtime(2);
        StopAllCoroutines();
        saving = false;
    }
}
