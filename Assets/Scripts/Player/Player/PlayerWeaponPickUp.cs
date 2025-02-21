using UnityEngine;

public class PlayerWeaponPickUp : MonoBehaviour {

    public void PickUpWeapon(Transform weapon) {
        weapon.parent = transform;
        weapon.localPosition = Vector3.zero;
        weapon.localEulerAngles = Vector3.zero;

        if(FindObjectOfType<Level1EventManager>() != null) {
            FindObjectOfType<Level1EventManager>().pickedUpWeapon();
        }

        if (GameObject.FindGameObjectWithTag("Player").name == "Peter") {
            FindObjectOfType<PeterAttack>().enabled = true;
        } else if (GameObject.FindGameObjectWithTag("Player").name == "Mint") {
            FindObjectOfType<MintAttack>().enabled = true;
            //weapon.localEulerAngles = Vector3.up * 120;
        }
        if (FindObjectOfType<Level1EventManager>() != null) {
            FindObjectOfType<Level1EventManager>().showEnemies();
        }
    }
}
