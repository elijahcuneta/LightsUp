using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

    [SerializeField]
    string missionText;

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            setMission();
            //Destroy(gameObject);
        }
    }

    void setMission() {
        FindObjectOfType<MissionManager>().setText(missionText);
    }


}
