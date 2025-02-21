using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSavedGame : MonoBehaviour {

    [SerializeField]
    GameObject peter, mint, sword, bow;

    GameObject player;

    public void InitializePlayer(string playerName, Vector3 position, float health, float mana, bool hasWeapon) {
        if (playerName == "Peter") {
            player = Instantiate(peter, peter.transform.position, peter.transform.rotation);
            player.gameObject.name = "Peter";
        } else if(playerName == "Mint") {
            player = Instantiate(mint, mint.transform.position, mint.transform.rotation);
            player.gameObject.name = "Mint";
        }

        if (hasWeapon) {
            GameObject playerWeapon = Instantiate(playerName == "Peter" ? sword : bow, Vector3.zero, Quaternion.identity);
            playerWeapon.transform.parent = FindObjectOfType<PlayerWeaponPickUp>().transform;
            playerWeapon.transform.localPosition = Vector3.zero;
            playerWeapon.transform.localEulerAngles = Vector3.zero;
            if(playerName == "Mint") {
                //playerWeapon.transform.localEulerAngles = Vector3.up * 120;
            }
            player.GetComponent<PlayerMovement>().pickedUpWeapon = true;
        }

        PlayerHealth.currentHealth = health;
        PlayerHealth.currentMana = mana;

        GameObject.FindGameObjectWithTag("Player").transform.position = position;
    }
   
    public void InitializePlayerNewGame(string playerName, Vector3 position) {
        if (playerName == "Peter") {
            GameObject player = Instantiate(peter, peter.transform.position, peter.transform.rotation);
            player.gameObject.name = "Peter";
        } else if (playerName == "Mint") {
            GameObject player = Instantiate(mint, mint.transform.position, mint.transform.rotation);
            player.gameObject.name = "Mint";
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = position;
    }

    public void InitializePlayer(Vector3 position) {
        GameObject.FindGameObjectWithTag("Player").transform.position = position;
    }

}
