using UnityEngine.SceneManagement;
using UnityEngine;

public class Level3EventManager : MonoBehaviour {

    SaveManager saveManager;

    [SerializeField]
    GameObject[] mandatoryObjects;

    void Awake() {
        saveManager = FindObjectOfType<SaveManager>();

        if (saveManager.saveState[SaveSlot.saveSlot].playerPosition != null && saveManager.saveState[SaveSlot.saveSlot].playerLocation == SceneManager.GetActiveScene().name) {
            saveManager.initializeGame.InitializePlayer(saveManager.saveState[SaveSlot.saveSlot].playerName, saveManager.saveState[SaveSlot.saveSlot].playerPosition,
                                                         saveManager.saveState[SaveSlot.saveSlot].health, saveManager.saveState[SaveSlot.saveSlot].mana, saveManager.saveState[SaveSlot.saveSlot].hasWeapon);
        } else {
            foreach (GameObject g in mandatoryObjects) {
                Destroy(g);
            }
            FindObjectOfType<RepositionPlayer>().spawn = true;
        }
    }

}
