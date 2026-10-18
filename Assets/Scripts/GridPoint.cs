using UnityEngine;

public class GridPoint : MonoBehaviour
{
    [SerializeField] private bool canGoUp = true, canGoLeft = true, canGoRight = true, canGoDown = true;

    public bool CanGoUp { get { return canGoUp; } }
    public bool CanGoLeft { get { return canGoLeft; } }
    public bool CanGoRight { get { return canGoRight; } }
    public bool CanGoDown { get { return canGoDown; } }
}
