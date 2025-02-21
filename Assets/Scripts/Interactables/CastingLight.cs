using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingLight : MonoBehaviour {

    public Color currentColor;
    public bool turnedOn;

    Ray ray;
    RaycastHit hit;
    LineRenderer lightRay;
    Material lightRay_Material;

    GameObject previousHit;

    public LayerMask maskToCollideRay;

    void Start() {
        lightRay = GetComponent<LineRenderer>();
        lightRay_Material = lightRay.material;
        currentColor = lightRay_Material.GetColor("_EmissionColor");
        lightRay_Material.color = new Color(lightRay_Material.color.r, lightRay_Material.color.g, lightRay_Material.color.b, 0);
    }

    void Update() {
        lightRay_Material.SetColor("_EmissionColor", currentColor);
        if (turnedOn) {
            lightRay_Material.color = new Color(lightRay_Material.color.r, lightRay_Material.color.g, lightRay_Material.color.b, 1);
        } else {
            lightRay_Material.color = new Color(lightRay_Material.color.r, lightRay_Material.color.g, lightRay_Material.color.b, 0);
        }

        ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);   

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, maskToCollideRay, QueryTriggerInteraction.Ignore) && turnedOn) {
            previousHit = hit.transform.gameObject;
            if(hit.transform.GetComponentInParent<Mirror>() != null) {
                hit.transform.GetComponentInParent<Mirror>().castColor = currentColor;
                hit.transform.GetComponentInParent<Mirror>().hitByRayCast = true;
                Debug.DrawLine(ray.origin, hit.point, Color.red);
             } else if (hit.transform.GetComponentInParent<LightReceiver>() != null) {
                SendMessageTo(hit.transform.gameObject, "OnEnterRayCast", currentColor);
            }
        } else {
                if(previousHit != null && previousHit.GetComponentInParent<Mirror>() != null) {
                    SendMessageTo(previousHit, "OnExitRaycast");
                }
            previousHit = null;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Everything"), QueryTriggerInteraction.Ignore)) {
            lightRay.SetPosition(0, transform.position);
            lightRay.SetPosition(1, hit.point);
        }
    }

    void SendMessageTo(GameObject target, string message) {
        if (target) {
            target.transform.GetComponentInParent<Mirror>().SendMessage(message, gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void SendMessageTo(GameObject target, string message, Color giveColor) {
        if (target) {
            target.transform.GetComponentInParent<LightReceiver>().SendMessage(message, giveColor, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnDrawGizmos() {
        ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);
        Gizmos.color = Color.red;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Everything"), QueryTriggerInteraction.Ignore)) {
            if (hit.transform.GetComponentInParent<Mirror>() != null) {
                Gizmos.color = Color.green;
            } else if (hit.transform.GetComponentInParent<LightReceiver>() != null) {
                Gizmos.color = Color.yellow;
            }
            //Gizmos.DrawLine(ray.origin, hit.point);
        }
    }

}
