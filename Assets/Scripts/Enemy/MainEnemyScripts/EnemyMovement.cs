using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {

    public float speed;

    EnemyHealth enemHealth;

    Rigidbody rbEnemy;
    Vector3 velocity;
    Vector3 currentPosition;

    [HideInInspector]
    public bool move;

    void Start() {
        rbEnemy = GetComponent<Rigidbody>();
        enemHealth = GetComponent<EnemyHealth>();
    }

    void Update() {
        if(currentPosition != transform.position) {
            move = true;
        } else {
            move = false;
        }
        currentPosition = transform.position;
    }

    public void Movement(float moveInput) {
        velocity = transform.TransformDirection(new Vector3(0, 0, moveInput < 0 ? -moveInput : moveInput));
        velocity = velocity.normalized * speed;
        rbEnemy.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    public void Turning(Transform targetTransform, float rotationSpeed) {
        Vector3 lookAtThisTarget = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
        transform.LookAt(lookAtThisTarget);
    }

    public void freezeEnemy() {
        speed = 0;
    }

    public void resetVelocity() {
        velocity = Vector3.zero;
    }
}
