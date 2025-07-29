using UnityEngine;

public interface ICharacterState
{
    void Enter(CharacterStateManager stateManager);

    void Exit();
}

