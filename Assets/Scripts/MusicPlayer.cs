using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private TV tv;
    [SerializeField] private ConsoleSetup console;

    [SerializeField] private AudioSource audioSource_L, audioSource_R;

    void Start()
    {
        ResetMusicPlay();
    }

    void Update()
    {
        bool L = tv.IsOn && console.IsAudioLAvailableToTv;
        bool R = tv.IsOn && console.IsAudioRAvailableToTv;

        audioSource_L.volume = L ? 1 : 0;
        audioSource_R.volume = R ? 1 : 0;
    }

    public void ResetMusicPlay()
    {
        audioSource_L.Stop();
        audioSource_R.Stop();
        audioSource_L.Play();
        audioSource_R.Play();
    }
}
