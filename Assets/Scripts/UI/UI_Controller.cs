using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public void SetCalmState()
    {
        GameEvents.RequestStateChange(CharacterState.Calm);
    }

    public void SetSadState()
    {
        GameEvents.RequestStateChange(CharacterState.Sad);
    }
}
