using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUI : MonoBehaviour {

    [SerializeField]
    string placeText;

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            setPlace();
            //Destroy(gameObject);
        }
    }

    public void setPlace() {
        FindObjectOfType<PlaceManager>().setText(placeText);
    }
}
