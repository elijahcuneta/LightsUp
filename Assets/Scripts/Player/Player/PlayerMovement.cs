using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public float speed = 4, jumpPower = 20, dashPower = 20, speedWhenPushing = 2;
    public bool canDoubleJump;

    [HideInInspector]
    public float startingSpeed;

    [HideInInspector]
    public bool move, isGrounded, pickedUpWeapon, isDashing, playerCanJump, playerCanDoAnything, playerCanTurn, playerCanAnimate, playerCanMove, playerCanMoveObject, playerCanRotateObject, willRotate;

    [SerializeField]
    PlayerCamera playerCamera;

    [SerializeField]
    AudioClip playerRunSFX, playerTouchGroundSFX, playerDashSFX;

    enum PlayerFacing { Left, Right, Up, Down };
    PlayerFacing playerFacing = new PlayerFacing();

    Animator playerAnim;
    AnimatorStateInfo playerAnimStateInfo;
    Rigidbody rbPlayer;
    Vector3 velocity;
    Collision infoCol;
    AudioSource playerAudioSource;
    RaycastHit hit, obstacleHit;

    [HideInInspector]
    public bool canPush;

    private bool  doubleJumped, willPush, willPull, obstacleInFront;
    private float moveInput, getJumpPower, timeMaxtoZero = 0.2f, decelRatePerSec, forwardVelocity, turnSpeed
    , directionSpeed, direction, rotationDegreePerSecond = 120f;

    [HideInInspector]
    public float locomotionThreshHold { get { return 0.2f; } }

    void Awake() {
        startingSpeed = speed;
        turnSpeed = 10;

        playerCanDoAnything = playerCanJump = playerCanTurn = playerCanAnimate = playerCanMove = playerCanMoveObject = true;

        rbPlayer = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<PlayerCamera>();

        playerAudioSource = GetComponent<AudioSource>();

        decelRatePerSec = -dashPower / timeMaxtoZero;
    }

    void Update() {
       if (playerCanDoAnything) {
         playerAnimStateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
         move = (InputManager.MainLeftJoystick_Raw().x != 0 || InputManager.MainLeftJoystick_Raw().z != 0) && playerAnimStateInfo.IsTag("canMove") && !willRotate;
         isGrounded = checkGround();
         obstacleInFront = CheckInFront();
        
            #region Move
            if (InputManager.MainLeftJoystick_Raw().z != 0) {
            moveInput = InputManager.MainLeftJoystick().z;
        } else if (InputManager.MainLeftJoystick_Raw().x != 0) {
            moveInput = InputManager.MainLeftJoystick().x;
        } else {
            moveInput = 0;
        }
            #endregion

            #region Jump
        if (playerCanJump) {
            if (canDoubleJump) {
                if ((InputManager.Cross_ButtonDown() || Input.GetKeyDown(KeyCode.Space)) && !doubleJumped){
                    getJumpPower = jumpPower;
                if (!isGrounded) {
                    doubleJumped = true;
                    getJumpPower *= .75f;
                }
                    Jumping(getJumpPower);
                }
            } else {
                if ((InputManager.Cross_ButtonDown() || Input.GetKeyDown(KeyCode.Space)) && isGrounded) {
                    getJumpPower = jumpPower;
                    Jumping(getJumpPower);
                }
            }
        }
            #endregion

            #region Dash
            if ((Input.GetButtonDown("Fire2") || InputManager.RightTrigger_Raw() == 1) && !canDoubleJump && isGrounded && !isDashing && move) {
            if(playerAudioSource.clip != playerDashSFX) {
                playerAudioSource.clip = playerDashSFX;
            }
            playerAudioSource.Play();
            rbPlayer.AddForce(transform.forward * dashPower * 10);
            forwardVelocity = transform.forward.z * dashPower;
            isDashing = true;
            playerCanTurn = false;
            StopCoroutine(Dash());
            StartCoroutine(Dash());
        }
        #endregion
         
         if (playerCanTurn)
             Turning();
         if (playerCanAnimate && playerCamera.cameraMode != PlayerCamera.CameraMode.FirstPerson)
             Animating();
         if (playerCanMoveObject)
             MoveObject();
         if (playerCanRotateObject)
             RotateObject();
         }
    }

    void FixedUpdate() {
        if (playerCanDoAnything) {
            if (((moveInput >= 0 && InputManager.MainLeftJoystick().x >= 0) || (moveInput < 0 && InputManager.MainLeftJoystick().x < 0)) && playerCanTurn){
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (InputManager.MainLeftJoystick().x < 0 ? -1 : 1), 0), Mathf.Abs(InputManager.MainLeftJoystick().x));
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            transform.rotation = (transform.rotation * deltaRotation);
            }
            if (playerCanMove && (moveInput != 0)) {
                Moving(moveInput);
            }
            if (obstacleInFront)
                playerCanMove = false;
            else
                playerCanMove = true;
        }
    }

    IEnumerator Dash() {
        playerCanJump = false;
        while (forwardVelocity > 0) {
            forwardVelocity += decelRatePerSec * Time.deltaTime;
            forwardVelocity = Mathf.Max(forwardVelocity, 0);
            rbPlayer.velocity = transform.forward * forwardVelocity;
            move = false;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        playerCanTurn = true;
        playerCanJump = true;
        rbPlayer.velocity = Vector3.zero;
    }

    void Moving(float moveInput) {
        if (move) {
            velocity = transform.TransformDirection(new Vector3(0, 0, moveInput < 0 ? -moveInput : moveInput));
            velocity = velocity.normalized * speed;
            rbPlayer.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

            if (playerAudioSource.clip != playerRunSFX) {
                playerAudioSource.clip = playerRunSFX;
            }
            if (!playerAudioSource.isPlaying && isGrounded) {
                playerAudioSource.Play();
            }
        }
    }

    void Jumping(float jumpPower) {
        rbPlayer.velocity = Vector3.up * jumpPower;
    }

    void Turning() {
        if (move) {
            if (InputManager.MainLeftJoystick_Raw().z == 1) {
                Quaternion rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                playerFacing = PlayerFacing.Up;
                move = true;
            } if (InputManager.MainLeftJoystick_Raw().z == -1) {
                Quaternion rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
                rotation *= Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                playerFacing = PlayerFacing.Down;
                move = true;
            } if (InputManager.MainLeftJoystick_Raw().x == -1) {
                Quaternion rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
                rotation *= Quaternion.Euler(0, -90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                playerFacing = PlayerFacing.Left;
                move = true;
            } if (InputManager.MainLeftJoystick_Raw().x == 1) {
                Quaternion rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
                rotation *= Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                playerFacing = PlayerFacing.Right;
                move = true;
            }
        }
    }

    void Animating() {
        playerAnim.SetBool("Walking", move);
        playerAnim.SetBool("OnGround", isGrounded);
        playerAnim.SetBool("Push", willPush);
        playerAnim.SetBool("Pull", willPull);
        playerAnim.SetBool("HasWeapon", pickedUpWeapon);
        if (gameObject.name == "Peter") {
            playerAnim.SetBool("Dash", isDashing);
        } else if(gameObject.name == "Mint") {
            playerAnim.SetBool("DoubleJump", doubleJumped);
        }
    }

    void PickUpWeapon() {
        FindObjectOfType<PlayerWeaponPickUp>().PickUpWeapon(GameObject.FindGameObjectWithTag("Weapon").transform);
    }

    void MoveObject() {
        if (canPush && InputManager.Triangle_Button()) {
            if (playerFacing == PlayerFacing.Up) {
                speed = InputManager.MainLeftJoystick_Raw().z > 0 ? speedWhenPushing : -speedWhenPushing;
            } else if (playerFacing == PlayerFacing.Down) {
                speed = InputManager.MainLeftJoystick_Raw().z > 0 ? -speedWhenPushing : speedWhenPushing;
            } else if (playerFacing == PlayerFacing.Left) {
                speed = InputManager.MainLeftJoystick_Raw().x > 0 ? -speedWhenPushing : speedWhenPushing;
            } else if (playerFacing == PlayerFacing.Right) {
                speed = InputManager.MainLeftJoystick_Raw().x > 0 ? speedWhenPushing : -speedWhenPushing;
            }

            if (speed <= 0) {
                willPull = true;
                willPush = false;
            } else if (speed > 0) {
                willPull = false;
                willPush = true;
            }

            if(GameObject.FindGameObjectWithTag("Weapon") != null) {
                if (GameObject.FindGameObjectWithTag("Weapon").transform.parent != null) {
                    GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }

            if (InputManager.MainLeftJoystick().z != 0) {
                Vector3 objectVelocity;
                objectVelocity = transform.TransformDirection(new Vector3(0, 0, moveInput < 0 ? -moveInput : moveInput));
                objectVelocity = objectVelocity.normalized * speed;
                infoCol.collider.GetComponent<Rigidbody>().isKinematic = false;
                infoCol.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                infoCol.collider.GetComponent<Rigidbody>().velocity = velocity;
            }

            playerCanTurn = false;
        }
        if (canPush && InputManager.Triangle_ButtonUp()) {
            if (GameObject.FindGameObjectWithTag("Weapon") != null) {
                if (GameObject.FindGameObjectWithTag("Weapon").transform.parent != null) {
                    GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<MeshRenderer>().enabled = true;
                }
            }
            infoCol.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            speed = startingSpeed;
            willPull = false;
            willPush = false;
            canPush = false;
            playerCanTurn = true;
        }
    }

    void RotateObject() {
        if (InputManager.Triangle_ButtonDown() && !isDashing) {
            willRotate = !willRotate;
            if (willRotate) {
                playerCanTurn = false;
                playerCanJump = false;
                
            } else {
                playerCanTurn = true;
                playerCanJump = true;
            }
        }
    }

    void refreshVelocity() {
        rbPlayer.velocity = Vector3.zero;
    }

    bool checkGround() {
        return Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.down) * 10f, out hit, 1, LayerMask.NameToLayer("PlayerCollider"));
    }
    
    bool CheckInFront() {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward) * 0.05f, Color.red);
        return Physics.Raycast(transform.position + new Vector3(0, 0.15f, 0), transform.TransformDirection(Vector3.forward) * 0.05f, out obstacleHit, 0.4f, LayerMask.NameToLayer("PushableObject"));
    }

    void OnCollisionEnter(Collision col) {
        if (!isGrounded) {
            playerAudioSource.clip = playerTouchGroundSFX;
            playerAudioSource.Play();
        }
        doubleJumped = false;
    }

    void OnCollisionStay(Collision col) {
        if (col.collider.tag == "Pushable") {
            canPush = true;
            infoCol = col;
        }
        if (col.collider.tag == "StickPlatform") {
            transform.parent = col.transform;
        }
    }

    void OnCollisionExit(Collision col) {
        if(col.collider.tag == "Pushable") {
            col.collider.GetComponent<Rigidbody>().isKinematic = false;
        }
        if (col.collider.tag == "StickPlatform") {
            transform.parent = null;
            transform.localScale = Vector3.one;
        }
    }

    void OnTriggerStay(Collider col) {
        if (col.tag == "Weapon" && InputManager.Triangle_ButtonDown() && !pickedUpWeapon) {
            playerAnim.SetTrigger("Pick Up Weapon");
            pickedUpWeapon = true;
        }
        if(col.tag == "Rotatetable" && col.GetComponentInParent<Rotatetable>() != null) {
            playerCanRotateObject = true;
            if (willRotate) {
                col.GetComponentInParent<Rotatetable>().canRotate = true;
                transform.parent = col.transform;
                rbPlayer.isKinematic = true;
                playerCamera.setRotateCameraMode(col.transform.parent, col.transform.parent.GetChild(0).forward ,new Vector3(0,0.03f,0), new Vector3(15,0,0));
                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer(gameObject.tag)); // turn off
            } else {
                col.GetComponentInParent<Rotatetable>().canRotate = false;
                transform.parent = null;
                rbPlayer.isKinematic = false;
                playerCamera.restoreCameraMode();
                Camera.main.cullingMask |= 1 << LayerMask.NameToLayer(gameObject.tag); // turn on
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Rotatetable" && col.GetComponentInParent<Rotatetable>() != null) {
            playerCanRotateObject = false;
            playerCanTurn = true;
            playerCanJump = true;
            col.GetComponentInParent<Rotatetable>().canRotate = false;
        }
    }
}
