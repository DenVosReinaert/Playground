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
    }

    void FixedUpdate()
    {
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
}
