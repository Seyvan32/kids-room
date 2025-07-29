using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryUIManager : MonoBehaviour
{
    public static StoryUIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject listViewPanel;
    [SerializeField] private GameObject detailViewPanel;

    [Header("List View Components")]
    [SerializeField] private GameObject storyListItemPrefab;
    [SerializeField] private Transform listContentParent;

    [SerializeField] private Button listCloseButton;

    [Header("Detail View Components")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI storyBodyText;
    [SerializeField] private Button detailBackButton;

    private List<GameObject> spawnedListItems = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); }
        else { Instance = this; }

        listCloseButton.onClick.AddListener(HideAllPanels);
        detailBackButton.onClick.AddListener(ShowListView); 
    }

    public void ShowStoryList(List<StoryData> stories)
    {
        foreach (var item in spawnedListItems)
        {
            Destroy(item);
        }
        spawnedListItems.Clear();

        foreach (var story in stories)
        {
            GameObject newListItem = Instantiate(storyListItemPrefab, listContentParent);

            var textComponent = newListItem.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = story.title;
            }

            var buttonComponent = newListItem.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnStorySelected(story));
            }

            spawnedListItems.Add(newListItem);
        }

        detailViewPanel.SetActive(false);
        listViewPanel.SetActive(true);
    }

    private void OnStorySelected(StoryData data)
    {
        titleText.text = data.title;
        storyBodyText.text = data.storyText;

        listViewPanel.SetActive(false);
        detailViewPanel.SetActive(true);
        detailBackButton.gameObject.SetActive(true);
    }

    private void ShowListView()
    {
        detailViewPanel.SetActive(false);
        listViewPanel.SetActive(true);
    }

    public void HideAllPanels()
    {
        detailViewPanel.SetActive(false);
        listViewPanel.SetActive(false);
    }
    
    public void ShowTemporaryMessage(string message)
    {
        titleText.text = "System";
        storyBodyText.text = message;

        listViewPanel.SetActive(false);
        detailViewPanel.SetActive(true);
        detailBackButton.gameObject.SetActive(false);
    }
}
