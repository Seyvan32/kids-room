using TMPro;
using UnityEngine;

public class InteractionUIManager : MonoBehaviour
{
    public static InteractionUIManager Instance { get; private set;}

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private RectTransform promptTextTransform;
    [SerializeField] private Vector3 offset = new Vector3(0, 100, 0);

    private Transform currentTarget;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(currentTarget.position);
            promptTextTransform.position = screenPoint + offset;
        }
    }

    public void ShowPrompt(Transform target, string message)
    {
        currentTarget = target;
        promptText.text = message;
        promptText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        currentTarget = null;
        promptText.gameObject.SetActive(false);
    }
}
