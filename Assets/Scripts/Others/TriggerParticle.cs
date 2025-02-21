using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour {

    [SerializeField]
    GameObject[] particles;

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            foreach(GameObject p in particles) {
                p.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            foreach (GameObject p in particles) {
                p.SetActive(false);
            }
        }
    }

}
