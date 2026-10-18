using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private Type type;

    public Type TypeOfPowerUp {  get { return type; } }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{other.name} has entered the trigger.");

        if (other.GetComponent<CharacterControls>() != null)
        {
            ActivatePowerUp();
        }
    }

    private void ActivatePowerUp()
    {
        foreach (GameObject gameObject in objectsToActivate)
        {
            gameObject.SetActive(true);
        }

        this.gameObject.SetActive(false);
    }

    public enum Type
    {
        None = -1,
        Item = 1,
        Win = 2,
    }
}
