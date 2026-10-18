using System;
using UnityEngine;
using UnityEngine.Events;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private GameObject[] objectsUsableOn;
    [SerializeField] private GameObject[] objectsToActivateWhenUsed;
    [SerializeField] private GameObject[] objectsToDeactivateWhenUsed;
    [SerializeField] private GameObject[] objectsToActivateWhilePickedUp;

    [SerializeField] private bool holdTheOtherWay;
    [SerializeField] private bool makeSmallerWhenHeld;

    private bool wasUsed = false;

    public GameObject[] ObjectsUsableOn {  get { return objectsUsableOn; } } 
    public GameObject[] ObjectsToActivateWhenUsed { get { return objectsToActivateWhenUsed; } }
    public GameObject[] ObjectsToDectivateWhenUsed { get { return objectsToDeactivateWhenUsed; } }
    public GameObject[] ObjectsToActivateWhilePickedUp { get { return objectsToActivateWhilePickedUp; } }

    public bool HoldTheOtherWay {  get { return holdTheOtherWay; } }

    public bool MakeSmallerWhenHeld { get { return makeSmallerWhenHeld; } }

    public bool WasUsed {  get { return wasUsed; } }

    public UnityEvent UsePickupObject;

    public UnityEvent PickUp;

    private void Awake()
    {
        if (root == null)
        {
            root = transform;
        }
    }

    private void Start()
    {
        UsePickupObject.AddListener(MarkAsUsed);
    }

    private void MarkAsUsed()
    {
        wasUsed = true;
    }
}
