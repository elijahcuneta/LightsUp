using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    [SerializeField]
    GameObject pausePanel, resume;

    [HideInInspector]
    public static bool pausePanelStatus;

    EventSystem es;

    void Start() {
        DontDestroyOnLoad(gameObject);
        pausePanel.SetActive(false);
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void LateUpdate () {
        pausePanel.SetActive(pausePanelStatus);

        if (InputManager.Start_ButtonDown()) {
            pausePanelStatus = !pausePanelStatus;
            if (pausePanelStatus) {
                EventSystem.current.SetSelectedGameObject(resume.gameObject);
                resume.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
            }
        }

        if (pausePanelStatus) {
            Time.timeScale = 0;
        }

    }



    public void OnClickResume() {
        Time.timeScale = 1;
        pausePanelStatus = false;
    }

    public void OnClickRestart() {
        Time.timeScale = 1;
        pausePanelStatus = false;
    }

    public void OnClickOption() {
        Time.timeScale = 1;
        pausePanelStatus = false;
    }

    public void OnClickExit() {
        pausePanelStatus = false;
        foreach(GameObject g in FindObjectsOfType<GameObject>()) {
            Destroy(g);
        }
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }
}
