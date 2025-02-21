using UnityEngine;

public class ColliderCinematicTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            FindObjectOfType<Level1EventManager>().playCinematic();
            Destroy(gameObject);
        }
    }
}
