using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceManager : MonoBehaviour {

    [SerializeField]
    Text placeText;

    Animator placeAnimator;

    void Start() {
        placeAnimator = GetComponent<Animator>();
    }

    public void setText(string newText) {
        placeText.text = newText;
        placeAnimator.SetTrigger("PlaceShow");
    }

    public void ChangeOriginFill() {
        GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
    }
}
