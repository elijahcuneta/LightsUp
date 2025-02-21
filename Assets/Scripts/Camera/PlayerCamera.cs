using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct CameraPosition{
    Vector3 position;

    Transform xTransform;

    public Vector3 Position { get { return position; } set { position = value; } }
    public Transform XTransform { get { return xTransform; } set { xTransform = value; } }

    public void Init(string cameraName, Vector3 position, Transform transform, Transform parent) {
        this.position = position;
        xTransform = transform;
        xTransform.name = cameraName;
        xTransform.parent = parent;
        xTransform.localPosition = Vector3.zero;
        xTransform.localPosition = position;
    }
}

public class PlayerCamera : MonoBehaviour {

    public enum CameraMode { Behind, FirstPerson, Target, Parent}
    public CameraMode cameraMode = CameraMode.Behind;

    public LayerMask maskForProtectCamera;

    [SerializeField]
    float distanceAway, distanceUp, smooth;

    [SerializeField]
    Transform targetXForm;

    PlayerMovement playerMovement;

    Transform targetParent;

    Vector3 lookDirection, targetPosition, currentLookDirection;
    Vector3 velocityCameraSmooth = Vector3.zero, velocityLookDirection = Vector3.zero;
    Vector3 lookAt;

    CameraPosition firstPersonCamPos;

    [SerializeField]
    float firstPersonLookSpeed = 1.5f, FPSRotationDegreePerSecond = 120f, lookDirectionDampTime = 0.1f, turnSpeed = 4f;

    [SerializeField]
    Vector2 firstPersonXAxisClamp = new Vector2(-70f, 90f), thirdPersonXAxisClamp = new Vector2(-70f, 90f);

    const float freeRotationDegreePerSecond = -5f;

    float xAxisRotation = 0f, xAxisRotationThirdPerson = 0f;
    float cameraSmoothDampTime = 0.1f;
    float distanceAwayFree, distanceUpFree;

    bool fromTarget;
    float camRotationX;

    void Start() {
        DontDestroyOnLoad(gameObject);
        targetXForm = GameObject.FindGameObjectWithTag("PlayerTarget").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        lookDirection = targetXForm.forward;
        currentLookDirection = targetXForm.forward;

        Cursor.lockState = CursorLockMode.Locked;

        firstPersonCamPos = new CameraPosition();
        firstPersonCamPos.Init("First Person Camera", new Vector3(0, 0.2f, 0.5f), new GameObject().transform, targetXForm);
    }

    void LateUpdate () {
        
        if (targetXForm != null) {
            Vector3 characterOffset = targetXForm.position + new Vector3(0, distanceUp, 0);
            lookAt = characterOffset;

            if (!PauseManager.pausePanelStatus) {
                if (InputManager.LeftBumper_ButtonDown()) {
                    cameraMode = CameraMode.Target;
                    fromTarget = true;
                } else {
                    if (cameraMode == CameraMode.FirstPerson && (InputManager.DPad_Vertical_Raw() == -1 || Input.GetKeyDown(KeyCode.B)) || (cameraMode == CameraMode.Target)) {
                        cameraMode = CameraMode.Behind;
                        playerMovement.playerCanDoAnything = true;
                    } else if ((InputManager.DPad_Vertical_Raw() == 1 || Input.GetKeyDown(KeyCode.V)) && cameraMode != CameraMode.FirstPerson && !playerMovement.move) {
                        xAxisRotation = 0;
                        cameraMode = CameraMode.FirstPerson;
                        playerMovement.playerCanDoAnything = false;
                    }
                }
            }

            switch (cameraMode) {
                case CameraMode.Behind:
                    if (playerMovement.speed > playerMovement.locomotionThreshHold && playerMovement.move || fromTarget) {
                        lookDirection = Vector3.Lerp(targetXForm.right * (InputManager.MainRightJoystick().x < 0 ? 1f : -1f), targetXForm.forward * (InputManager.MainRightJoystick().x < 0 ? -1f : 1f), Mathf.Abs(Vector3.Dot(transform.forward, targetXForm.forward)));
                        currentLookDirection = Vector3.Normalize(characterOffset - transform.position);
                        currentLookDirection.y = 0;
                        currentLookDirection = Vector3.SmoothDamp(currentLookDirection, lookDirection, ref velocityLookDirection, lookDirectionDampTime);
                        fromTarget = false;
                    }
                    targetPosition = characterOffset + targetXForm.up * distanceUp - Vector3.Normalize(currentLookDirection) * distanceAway;
                    break;
                case CameraMode.Target:
                    lookDirection = targetXForm.forward;
                    targetPosition = characterOffset + targetXForm.up * distanceUp - lookDirection * distanceAway;
                    break;
                case CameraMode.FirstPerson:
                    xAxisRotation += (-InputManager.MainRightJoystick().z * firstPersonLookSpeed);
                    xAxisRotation = Mathf.Clamp(xAxisRotation, firstPersonXAxisClamp.x, firstPersonXAxisClamp.y);
                    firstPersonCamPos.XTransform.localRotation = Quaternion.Euler(xAxisRotation, 0, 0);
                    Quaternion rotationalShift = Quaternion.FromToRotation(transform.forward, firstPersonCamPos.XTransform.forward);
                    transform.rotation = rotationalShift * transform.rotation;

                    Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, FPSRotationDegreePerSecond * (InputManager.MainRightJoystick().x < 0f ? -1f : 1f), 0f), Mathf.Abs(InputManager.MainRightJoystick().x));
                    Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
                    playerMovement.transform.rotation = (playerMovement.transform.rotation * deltaRotation);

                    targetPosition = firstPersonCamPos.XTransform.position;

                    lookAt = Vector3.Lerp(targetPosition + targetXForm.forward, transform.position + transform.forward, cameraSmoothDampTime * Time.deltaTime);
                    lookAt = Vector3.Lerp(transform.position + transform.forward, lookAt, Vector3.Distance(transform.position, firstPersonCamPos.XTransform.position));

                    break;
                case CameraMode.Parent:
                    if (targetParent != null) {
                        transform.parent = targetParent;
                    }
                    break;
            }
            if (cameraMode != CameraMode.Parent) {
                protectFromWall(characterOffset, ref targetPosition);
                smoothPosition(transform.position, targetPosition);
                transform.LookAt(lookAt);
            }
        }
    }

    void smoothPosition(Vector3 fromPos, Vector3 toPos) {
        transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCameraSmooth, cameraSmoothDampTime);
    }

    void protectFromWall(Vector3 fromObject, ref Vector3 toTarget) {
        RaycastHit hit;
        if(Physics.Linecast(fromObject, toTarget, out hit, maskForProtectCamera, QueryTriggerInteraction.Ignore)) {
            toTarget = new Vector3(hit.point.x, toTarget.y, hit.point.z);
        }
    }

    public void setRotateCameraMode(Transform target, Vector3 lookHere, Vector3 positionOffset, Vector3 rotationOffset) {
        targetParent = target;
        transform.localPosition = positionOffset;
        transform.localEulerAngles = rotationOffset;
        transform.localScale = Vector3.one;
        cameraMode = CameraMode.Parent;
    }

    public void restoreCameraMode() {
        cameraMode = CameraMode.Behind;
        transform.parent = null;
    }
}