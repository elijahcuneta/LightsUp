using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuePicker : MonoBehaviour {

    [SerializeField]
    float sizeChange = 20;

    float radius = 150f;
    PlayerOrbChangeHue playerHue;

    void Start() {
        playerHue = FindObjectOfType<PlayerOrbChangeHue>();
    }

    void Update() {
        if(InputManager.MainRightJoystick().x != 0 && InputManager.MainRightJoystick().z != 0 && (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)) {
            float x = InputManager.MainRightJoystick().x * radius;
            float y = InputManager.MainRightJoystick().z;

            y = y > 0 ? Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2)) : Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2)) * -1f;

            transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(x, y, 0), Time.unscaledDeltaTime * 150f);
        } else if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0) {
            transform.position = Input.mousePosition;
        } else if(Time.timeScale == 1){
            transform.localPosition = Vector3.zero;
        }

    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.GetComponent<HueMode>() != null && playerHue != null) {
            playerHue.setHueMode(col.GetComponent<HueMode>().huePicked.ToString());
            col.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(col.transform.GetComponent<RectTransform>().sizeDelta.x + sizeChange, col.transform.GetComponent<RectTransform>().sizeDelta.y + sizeChange);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.GetComponent<HueMode>() != null && playerHue != null) {
            col.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(col.transform.GetComponent<RectTransform>().sizeDelta.x - sizeChange, col.transform.GetComponent<RectTransform>().sizeDelta.y - sizeChange);
        }
    }


}
