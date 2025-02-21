using UnityEngine;

public class CinematicCamera : MonoBehaviour {

    public void stopCinematicCamera() {
        if (FindObjectOfType<PlayerMovement>() != null)
            FindObjectOfType<PlayerMovement>().enabled = true;
        GetComponent<Camera>().enabled = false;
    }

    public void playCinematicCamera() {
        if (FindObjectOfType<PlayerMovement>() != null)
            FindObjectOfType<PlayerMovement>().enabled = false;
    }
}
