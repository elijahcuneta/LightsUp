using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPiece : MonoBehaviour {

    Ray ray;
    RaycastHit hit;

    GameObject previousHit;

    public LayerMask maskToCollide;

    void Update() {
        ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, maskToCollide, QueryTriggerInteraction.Ignore)) {
            previousHit = hit.transform.gameObject;
            if (hit.transform.tag == "ShadowTarget") {
                if (previousHit.GetComponentInParent<Mirror>() != null) {
                    SendMessageTo(previousHit, "OnEnterShadowPiece");
                }
            }
        } else {
            if (previousHit != null && previousHit.GetComponentInParent<Mirror>() != null) {
                SendMessageTo(previousHit, "OnExitShadowPiece");
            }
            previousHit = null;
        }
    }

    void SendMessageTo(GameObject target, string message) {
        if (target) {
            target.transform.GetComponentInParent<Mirror>().SendMessage(message, gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnDrawGizmos() {
        ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);
        Gizmos.color = Color.red;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Everything"), QueryTriggerInteraction.Ignore)) {
            if (hit.transform.tag == "ShadowTarget") {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(ray.origin, hit.point);
        }
    }
}
