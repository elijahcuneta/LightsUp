using UnityEngine.EventSystems;
using UnityEngine;

public class MainMenuButtonSelected : MonoBehaviour, ISelectHandler, IDeselectHandler {

    [SerializeField]
    GameObject peterLight, mintLight;

    [SerializeField]
    bool peterButton;

    public void OnSelect(BaseEventData eventData) {
        if (peterButton) {
            peterLight.SetActive(true);
        } else {
            mintLight.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData) {
        if (peterButton) {
            peterLight.SetActive(false);
        } else {
            mintLight.SetActive(false);
        }
    }
}
