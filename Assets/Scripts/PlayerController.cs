using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private float currentMoveSpeed;

    private CharacterController characterController;
    private PlayerControls playerControls;
    private Vector2 moveInput;

    private IInteractable currentInteractable;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Player.Enable();
        playerControls.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
        playerControls.Player.Interact.performed -= OnInteract;
    }

    
    void Update()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(-moveInput.x, 0, -moveInput.y);

        characterController.Move(move * currentMoveSpeed * Time.deltaTime);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this.gameObject);
        }
    }

    public void SetMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
            InteractionUIManager.Instance.ShowPrompt(other.transform, "Press [E] to Interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
            InteractionUIManager.Instance.HidePrompt();
        }
    }
}
