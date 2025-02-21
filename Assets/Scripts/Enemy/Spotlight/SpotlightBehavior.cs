using UnityEngine;

public class SpotlightBehavior : MonoBehaviour {


    [SerializeField]
    Vector3 startPosition, endPosition;

    [SerializeField]
    float speed = 5, speedWhenChasing = 3;

    Vector3 newEndPostion;

    bool endPoint, followTarget;
    float originalSpeed;

    #region For Inspector Buttons
    public void clickedSetStartPosition() {
        startPosition = transform.position;
    }

    public void clickedSetEndPosition() {
        endPosition = transform.position;
    }

    #endregion


    void Start() {
        transform.position = startPosition;
        newEndPostion = endPosition;

        originalSpeed = speed;
    }

    void Update() {
        if (transform.position == endPosition) {
            endPoint = true;
        } else if (transform.position == startPosition) {
            endPoint = false;
        }

        if (!followTarget) {
            if (endPoint) {
                newEndPostion = startPosition;
            } else {
                newEndPostion = endPosition;
            }
        }

        transform.localPosition = Vector3.MoveTowards(transform.position, newEndPostion, speed * Time.deltaTime);
    }

    public void changeTarget(Vector3 newTarget) {
        newEndPostion = new Vector3(newTarget.x, newEndPostion.y, newTarget.z);
        speed = speedWhenChasing;
        followTarget = true;
    }

    public void releaseTarget() {
        newEndPostion = startPosition;
        speed = originalSpeed;
        followTarget = false;
    }
}
