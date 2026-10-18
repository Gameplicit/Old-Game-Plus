using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform inHandPosition;
    private PickupObject heldObject;
    private Vector3 heldObjectPositionBeforeHoldingIt = Vector3.zero;
    private Quaternion heldObjectRotationBeforeHoldingIt = Quaternion.identity;

    [SerializeField] private TV tv;
    [SerializeField] private ConsoleSetup console;

    [SerializeField]
    private AudioSource
        audioSourcePickup_L,
        audioSourcePickup_R;

    private void Start()
    {
        heldObject = null;
    }

    void Update()
    {
        audioSourcePickup_L.volume = 1f;
        audioSourcePickup_R.volume = 1f;
    }

    public void PlayPickupSound()
    {
        audioSourcePickup_L.Play();
        audioSourcePickup_R.Play();
    }

    public PickupObject HeldObject { get { return heldObject; } }

    public void PickupObject(PickupObject objectToHold)
    {
        heldObject = objectToHold;

        foreach (GameObject gameObject in heldObject.ObjectsToActivateWhilePickedUp)
        {
            gameObject.SetActive(true);
        }

        heldObjectPositionBeforeHoldingIt = heldObject.transform.position;
        heldObjectRotationBeforeHoldingIt = heldObject.transform.rotation;

        heldObject.transform.position = inHandPosition.position;
        heldObject.transform.rotation = inHandPosition.rotation;

        if (heldObject.HoldTheOtherWay)
        {
            heldObject.transform.Rotate(Vector3.forward, 180f);
        }

        if (heldObject.MakeSmallerWhenHeld)
        {
            var rectTransform = heldObject.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                rectTransform.localScale /= 3f;
            }
            else
            {
                heldObject.transform.localScale /= 3f;
            }
        }

        heldObject.PickUp.Invoke();

        PlayPickupSound();
    }

    public void PutHeldObjectBack()
    {
        if (heldObject != null)
        {
            foreach (GameObject gameObject in heldObject.ObjectsToActivateWhilePickedUp)
            {
                gameObject.SetActive(false);
            }

            heldObject.transform.position = heldObjectPositionBeforeHoldingIt;
            heldObject.transform.rotation = heldObjectRotationBeforeHoldingIt;

            if (heldObject.MakeSmallerWhenHeld)
            {
                var rectTransform = heldObject.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    rectTransform.localScale *= 3f;
                }
                else
                {
                    heldObject.transform.localScale *= 3f;
                }
            }

            heldObject = null;

            PlayPickupSound();
        }
    }
}
