using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour {

    Color gotColor, previousColor;
    MeshRenderer meshColorReceiver;

    bool hitByRaycast;

	void Start () {
        meshColorReceiver = GetComponentInChildren<MeshRenderer>();
        gotColor = previousColor = meshColorReceiver.material.color;
    }

    void Update () {
        if (gotColor != previousColor) {
            meshColorReceiver.material.color = gotColor;
        } else {
            meshColorReceiver.material.color = previousColor;
        }
	}

    void OnEnterRayCast(Color currentColor) {
        gotColor = currentColor;
        hitByRaycast = true;
    }

    void OnExitRaycast(GameObject sender) {
        gotColor = previousColor;
        hitByRaycast = false;
    }

    public bool getHitByRaycast() {
        return hitByRaycast;
    }
}
