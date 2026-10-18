using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float movementSpeed = 1.5f;
    private GridPoint[] gridPoints;
    private float[] distances;

    private GridPoint gridPosition = null;

    private float lastTimeMoveModeChanged;
    private Direction currentDirection, lastDirection;

    private MoveMode moveMode;

    enum MoveMode
    {
        Undecided = 0,
        CenterOnCurrentGridPosition = 1,
        NeighborGridPosition = 2,
    }

    enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }

    private void Start()
    {
        lastDirection = Direction.Down;
        currentDirection = Direction.Left;

        moveMode = MoveMode.Undecided;
        lastTimeMoveModeChanged = Time.time - 4.0f;

        gridPoints = FindObjectsByType<GridPoint>();
        distances = new float[gridPoints.Length];

        gridPosition = GetNewGridPosition();
    }

    void Update()
    {
        gridPosition = GetNewGridPosition();

        var now = Time.time;

        if (now - lastTimeMoveModeChanged > 3.0f)
        {
            switch (moveMode)
            {
                case MoveMode.NeighborGridPosition:
                    moveMode = MoveMode.CenterOnCurrentGridPosition;
                    break;
                default:
                    moveMode = MoveMode.NeighborGridPosition;
                    PickNewDirection();
                    break;
            }
            lastTimeMoveModeChanged = now;
        }

        if (moveMode == MoveMode.NeighborGridPosition)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    rb.AddForce(Vector3.up * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    break;
                case Direction.Left:
                    rb.AddForce(Vector3.left * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    break;
                case Direction.Right:
                    rb.AddForce(Vector3.right * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    break;
                case Direction.Down:
                    rb.AddForce(Vector3.down * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    break;
            }
        }
        else
        {
            rb.AddForce((gridPosition.transform.position - this.transform.position) * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void PickNewDirection()
    {
        List<Direction> possibleDirections = new List<Direction>();

        if (gridPosition.CanGoUp)
        {
            possibleDirections.Add(Direction.Up);
        }
        if (gridPosition.CanGoLeft)
        {
            possibleDirections.Add(Direction.Left);
        }
        if (gridPosition.CanGoRight)
        {
            possibleDirections.Add(Direction.Right);
        }
        if (gridPosition.CanGoDown)
        {
            possibleDirections.Add(Direction.Down);
        }

        lastDirection = currentDirection;

        int directionIndex = Random.Range(0, possibleDirections.Count);

        if (directionIndex < 0 || directionIndex >= possibleDirections.Count)
        {
            Debug.Log("Fallback enemy behaviour.");
            moveMode = MoveMode.CenterOnCurrentGridPosition;
        }
        else
        {
            currentDirection = possibleDirections[directionIndex];
        }
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
}
