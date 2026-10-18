using UnityEngine;

public class TVInputSwitcher : MonoBehaviour
{
    [SerializeField] private TV tv;
    [SerializeField] private ConsoleSetup console;

    [SerializeField] Material offScreenMaterial, tvStaticMaterial, gameScreenMaterial;
    [SerializeField] MeshRenderer screenRenderer;

    [SerializeField] private AudioSource whiteNoise;

    void Start()
    {
        screenRenderer.sharedMaterial = offScreenMaterial;
    }

    private void Update()
    {
        if (tv.IsOn)
        {
            if (console.IsVideoAvailableToTv)
            {
                screenRenderer.sharedMaterial = gameScreenMaterial;
                if (whiteNoise.isPlaying)
                {
                    whiteNoise.Stop();
                }
            }
            else
            {
                screenRenderer.sharedMaterial = tvStaticMaterial;
                screenRenderer.sharedMaterial.mainTextureOffset = new Vector2(Random.Range(-100, 101), Random.Range(-100, 101));
                if (!whiteNoise.isPlaying)
                {
                    whiteNoise.Play();
                }
            }
        }
        else
        {
            screenRenderer.sharedMaterial = offScreenMaterial;
            if (whiteNoise.isPlaying)
            {
                whiteNoise.Stop();
            }
        }
    }

    [ContextMenu("Turn TV Off")]
    public void TurnTvOff()
    {
        screenRenderer.sharedMaterial = offScreenMaterial;
    }

    [ContextMenu("Switch to Static")]
    public void SwitchToStatic()
    {
        screenRenderer.sharedMaterial = tvStaticMaterial;
    }

    [ContextMenu("Switch to Game 1")]
    public void SwitchToGame1()
    {
        screenRenderer.sharedMaterial = gameScreenMaterial;
    }
}
