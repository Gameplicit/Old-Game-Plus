using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInteraction : MonoBehaviour
{
    Ray mousePointerRay;
    RaycastHit MousePointerRayHit;

    [SerializeField] GameObject helper;
    private Vector3 defaultHelperSize;
    private static Vector3 pointerOutOfBoundsPosition = Vector3.zero;

    [SerializeField] Hand hand;

    private void Start()
    {
        defaultHelperSize = helper.transform.localScale;

#if !UNITY_EDITOR
        defaultHelperSize = Vector3.zero;
#endif
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (hand.HeldObject != null)
            {
                Debug.Log($"Putting back {hand.HeldObject.gameObject.name}");
                hand.PutHeldObjectBack();
                return;
            }
        }

        mousePointerRay = Camera.main.ScreenPointToRay(
            new Vector3(Mouse.current.position.x.magnitude, Mouse.current.position.y.magnitude, 0f), Camera.main.stereoActiveEye);

        if (Physics.Raycast(mousePointerRay, out MousePointerRayHit))
        {
            helper.transform.position = MousePointerRayHit.point;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (hand.HeldObject == null)
                {
                    //print($"Player clicked on " + MousePointerRayHit.collider.name + ".");
                    helper.transform.localScale = defaultHelperSize * 1.5f;

                    PickupObject pickup = MousePointerRayHit.transform.GetComponent<PickupObject>();

                    InteractiveObject interactiveObject = MousePointerRayHit.collider.GetComponent<InteractiveObject>();

                    if (pickup == null)
                    {
                        pickup = MousePointerRayHit.transform.GetComponentInParent<PickupObject>();
                    }

                    if (pickup != null && !pickup.WasUsed)
                    {
                        hand.PickupObject(pickup);
                    }
                    
                    if (interactiveObject != null)
                    {
                        //Debug.Log($"Interactive object found: {interactiveObject.gameObject.name}.");
                        interactiveObject.UseInteractiveObject.Invoke();
                    }
                    else
                    {
                        //Debug.Log($"Object {MousePointerRayHit.collider.gameObject.name} is not interactive.");
                    }
                }
                else
                {
                    GameObject useTargetObject = MousePointerRayHit.transform.gameObject;

                    if (hand.HeldObject.ObjectsUsableOn.Contains(useTargetObject))
                    {
                        PickupObject objectInUse = hand.HeldObject;

                        hand.PutHeldObjectBack();

                        foreach (GameObject gameObject in objectInUse.ObjectsToActivateWhilePickedUp)
                        {
                            gameObject.SetActive(false);
                        }

                        foreach (GameObject gameObject in objectInUse.ObjectsToActivateWhenUsed)
                        {
                            gameObject.SetActive(true);
                        }

                        foreach (GameObject gameObject in objectInUse.ObjectsToDectivateWhenUsed)
                        {
                            gameObject.SetActive(false);
                        }

                        objectInUse.gameObject.SetActive(false);

                        objectInUse.UsePickupObject.Invoke();
                    }

                    //print($"Player clicked on " + MousePointerRayHit.collider.name + ".");
                    helper.transform.localScale = defaultHelperSize * 1.5f;
                }
            }
            else
            {
                helper.transform.localScale = defaultHelperSize;
            }
        }
        else
        {
            helper.transform.position = pointerOutOfBoundsPosition;
            helper.transform.localScale = defaultHelperSize;
        }
    }
}