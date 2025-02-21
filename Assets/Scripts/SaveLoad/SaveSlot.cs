using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

    public static int saveSlot = 0;

    [SerializeField]
    Button[] saveSlotButton;

    [SerializeField]
    bool loadSlot;

    SaveManager saveManager;

    void Awake() {
        saveManager = FindObjectOfType<SaveManager>();
        if (loadSlot) {
            for (int i = 0; i < saveSlotButton.Length; i++) {
                if (File.Exists("LightsUpSave" + i.ToString() + ".txt")) {
                    saveSlotButton[i].interactable = true;
                }
            }
        }
    }

    public void setSaveSlot(int saveSlotNumber) {
        saveSlot = saveSlotNumber;
        if (File.Exists("LightsUpSave" + saveSlot.ToString() + ".txt") && loadSlot) {
            TransitionScene();
        }
    }

    public void TransitionScene() {
        Level1EventManager.newGame = false;
        saveManager.load();
        SceneManager.LoadScene("Loading Screen");
    }
}
