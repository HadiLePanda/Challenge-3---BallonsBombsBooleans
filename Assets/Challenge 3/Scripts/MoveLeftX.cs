using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    [Header("Settings")]
    public float speed;
    public float leftBound = -10f;

    private void Update()
    {
        // if game is not over, move to the left
        if (!GameManager.singleton.IsGameOver)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);
        }

        // if object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound &&
            !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}
