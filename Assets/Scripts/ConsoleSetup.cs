using System;
using UnityEngine;

public class ConsoleSetup : MonoBehaviour
{
    [SerializeField] private PickupObject gameCartridge;
    [SerializeField] private Transform cartridgeDock;

    [SerializeField] private InteractiveObject onOffSwitch;
    [SerializeField] private IndicatorLight light;
    [SerializeField] private Transform onPosition, offPosition;

    [SerializeField] private GameObject cableAudioL, cableAudioR, cableVideo;

    private ConsoleStatus status;
    public Cartridge CurrentlyRunningGame { get; private set; }
    public bool IsVideoAvailableToTv { get; private set; }
    public bool IsAudioLAvailableToTv { get; private set; }
    public bool IsAudioRAvailableToTv { get; private set; }
    
    void Start()
    {
        CurrentlyRunningGame = null;

        light.TurnOff();
        onOffSwitch.transform.position = offPosition.position;

        status.isPluggedIntoPower = true; //DEBUG
        status.isTurnedOn = false;
        status.isPluggedIntoTvVideo = false;
        status.isPluggedIntoTvAudioL = false;
        status.isPluggedIntoTvAudioR = false;
        status.cartridgeInserted = null;

        gameCartridge.PickUp.AddListener(CartridgePickedUp);
        gameCartridge.UsePickupObject.AddListener(InsertCartridge);
        onOffSwitch.UseInteractiveObject.AddListener(SwitchOnOff);
    }

    private void CartridgePickedUp()
    {
        if (status.cartridgeInserted == gameCartridge.GetComponent<Cartridge>())
        {
            status.cartridgeInserted = null;
            CurrentlyRunningGame = null;
        }
    }

    private void SwitchOnOff()
    {
        status.isTurnedOn = !status.isTurnedOn;

        if (status.isTurnedOn)
        {
            light.TurnOn();
            onOffSwitch.transform.position = onPosition.position;
        }
        else
        {
            light.TurnOff();
            onOffSwitch.transform.position = offPosition.position;
        }
    }

    void Update()
    {
        status.isPluggedIntoTvAudioL = cableAudioL.activeInHierarchy;
        status.isPluggedIntoTvAudioR = cableAudioR.activeInHierarchy;
        status.isPluggedIntoTvVideo = cableVideo.activeInHierarchy;

        if (status.isPluggedIntoPower && status.isTurnedOn)
        {
            CurrentlyRunningGame = status.cartridgeInserted;

            IsVideoAvailableToTv = CurrentlyRunningGame != null && status.isPluggedIntoTvVideo;
            IsAudioLAvailableToTv = CurrentlyRunningGame != null && status.isPluggedIntoTvAudioL;
            IsAudioRAvailableToTv = CurrentlyRunningGame != null && status.isPluggedIntoTvAudioR;
        }
        else
        {
            CurrentlyRunningGame = null;
            IsVideoAvailableToTv = false;
            IsAudioLAvailableToTv = false;
            IsAudioRAvailableToTv = false;
        }
    }

    void InsertCartridge()
    {
        status.cartridgeInserted = gameCartridge.GetComponent<Cartridge>();
        gameCartridge.transform.position = cartridgeDock.position;
        gameCartridge.transform.rotation = cartridgeDock.rotation;
        gameCartridge.gameObject.SetActive(true);
    }

    public struct ConsoleStatus
    {
        public bool isPluggedIntoPower;
        public bool isTurnedOn;
        public bool isPluggedIntoTvVideo;
        public bool isPluggedIntoTvAudioL;
        public bool isPluggedIntoTvAudioR;
        public Cartridge cartridgeInserted;
    }

    public enum AudioChannel
    {
        None = 0,
        Mono = 1,
        LeftOnly = 2,
        RightOnly = 3,
    }

    [ContextMenu("Plug In Power")]
    public void PlugInPower()
    {
        status.isPluggedIntoPower = true;
    }

    [ContextMenu("Plug Out Power")]
    public void PlugOutPower()
    {
        status.isPluggedIntoPower = false;
    }

    [ContextMenu("Turn On")]
    public void TurnOn()
    {
        status.isTurnedOn = true;
    }

    [ContextMenu("Turn Off")]
    public void TurnOff()
    {
        status.isTurnedOn = false;
    }

    [ContextMenu("Plug In Video")]
    public void PlugInVideo()
    {
        status.isPluggedIntoTvVideo = true;
    }

    [ContextMenu("Plug Out Video")]
    public void PlugOutVideo()
    {
        status.isPluggedIntoTvVideo = false;
    }

    [ContextMenu("Plug In Audio L")]
    public void PlugInAudioL()
    {
        status.isPluggedIntoTvAudioL = true;
    }

    [ContextMenu("Plug Out Audio L")]
    public void PlugOutAudioL()
    {
        status.isPluggedIntoTvAudioL = false;
    }

    [ContextMenu("Plug In Audio R")]
    public void PlugInAudioR()
    {
        status.isPluggedIntoTvAudioR = true;
    }

    [ContextMenu("Plug Out Audio R")]
    public void PlugOutAudioR()
    {
        status.isPluggedIntoTvAudioR = false;
    }
}
