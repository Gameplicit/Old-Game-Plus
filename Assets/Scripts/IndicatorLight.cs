using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;
    [SerializeField] MeshRenderer meshRenderer;

    private void Start()
    {
        TurnOff();
    }

    public void TurnOn()
    {
        meshRenderer.sharedMaterial = onMaterial;
    }

    public void TurnOff()
    {
        meshRenderer.sharedMaterial = offMaterial;
    }
}
