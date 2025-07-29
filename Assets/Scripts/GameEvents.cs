using System;
using UnityEditor;
using UnityEngine;
using static UnityEngine.CullingGroup;

public enum CharacterState { Sad, Calm };

public static class GameEvents
{
    public static event Action<CharacterState> onStateChangeRequested;
    public static event Action<CharacterState> onStateChanged;

    public static void RequestStateChange(CharacterState newState)
    {
        onStateChangeRequested?.Invoke(newState);
    }

    public static void BroadcastStateChange(CharacterState newState)
    {
        onStateChanged?.Invoke(newState);
    }
}

