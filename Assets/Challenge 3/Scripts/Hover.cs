using UnityEngine;

public class Hover : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float hoverSpeed = 2f;
    [Tooltip("Height of hovering")]
    [SerializeField] private float hoverAmplitude = 0.5f;

    private Vector3 startPosition;

    private void Start()
    {
        // store the starting position of the object
        startPosition = transform.position;
    }

    private void Update()
    {
        // calculate the new Y position for the hover effect
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;

        // update the object's position with the new Y value
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
