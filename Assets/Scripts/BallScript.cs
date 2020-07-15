using System;
using UnityEngine;

[RequireComponent(typeof(DotPool))]
public class BallScript : MonoBehaviour
{
    #region events
    public event EventHandler<bool> Collision;
    #endregion

    #region private variables
    private DotPool dotPool;

    private bool isInteractable = true;
    private Vector3 startingPosition;
    private float frequency = 1.8f;
    private float magnitude = 0.5f;
    private float directionVectorMovementSpeed = 1f;
    private bool isMoving = false;
    private float time = 0.0f;

    private const float speed = 3.0f;
    private const float minFrequency = 0.6f;
    private const float maxFrequency = 1.8f;
    private const float minMagnitude = 0.5f;
    private const float maxMagnitude = 5.0f;
    private const string flagpoleLayerName = "Flagpole";
    #endregion

    #region Unity methods
    void Start()
    {
        dotPool = GetComponent<DotPool>();

        startingPosition = transform.localPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            time += Time.deltaTime;

            transform.position = startingPosition + (transform.right * time * speed) + (transform.up * Mathf.Sin(time * frequency) * magnitude);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResetPosition();

        OnCollision(collision.gameObject.layer == LayerMask.NameToLayer(flagpoleLayerName));
    }
    #endregion

    #region public methods
    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;
    }

    public void Kick()
    {
        if (isInteractable && !isMoving && frequency < maxFrequency && magnitude > minMagnitude)
        {
            dotPool.ReturnAllObjectsToPool();
            time = 0.0f;
            isMoving = true;
        }
    }

    public void IncrementKickingStrength()
    {
        if (isInteractable && !isMoving)
        {
            time += Time.deltaTime;

            dotPool.ReturnAllObjectsToPool();

            frequency = Mathf.Lerp(maxFrequency, minFrequency, time * directionVectorMovementSpeed);
            magnitude = Mathf.Lerp(minMagnitude, maxMagnitude, time * directionVectorMovementSpeed);

            DrawBallPath();

            if (frequency <= minFrequency && magnitude >= maxMagnitude)
            {
                Kick();
            }
        }
    }

    public void ResetSettings()
    {
        directionVectorMovementSpeed = 1.0f;
        frequency = maxFrequency;
        magnitude = minMagnitude;
    }

    public void IncrementDirectionVectorMovementSpeed()
    {
        directionVectorMovementSpeed += 0.1f;
    }
    #endregion

    #region private methods
    private void ResetPosition()
    {
        time = 0.0f;
        isMoving = false;
        transform.localPosition = startingPosition;
    }

    private void DrawBallPath()
    {
        for (float i = 0.0f; i < 10f; i += 0.2f)
        {
            var pooledObject = dotPool.GetPooledObject();

            if (pooledObject != null)
            {
                var position = (transform.right * i * speed) + (transform.up * Mathf.Sin(i * frequency) * magnitude);

                if (position.y >= 0)
                {
                    pooledObject.transform.parent = transform;
                    pooledObject.transform.localPosition = position;
                    pooledObject.SetActive(true);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    private void OnCollision(bool isHoleCollision)
    {
        Collision?.Invoke(this, isHoleCollision);
    }
    #endregion
}
