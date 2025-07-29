using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    private ICharacterState currentState;
    public CharacterState CurrentStateType { get; private set; }
    private Dictionary<CharacterState, ICharacterState> allStates;

    void Awake()
    {
        allStates = new Dictionary<CharacterState, ICharacterState>
        {
            { CharacterState.Calm, new CalmState() },
            { CharacterState.Sad, new SadState() }
        };
    }

    void OnEnable()
    {
        GameEvents.onStateChangeRequested += OnStateChangeRequested;
    }

    void OnDisable()
    {
        GameEvents.onStateChangeRequested -= OnStateChangeRequested;
    }

    void Start()
    {
        ChangeState(CharacterState.Calm);
    }

    private void OnStateChangeRequested(CharacterState requestedState)
    {
        ChangeState(requestedState);
    }

    public void ChangeState(CharacterState newStateKey)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = allStates[newStateKey];
        currentState.Enter(this);
        CurrentStateType = newStateKey;

        GameEvents.BroadcastStateChange(newStateKey);
    }
}
