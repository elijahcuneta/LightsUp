using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    EventSystem es;

    void Start() {
        es = FindObjectOfType<EventSystem>();
    }

    public void choosedPeter() {
        Level1EventManager.playerChoosedPeter = true;
        transitionNextScene();
    }

    public void choosedMint() {
        Level1EventManager.playerChoosedPeter = false;
        transitionNextScene();
    }

    //New Game
    void transitionNextScene() {
        File.Delete("LightsUpSave" + SaveSlot.saveSlot.ToString() + ".txt");
        Level1EventManager.newGame = true;
        SceneManager.LoadScene("Loading Screen");
        SceneLoader.sceneName = "Bedroom (Tutorial)";
    }

    public void setFocusedButton(Button buttonFocused) {
        es.SetSelectedGameObject(buttonFocused.gameObject);
    }

    public void Quit() {
        Application.Quit();
    }
}
