using UnityEngine;

public class SpinObjectsX : MonoBehaviour
{
    public float spinSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
