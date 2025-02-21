using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
    }

    public void ScreenMode(Toggle windowToggle) {
        Screen.fullScreen = !windowToggle.isOn;
    }

}
