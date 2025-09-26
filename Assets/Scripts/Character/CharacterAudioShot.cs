using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class CharacterAudioShot : MonoBehaviour
{
    private const float SpreadPitch = 0.3f;

    [SerializeField] private AudioClip[] _clip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (_clip.Length == 0)
            return;

        _audioSource.pitch = Random.Range(-SpreadPitch, SpreadPitch) + 1;

        int randomIndex = Random.Range(0, _clip.Length);
        _audioSource.PlayOneShot(_clip[randomIndex]);
    }
}
