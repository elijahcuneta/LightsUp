using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseImageMouseUp : MonoBehaviour, ISelectHandler, IDeselectHandler {

    [SerializeField]
    Sprite selectedSprite, notSelectedSprite;

    [SerializeField]
    GameObject target;

    [SerializeField]
    float changeSize = 20f;

    public void OnSelect(BaseEventData eventData) {
        target.GetComponent<Image>().sprite = selectedSprite;
        target.GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
        target.GetComponent<RectTransform>().sizeDelta = new Vector2(target.GetComponent<RectTransform>().sizeDelta.x + changeSize, target.GetComponent<RectTransform>().sizeDelta.y + changeSize);
    }

    public void OnDeselect(BaseEventData eventData) {
        target.GetComponent<Image>().sprite = notSelectedSprite;
        target.GetComponent<Image>().color = GetComponent<Button>().colors.normalColor;
        target.GetComponent<RectTransform>().sizeDelta = new Vector2(target.GetComponent<RectTransform>().sizeDelta.x - changeSize, target.GetComponent<RectTransform>().sizeDelta.y - changeSize);
    }
}
