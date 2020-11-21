using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float gravity = 19.5f;
    [SerializeField] LayerMask groundLayerMask = 0;
    [SerializeField] LayerMask slideLayerMask = 0;
    float slideMaxDistance = 25;
    Vector3 velocity;
    float moveSpeed = 6.0f;
    float cameraPitch;
    float mouseSensitivity = 7.5f;
    bool paused = false;
    bool onGround;
    Transform playerCamera;
    Transform groundChecker;
    
    public void Start() {
        controller = GetComponent<CharacterController> ();
        playerCamera = transform.Find ("Camera");
        groundChecker = transform.Find ("GroundChecker");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SlideBlock() {
        Ray ray = new Ray (playerCamera.position, playerCamera.forward);
        Debug.DrawLine (ray.origin, ray.GetPoint (slideMaxDistance));
        RaycastHit hit;
        if(Physics.Raycast (ray, out hit, slideMaxDistance, slideLayerMask) ) {
            hit.collider.gameObject.GetComponent<SlidablePanel> ().Hit ();
        }
        
    }
    public void Update() {
        if ( Input.GetMouseButtonDown(0)) {
            SlideBlock ();
        }

        onGround = Physics.CheckSphere (groundChecker.position, 0.4f, groundLayerMask);

        if ( onGround ) {
            velocity.y = 0f;
        }

        if (Input.GetButtonDown ("Pause") ) {
            paused = !paused;
            Cursor.visible = paused;
            if ( paused ) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        float rotationHorizontal = Input.GetAxis ("RotateHorizontal") * mouseSensitivity;
        float rotationVertical = Input.GetAxis ("RotateVertical") * mouseSensitivity;
        float x = Input.GetAxis ("MoveStrafe");
        float z = Input.GetAxis ("MoveForward");

        Vector3 movement = transform.right * x + transform.forward * z;

        rotationHorizontal *= Time.deltaTime;
        rotationVertical *= Time.deltaTime;

        cameraPitch += rotationVertical;
        cameraPitch = Mathf.Clamp (cameraPitch, -90, 90);

        if ( !paused ) {
            transform.Rotate (0, rotationHorizontal, 0);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        }
        
        controller.Move (movement * moveSpeed * Time.deltaTime);

        velocity.y -= gravity;

        controller.Move (velocity * Time.deltaTime);


    }

}
