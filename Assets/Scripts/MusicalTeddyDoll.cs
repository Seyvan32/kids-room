using System.Collections;
using UnityEngine;

public class MusicalTeddyDoll : MonoBehaviour, IInteractable
{

    private AudioSource audioSource;
    private bool isOn = false;
    private Coroutine runningFade;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip sadSound;
    [SerializeField] private AudioClip calmSound;
    [SerializeField] private float crossfadeDuration = 1.5f;
    [SerializeField] private float maxVolume = 0.75f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0; 
        audioSource.loop = true; 
    }

    private void OnEnable()
    {
        GameEvents.onStateChanged += OnCharacterStateChanged;
    }

    private void OnDisable()
    {
        GameEvents.onStateChanged -= OnCharacterStateChanged;
    }

    public void Interact(GameObject interactor)
    {
        isOn = !isOn;

        if (isOn)
        {
            if (interactor.TryGetComponent(out CharacterStateManager stateManager))
            {
                UpdateSoundClip(stateManager.CurrentStateType);
            }
        }
        else
        {
             UpdateSoundClip(CharacterState.Calm, true);
        }

    }

    private void OnCharacterStateChanged(CharacterState newState)
    {
        if (!isOn)
        {
            return;
        }

        UpdateSoundClip(newState);
    }

    private void UpdateSoundClip(CharacterState currentStateType, bool turningOff = false)
    {
        if (runningFade != null)
        {
            StopCoroutine(runningFade);
        }

        AudioClip targetClip = null;

        if (!turningOff)
        {
            switch (currentStateType)
            {
                case CharacterState.Sad:
                    targetClip = sadSound;
                    break;
                case CharacterState.Calm:
                    targetClip = calmSound;
                    break;
            }
        }

        runningFade = StartCoroutine(Crossfade(targetClip));
    }

    private IEnumerator Crossfade(AudioClip newClip)
    {
        float timer = 0f;
        float startVolume = audioSource.volume;
        float halfDuration = crossfadeDuration / 2f;

        // fade out 
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / halfDuration);
            yield return null;
        }

        audioSource.volume = 0f;

        if (newClip == null)
        {
            audioSource.Stop();
            yield break;
        }

        // fade in
        audioSource.clip = newClip;
        audioSource.Play();

        timer = 0f;
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, maxVolume, timer / halfDuration);
            yield return null;
        }

        audioSource.volume = maxVolume;
    }

}
