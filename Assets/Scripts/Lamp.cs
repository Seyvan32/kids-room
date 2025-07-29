using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    [Header("Light Settings")]
    [SerializeField] private Light light;
    [SerializeField] private Color calmColor = Color.white;
    [SerializeField] private Color sadColor = Color.blue;

    private bool isLit = false;

    private void Awake()
    {
        light.enabled = false;
    }

    private void OnEnable()
    {
        GameEvents.onStateChanged += OnCharacterStateChanged;
    }

    private void OnDisable()
    {
        GameEvents.onStateChanged -= OnCharacterStateChanged;
    }

    public void Interact(GameObject interactor)
    {
        isLit = !isLit;

        if (!isLit) { light.enabled = false; return; }

        light.enabled = true;

        if (interactor.TryGetComponent(out CharacterStateManager stateManager))
        {
            UpdateLightColor(stateManager.CurrentStateType);
        }
        else
        {
            UpdateLightColor(CharacterState.Calm);
        }
    }

    private void OnCharacterStateChanged(CharacterState newState)
    {
        if (!isLit)
        {
            return;
        }

        UpdateLightColor(newState);
    }

    private void UpdateLightColor(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Calm:
                light.color = calmColor;
                break;
            case CharacterState.Sad:
                light.color = sadColor;
                break;
        }
    }
}