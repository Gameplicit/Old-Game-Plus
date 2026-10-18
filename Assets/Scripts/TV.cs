using UnityEngine;

public class TV : MonoBehaviour
{
    [SerializeField] private InteractiveObject onOffSwitch;
    [SerializeField] private IndicatorLight light;
    [SerializeField] private Transform onPosition, offPosition;

    private bool isOn;

    public bool IsOn { get { return isOn; } }

    void Start()
    {
        isOn = false;

        light.TurnOff();
        onOffSwitch.transform.rotation = offPosition.rotation;

        onOffSwitch.UseInteractiveObject.AddListener(SwitchOnOff);
    }

    private void SwitchOnOff()
    {
        isOn = !isOn;

        if (isOn)
        {
            light.TurnOn();
            onOffSwitch.transform.rotation = onPosition.rotation;
        }
        else
        {
            light.TurnOff();
            onOffSwitch.transform.rotation = offPosition.rotation;
        }
    }
}
