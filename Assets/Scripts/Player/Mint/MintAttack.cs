using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MintAttack : MonoBehaviour {

    [SerializeField]
    GameObject arrow;

    [HideInInspector]
    public bool attacking;

    [Header("Starting Damage")]
    public int damage;

    int originalDamage;

    [SerializeField]
    Transform arrowSpawn;

    Animator playerAnim;
    PlayerMovement playerMove;
    Rigidbody rbPlayer;
    AnimatorStateInfo charPlayerStateInfo;

    void Awake() {
        playerMove = GetComponent<PlayerMovement>();
        playerAnim = GetComponent<Animator>();
        rbPlayer = GetComponent<Rigidbody>();

        originalDamage = damage;
    }

    void Update() {
        charPlayerStateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if ((InputManager.Square_Button() || Input.GetAxisRaw("Fire1") == 1) && !playerMove.move && playerMove.pickedUpWeapon) {
            Attacking();
        }
        Animating();
    }

    void Attacking() {
        attacking = true;
    }

    void AttackCancel() {
        attacking = false;
    }

    void SpawnArrow() {
        if(arrowSpawn != null) {
            Instantiate(arrow, arrowSpawn.position, transform.rotation);
        }
    }

    void DecreaseDamage() {
        damage--;
    }

    void RestoreDamage() {
        damage = originalDamage;
    }

    void Animating() {
        playerAnim.SetBool("Attack1", attacking);
    }
}
