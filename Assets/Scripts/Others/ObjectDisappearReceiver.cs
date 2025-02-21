using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearReceiver : MonoBehaviour {

    [SerializeField]
    LightReceiver lightReceiver;

    [SerializeField]
    MeshRenderer[] meshToDisappear;

    [SerializeField]
    Collider[] colliderToDisappear;

    bool triggerChange;

	void Update () {
	    if(lightReceiver != null) {
            if(lightReceiver.getHitByRaycast()) {
                if (!triggerChange) {
                foreach(MeshRenderer m in meshToDisappear) {
                    m.enabled = false;
                }
                foreach(Collider c in colliderToDisappear) {
                    c.enabled = false;
                }
              }
                triggerChange = true;
            } else {
                triggerChange = false;
            }
        }	
	}
}
