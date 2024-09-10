using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private AudioSource playerAudio;

    [Header("Particles")]
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    [Header("Sounds")]
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    [Header("Settings")]
    public float floatForce = 20f;
    public float bounceForce = 7f;
    public Vector3 defaultGravity = new(0f, -9.81f, 0);
    public float gravityModifier = 1.5f;
    public float maxFloatHeight = 15f;

    private bool isLowEnough;

    private void Start()
    {
        // setup gravity
        Physics.gravity = defaultGravity * gravityModifier;

        // apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    private void Update()
    {
        // check if low enough to float
        isLowEnough = transform.position.y < maxFloatHeight;

        // while space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) &&
            isLowEnough &&
            !GameManager.singleton.IsGameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // player collides with bomb
        if (other.gameObject.CompareTag("Bomb"))
        {
            // play explosion effects
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);

            // trigger game over
            GameManager.singleton.GameOver();

            // destroy bomb
            Destroy(other.gameObject);
        }
        // player collides with money
        else if (other.gameObject.CompareTag("Money"))
        {
            // play fireworks
            fireworksParticle.Play();

            // play money collect sound
            playerAudio.PlayOneShot(moneySound, 1.0f);

            // destroy money
            Destroy(other.gameObject);
        }
        // player collides with ground
        else if (other.gameObject.CompareTag("Ground"))
        {
            // make the player bounce
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

            // play bounce sound
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }
    }
}
