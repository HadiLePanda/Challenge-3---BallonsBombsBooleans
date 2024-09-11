using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider boxCollider;

    private Vector3 startPos;
    private float repeatWidth;

    private void Start()
    {
        // establish the default starting position 
        startPos = transform.position;
        // set repeat width to half of the background
        repeatWidth = boxCollider.size.x / 2;
    }

    private void Update()
    {
        // if background moves left by its repeat width, move it back to start position
        if (transform.position.x <= startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
