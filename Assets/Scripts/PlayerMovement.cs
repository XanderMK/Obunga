using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform playerCam;
    public Transform groundCheck;
    public Transform mobamaHead;

    public float groundCheckRadius;

    private int playerMask = 1 << 3;
    private int allMask = ~0;

    private Rigidbody rb;
    private Camera cam;

    public float topSpeed;
    public float acceleration;
    public float airAcceleration;
    public float jumpHeight;

    public float vertMouseSensitivity;
    public float horiMouseSensitivity;

    private float yRot = 0f;
    private float xRot = 0f;
    private bool isThirdPerson = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = playerCam.GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

        playerMask = ~playerMask;
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal") * acceleration;
        float inputY = Input.GetAxis("Vertical") * acceleration;

        if (Physics.CheckSphere(groundCheck.position, groundCheckRadius, playerMask))
            rb.AddForce((transform.forward * inputY * acceleration) + (transform.right * inputX * acceleration), ForceMode.Acceleration);
        else
            rb.AddForce((transform.forward * inputY * airAcceleration) + (transform.right * inputX * airAcceleration), ForceMode.Acceleration);

        float ySpeed = rb.velocity.y;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        Vector3 horiMag = Vector3.ClampMagnitude(rb.velocity, topSpeed);

        rb.velocity = new Vector3(horiMag.x, ySpeed, horiMag.z);

    }

    void Update() {
        yRot -= Input.GetAxis("Mouse Y") * vertMouseSensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, -85f, 85f);

        xRot += Input.GetAxis("Mouse X") * horiMouseSensitivity * Time.deltaTime;

        
        if (!isThirdPerson) {
            playerCam.localRotation = Quaternion.Euler(Vector3.right * yRot);
        }

        mobamaHead.localRotation = Quaternion.Euler(Vector3.right * yRot);
        transform.rotation = Quaternion.Euler(Vector3.up * xRot);

        if (Input.GetKeyDown(KeyCode.F5)) {
            isThirdPerson = !isThirdPerson;
            if (isThirdPerson) {
                playerCam.SetParent(mobamaHead);
                cam.cullingMask = allMask;
                playerCam.localPosition = new Vector3(0f, 0.08341484f, -3f);
            } else {
                playerCam.SetParent(groundCheck.parent);
                cam.cullingMask = playerMask;
                playerCam.localPosition = new Vector3(0f, 1.73f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(groundCheck.position, groundCheckRadius, playerMask)) {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
