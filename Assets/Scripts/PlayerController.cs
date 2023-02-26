using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    Vector2 moveInput;
    Vector3 moveDir;
    Vector2 mouseRotationInput, playerRotation;

    public GameObject camera;
    public Vector2 cameraAngleConstraints;

    public Rigidbody playerRb;
    public float moveSpeed;

    [SerializeField]
    bool moveLock;

    #endregion

    #region Interaction Variables

    public LayerMask interactableLayers;
    public float interactionRange;

    Ray targetRay;
    RaycastHit targetRayHit;
    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;



    }

    void Update()
    {
        mouseRotationInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

        if(Input.GetButtonDown("Interact"))
        {
            Interact();
        }

        Debug.DrawRay(camera.transform.position, camera.transform.forward * interactionRange, Color.red);

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

    void Interact()
    {
        targetRay = new Ray(camera.transform.position, camera.transform.forward);
        targetRayHit = new RaycastHit();

        if (Physics.Raycast(targetRay, out targetRayHit, interactionRange, interactableLayers))
        {
            var interactableObject = targetRayHit.transform.parent.gameObject.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                interactableObject.Interact();
            }
            else
                Debug.Log("This object cannot be interacted with");
            
        }
    }
}
