using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkills { Taunt, Escape }

public class PlayerSkill : MonoBehaviour {

    PlayerOrbBehavior playerOB;
    HUDManager hudManager;
    PlayerMovement playerMovement;

    PlayerSkills playerSkills = PlayerSkills.Taunt;

    [HideInInspector]
    public bool usingSkill;

    [SerializeField]
    float tauntDuration = 5f, escapeDuration = 5f;

    [SerializeField]
    float tauntManaCost = 0f, escapeManaCost = 0f;

	void Start () {
        playerOB = GetComponentInChildren<PlayerOrbBehavior>();
        hudManager = FindObjectOfType<HUDManager>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	void Update () {
        if (InputManager.Circle_ButtonDown() && !usingSkill) {
            if (hudManager.playerSkills == PlayerSkills.Taunt) {
                Taunt();
            } else if(hudManager.playerSkills == PlayerSkills.Escape) {
                Escape();
            }
            usingSkill = true;
        }
    }

    public void Taunt() {
        if (PlayerHealth.currentMana >= tauntManaCost) {
            StartCoroutine(playerOB.Taunting(tauntDuration));
            PlayerHealth.currentMana -= tauntManaCost;
            playerSkills = PlayerSkills.Taunt;
        }
    }

    public void Escape() {
        if (PlayerHealth.currentMana >= escapeManaCost) {
            PlayerHealth.currentMana -= escapeManaCost;
            playerMovement.speed += 2f;
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            if (playerMovement.pickedUpWeapon) {
                GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            StartCoroutine(playerOB.Escaping(escapeDuration));
            if(FindObjectsOfType<EnemyMovement>() != null) {
                foreach(EnemyMovement e in FindObjectsOfType<EnemyMovement>()) {
                    e.GetComponent<BoxCollider>().enabled = false;
                }
            }
            playerSkills = PlayerSkills.Escape;
        }
    }

    public void ReturnPlayerStat() {
        playerMovement.speed = playerMovement.startingSpeed;
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        if (playerMovement.pickedUpWeapon) {
            GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        if (FindObjectsOfType<EnemyMovement>() != null) {
            foreach (EnemyMovement e in FindObjectsOfType<EnemyMovement>()) {
                e.GetComponent<BoxCollider>().enabled = transform;
            }
        }
        usingSkill = false;
    }
}
