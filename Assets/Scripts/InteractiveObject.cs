using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    public UnityEvent UseInteractiveObject;

    [SerializeField] private AudioSource audioSourceClick_L, audioSourceClick_R;
    private void Start()
    {
        UseInteractiveObject.AddListener(PlayClickSound);
    }

    public void PlayClickSound()
    {
        audioSourceClick_L.Play();
        audioSourceClick_R.Play();
    }
}
