using UnityEngine;

public class CalmState : ICharacterState
{
    private PlayerController player;

    public void Enter(CharacterStateManager stateManager)
    {
        player = stateManager.GetComponent<PlayerController>();
        player.SetMoveSpeed(5f); 
    }

    public void Exit()
    {
        Debug.Log("Exiting Calm State.");
    }
}
