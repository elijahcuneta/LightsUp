using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAttack : MonoBehaviour {

    [SerializeField]
    FallNormalSphere normalSphere;

    [SerializeField]
    FallBlindSphere blindSphere;

    FallNormalSphere refNormalSphere;
    FallBlindSphere refBlindSphere;

    [HideInInspector]
    public Transform target;

    public Transform attackPosition;

    public enum AttackMode { Normal, Blind, Ray };
    public AttackMode attackMode = AttackMode.Normal;

    public delegate void FallAttackMode();
    public event FallAttackMode fallAttackMode;

    FallHealth fallHealth;
    List<FallAttackMode> fallAttacks;

    void Start() {
        fallHealth = GetComponent<FallHealth>();
        fallAttacks = new List<FallAttackMode>();

        fallAttackMode += NormalAttack;
        fallHealth.fallDead += FallDead;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        AddAttack(NormalAttack);
    }

    public void NormalAttack() {
        attackMode = AttackMode.Normal;
    }
     
    public void BlindAttack() {
        attackMode = AttackMode.Blind;
    }

    public void RayAttack() {
        attackMode = AttackMode.Ray;
    }

    public void AddAttack(FallAttackMode attackMode) {
        fallAttacks.Add(attackMode);
    }

    public void StartAttack() {
        if (fallAttackMode != null) {
            fallAttacks[Random.Range(0, fallAttacks.Count)]();
        }
    }

    public void FallDead() {
        for(int i = 0; i < fallAttacks.Count; i++) {
            fallAttackMode -= fallAttacks[i];
            fallAttacks.Remove(fallAttacks[i]);
        }
        Debug.Log("FallAttackClass: Dead");

        fallHealth.fallDead -= FallDead;
    }

    public void InstantiateNormalAttack() {
        FallNormalSphere fms = Instantiate(normalSphere, transform.position, Quaternion.identity);
        refNormalSphere = fms;
    }

    public void DeployNormalAttack() {
        refNormalSphere.deploy = true;
        refNormalSphere.Deploy(target.position);
    }

    public void InstantiateBlindAttack() {
        FallBlindSphere fbs = Instantiate(blindSphere, transform.position, Quaternion.identity);
        refBlindSphere = fbs;
    }

    public void DeployBlindAttack() {
        refBlindSphere.deploy = true;
        refBlindSphere.Deploy(target);
    }


}
