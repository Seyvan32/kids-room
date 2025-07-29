using System.Collections.Generic;
using UnityEngine;

public class StoryTerminal : MonoBehaviour, IInteractable
{
    [Header("Feedback")]
    [SerializeField] private string loadingMessage = "Connecting...";

    private bool isRequesting = false;

    public void Interact(GameObject interactor)
    {
        if (isRequesting) return;
        FetchAndDisplayStoryList();
    }

    private async void FetchAndDisplayStoryList()
    {
        isRequesting = true;
        StoryUIManager.Instance.ShowTemporaryMessage(loadingMessage);

        List<StoryData> allStories = await StoryAPIService.Instance.GetAllStoriesAsync();

        if (allStories != null && allStories.Count > 0)
        {
            StoryUIManager.Instance.ShowStoryList(allStories);
        }
        else
        {
            StoryUIManager.Instance.ShowTemporaryMessage("Could not retrieve story archives.");
        }

        isRequesting = false;
    }
}
