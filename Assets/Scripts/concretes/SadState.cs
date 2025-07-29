using UnityEngine;

public class SadState : ICharacterState
{
    private PlayerController player;

    public void Enter(CharacterStateManager stateManager)
    {
        player = stateManager.GetComponent<PlayerController>();
        player.SetMoveSpeed(2f); 
    }

    public void Exit()
    {
        Debug.Log("Exiting Sad State.");
    }

}
