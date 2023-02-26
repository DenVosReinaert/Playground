using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Vector3 moveDir;
    Vector2 mouseRotationInput, playerRotation;

    public GameObject camera;
    public Vector2 cameraAngleConstraints;

    public Rigidbody playerRb;
    public float moveSpeed;

    public float jumpHeight;
    [SerializeField]
    GameObject groundingObj;
    [SerializeField]
    float groundingObjRadius;
    [SerializeField]
    LayerMask groundMask;
    public bool grounded;

    [SerializeField]
    bool moveLock;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseRotationInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

        if (!moveLock)
            if (Input.GetButtonDown("Jump") && grounded)
            {
                Jump();
            }

    }

    void FixedUpdate()
    {
        GroundCheck();
        Debug.DrawLine(groundingObj.transform.position, groundingObj.transform.position + -groundingObj.transform.up * groundingObjRadius, Color.yellow);

        if (moveLock != true)
        {
            playerRb.MovePosition(playerRb.position + (moveDir * moveSpeed * Time.deltaTime));



            #region Camera Controls

            playerRotation = new Vector2(playerRotation.x + mouseRotationInput.x, Mathf.Clamp(playerRotation.y + mouseRotationInput.y, cameraAngleConstraints.x, cameraAngleConstraints.y));

            playerRb.MoveRotation(Quaternion.Euler(0, playerRotation.x, 0));
            camera.transform.localRotation = Quaternion.Euler(transform.rotation.x + -playerRotation.y, playerRb.rotation.y, 0);

            #endregion
        }
    }

    bool GroundCheck()
    {
        Collider[] hitColliders = Physics.OverlapSphere(groundingObj.transform.position, groundingObjRadius, groundMask);

        foreach (Collider collider in hitColliders)
        {
            return grounded = true;
        }

        return grounded = false;
    }

    public void Jump()
    {
        playerRb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }
}
