using UnityEngine;

public class SpotlightCollider : MonoBehaviour {

    SpotlightBehavior spotlightBehavior;

    void Start() {
        spotlightBehavior = GetComponentInParent<SpotlightBehavior>();
    }

    void OnTriggerStay(Collider col) {
        if (col.tag == "Player" && gameObject.name == "RadiusToFollow") {
            spotlightBehavior.changeTarget(col.transform.position);
        } else if (col.tag == "Player" && gameObject.name == "RadiusToKill") {
            if (!col.GetComponent<PlayerHealth>().isDead) {
                col.GetComponent<PlayerHealth>().Death();
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            spotlightBehavior.releaseTarget();
        }
    }
}
