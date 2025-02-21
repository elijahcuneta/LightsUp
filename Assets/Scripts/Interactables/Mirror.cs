using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

    [HideInInspector]
    public Color castColor;

    [HideInInspector]
    public bool hitByRayCast;


    [SerializeField]
    Collider reflectionTarget;

    public LayerMask maskToCollideRay;

    Ray ray = new Ray();
    RaycastHit hit;
    LineRenderer lightRay;
    Material lightRay_Material;


    GameObject previousHit;

    void Start() {
        lightRay = GetComponent<LineRenderer>();
        lightRay_Material = lightRay.material;
        castColor = lightRay_Material.GetColor("_EmissionColor");
        lightRay_Material.color = new Color(lightRay_Material.color.r, lightRay_Material.color.g, lightRay_Material.color.b, 0);
    }

    void Update() {
        if (!hitByRayCast) {
            lightRay_Material.color = new Color(lightRay_Material.color.r, lightRay_Material.color.g, lightRay_Material.color.b, 0);
        } else {
            lightRay_Material.color = new Color(lightRay_Material.color.r, lightRay_Material.color.g, lightRay_Material.color.b, 1);
        }

        ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);
        lightRay_Material.SetColor("_EmissionColor", castColor);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, maskToCollideRay, QueryTriggerInteraction.Ignore) && hitByRayCast) {
            previousHit = hit.transform.gameObject;
            if (hit.transform.GetComponentInParent<Mirror>() != null) {
                hit.transform.GetComponentInParent<Mirror>().castColor = castColor;
                hit.transform.GetComponentInParent<Mirror>().hitByRayCast = true;
            } else if (hit.transform.GetComponentInParent<LightReceiver>() != null) {
                SendMessageTo(hit.transform.gameObject, "OnEnterRayCast", castColor);
            }
        } else {
                if (previousHit != null && previousHit.GetComponentInParent<Mirror>() != null) {
                    SendMessageTo(previousHit, "OnExitRaycast");
                }
            previousHit = null;
        }

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Everything"), QueryTriggerInteraction.Ignore)) {
            lightRay.SetPosition(0, transform.position);
            lightRay.SetPosition(1, hit.point);
        }
    }

    void OnExitRaycast(GameObject sender) {
        hitByRayCast = false;
    }

    void OnEnterShadowPiece(GameObject sender) {
        reflectionTarget.isTrigger = false;
    }

    void OnExitShadowPiece(GameObject sender) {
        reflectionTarget.isTrigger = true;
    }

    void SendMessageTo(GameObject target, string message) {
        if (target) {
            target.transform.GetComponentInParent<Mirror>().SendMessage(message, gameObject, SendMessageOptions.DontRequireReceiver);
        }
        hitByRayCast = false;
    }

    void SendMessageTo(GameObject target, string message, Color giveColor) {
        if (target) {
            target.transform.GetComponentInParent<LightReceiver>().SendMessage(message, giveColor, SendMessageOptions.DontRequireReceiver);
        }
        hitByRayCast = false;
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
