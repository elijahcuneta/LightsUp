using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1EventManager : MonoBehaviour {

    [SerializeField]
    GameObject enemies;

    [SerializeField]
    GameObject sword, bow;

    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    GameObject nextLevel, cantNextLevel;

    SaveManager saveManager;

    public bool debugMode;

    public static bool playerChoosedPeter = true, newGame;
    bool debug_PeterChoose;

    void Awake() {
        saveManager = FindObjectOfType<SaveManager>();
        if (!debugMode) {
            if (playerChoosedPeter) {
                bow.SetActive(false);
                sword.SetActive(true);
            } else if(!playerChoosedPeter){
                sword.SetActive(false);
                bow.SetActive(true);
            }
        }

        if (newGame) {
            saveManager.initializeGame.InitializePlayerNewGame(playerChoosedPeter ? "Peter" : "Mint", FindObjectOfType<RepositionPlayer>().transform.position);
        } else if (saveManager.saveState[SaveSlot.saveSlot].playerPosition != null && saveManager.saveState[SaveSlot.saveSlot].playerLocation == SceneManager.GetActiveScene().name) {
            saveManager.initializeGame.InitializePlayer(saveManager.saveState[SaveSlot.saveSlot].playerName, saveManager.saveState[SaveSlot.saveSlot].playerPosition,
                                                        saveManager.saveState[SaveSlot.saveSlot].health, saveManager.saveState[SaveSlot.saveSlot].mana, saveManager.saveState[SaveSlot.saveSlot].hasWeapon);
            if(saveManager.saveState[SaveSlot.saveSlot].playerName == "Peter") {
                bow.SetActive(false);
                sword.SetActive(true);
            } else if(saveManager.saveState[SaveSlot.saveSlot].playerName == "Mint") {
                sword.SetActive(false);
                bow.SetActive(true);
            }

            if (saveManager.saveState[SaveSlot.saveSlot].hasWeapon) {
                bow.SetActive(false);
                sword.SetActive(false);
                Destroy(GameObject.Find("WeaponPickUpButton"));
            }
        }
        if (saveManager.saveState[SaveSlot.saveSlot].hasWeapon) {
            nextLevel.SetActive(true);
            cantNextLevel.SetActive(false);
        } else {
            nextLevel.SetActive(false);
            cantNextLevel.SetActive(true);
        }
    }

    public void showEnemies() {
        enemies.SetActive(true);
    }

    public void playCinematic() {
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<CinematicCamera>().GetComponent<Camera>().enabled = true;
        FindObjectOfType<CinematicCamera>().GetComponent<Animator>().SetTrigger("Next");
    }

    public void pickedUpWeapon() {
        nextLevel.SetActive(true);
        cantNextLevel.SetActive(false);
    }
}