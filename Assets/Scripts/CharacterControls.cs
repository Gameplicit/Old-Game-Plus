using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControls : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 speedChangeInCurrentFrame = Vector3.zero;
    private float movementSpeed = 4f;
    private GridPoint[] gridPoints;
    private float[] distances;

    private GridPoint gridPosition = null;

    private Vector3 startPoint;

    [SerializeField] private TV tv;
    [SerializeField] private ConsoleSetup console;

    [SerializeField] private AudioSource
        audioSourceDamage_L,
        audioSourceDamage_R,
        audioSourceWin_L,
        audioSourceWin_R,
        audioSourceItemGet_L,
        audioSourceItemGet_R;

    private void Awake()
    {
        startPoint = transform.position;
    }

    private void Start()
    {
        gridPoints = FindObjectsByType<GridPoint>();
        distances = new float[gridPoints.Length];

        gridPosition = GetNewGridPosition();
    }

    void Update()
    {
        gridPosition = GetNewGridPosition();

        speedChangeInCurrentFrame = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            if (gridPosition == null || gridPosition.CanGoUp || transform.position.y < gridPosition.transform.position.y)
            {
                speedChangeInCurrentFrame += Vector3.up;
            }
        }

        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            if (gridPosition == null || gridPosition.CanGoDown || transform.position.y > gridPosition.transform.position.y)
            {
                speedChangeInCurrentFrame += Vector3.down;
            }
        }

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            if (gridPosition == null || gridPosition.CanGoLeft || transform.position.x < gridPosition.transform.position.x)
            {
                speedChangeInCurrentFrame += Vector3.left;
            }
        }

        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            if (gridPosition == null || gridPosition.CanGoRight || transform.position.x > gridPosition.transform.position.x)
            {
                speedChangeInCurrentFrame += Vector3.right;
            }
        }

        rb.AddForce(speedChangeInCurrentFrame * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);

        UpdateSFXVolumes();
    }

    private void UpdateSFXVolumes()
    {
        bool L = tv.IsOn && console.IsAudioLAvailableToTv;
        bool R = tv.IsOn && console.IsAudioRAvailableToTv;

        audioSourceDamage_L.volume = L ? 1 : 0;
        audioSourceDamage_R.volume = R ? 1 : 0;
        audioSourceWin_L.volume = L ? 1 : 0;
        audioSourceWin_R.volume = R ? 1 : 0;
        audioSourceItemGet_L.volume = L ? 1 : 0;
        audioSourceItemGet_R.volume = R ? 1 : 0;
    }

    private GridPoint GetNewGridPosition()
    {
        float closestDistance = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < gridPoints.Length; i++)
        {
            distances[i] = Vector3.Magnitude(gridPoints[i].transform.position - this.transform.position);
            if (distances[i] < closestDistance)
            {
                closestDistance = distances[i];
                closestIndex = i;
            }
        }

        return gridPoints[closestIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerUp powerUp = other.GetComponent<PowerUp>();

        if (powerUp != null)
        {
            switch (powerUp.TypeOfPowerUp)
            {
                case PowerUp.Type.Item:
                    PlayItemGetSound();
                    break;
                case PowerUp.Type.Win:
                    PlayWinSound();
                    break;
                default:
                    break;
            }
        }
        else if (other.GetComponent<Enemy>() != null)
        {
            PlayDamageSound();
            transform.position = startPoint;
        }
    }

    private void PlayDamageSound()
    {
        audioSourceDamage_L.Play();
        audioSourceDamage_R.Play();
    }

    private void PlayWinSound()
    {
        audioSourceWin_L.Play();
        audioSourceWin_R.Play();
    }

    private void PlayItemGetSound()
    {
        audioSourceItemGet_L.Play();
        audioSourceItemGet_R.Play();
    }
}
