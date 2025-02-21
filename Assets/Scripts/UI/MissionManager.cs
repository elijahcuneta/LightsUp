using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

    [SerializeField]
    Text missionText;

    Animator missionAnimator;

    void Start() {
        DontDestroyOnLoad(transform.root.gameObject);
        missionAnimator = GetComponent<Animator>();     
    }

    public void setText(string newText) {
        missionText.text = newText;
        missionAnimator.SetTrigger("MissionShow");
    }

    public void ChangeOriginFill() {
        GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
    }
}
