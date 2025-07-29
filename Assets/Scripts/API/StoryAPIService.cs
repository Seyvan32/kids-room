using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class StoryAPIService : MonoBehaviour
{
    public static StoryAPIService Instance { get; private set; }

    private Dictionary<int, StoryData> storyDatabase;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            LoadStoriesFromJSON();
        }
    }

    private void LoadStoriesFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/stories");
        if (jsonFile == null)
        {
            Debug.LogError("Could not find stories.json in the Resources folder!");
            storyDatabase = new Dictionary<int, StoryData>();
            return;
        }

        StoryCollection storyCollection = JsonUtility.FromJson<StoryCollection>(jsonFile.text);

        storyDatabase = storyCollection.stories.ToDictionary(story => story.id, story => story);

        Debug.Log($"Successfully loaded {storyDatabase.Count} stories from JSON.");
    }

    public async Task<StoryData> GetStoryAsync(int storyId)
    {
        Debug.Log($"API: Request received for story ID: {storyId}. Simulating network delay...");

        // jsut for test
        await Task.Delay(Random.Range(500, 1500));

        if (storyDatabase.ContainsKey(storyId))
        {
            Debug.Log($"API: Story {storyId} found. Sending response.");
            return storyDatabase[storyId];
        }
        else
        {
            Debug.LogError($"API: Story ID {storyId} not found in database.");
            return null;
        }
    }

    public async Task<List<StoryData>> GetAllStoriesAsync()
    {
        Debug.Log("API: Request received for ALL stories. Simulating network delay...");

        // just testing
        await Task.Delay(Random.Range(200, 800)); 

        List<StoryData> allStories = storyDatabase.Values.ToList();
        Debug.Log($"API: Found {allStories.Count} stories. Sending response.");
        return allStories;
    }
}
