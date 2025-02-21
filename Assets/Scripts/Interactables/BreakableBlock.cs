using UnityEngine;

public class BreakableBlock : MonoBehaviour {


    [SerializeField]
    AudioSource breakableAudioSource;

    [SerializeField]
    float force;

    void Start() {
        breakableAudioSource = GetComponentInParent<AudioSource>();
    }

    void OnCollisionEnter(Collision col) {
        if (col.collider.tag == "Player") {
            if (col.collider.GetComponent<PlayerMovement>().isDashing) {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(col.collider.transform.forward * 200);
                breakableAudioSource.Play();
                Destroy(gameObject, 3f);
            }
        }
    }
}
