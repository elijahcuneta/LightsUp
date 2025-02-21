using UnityEngine;

public class RepositionPlayer : MonoBehaviour {

    [HideInInspector]
    public bool spawn;

	void LateUpdate () {
        if(GameObject.FindGameObjectWithTag("Player") != null && spawn) {
            GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
            Destroy(gameObject);
        }
        Destroy(gameObject);
	}
}
