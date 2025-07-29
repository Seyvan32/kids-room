using UnityEngine;

[System.Serializable]
public class StoryData
{
    public int id;
    public string title;
    public string storyText;
}

[System.Serializable]
public class StoryCollection
{
    public StoryData[] stories;
}
