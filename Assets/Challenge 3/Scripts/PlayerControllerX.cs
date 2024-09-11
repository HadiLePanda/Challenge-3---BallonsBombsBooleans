using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioSource windAudio;
    [SerializeField] private MeshRenderer playerMesh;

    [Header("Particles")]
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem fireworksParticle;
    [SerializeField] private ParticleSystem windParticle;

    [Header("Sounds")]
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip explodeSound;
    [SerializeField] private AudioClip bounceSound;

    [Header("Settings")]
    [SerializeField] private float startImpulseForce = 5f;
    [SerializeField] private float floatForce = 6f;
    [SerializeField] private float bounceForce = 7f;
    [SerializeField] private Vector3 defaultGravity = new(0f, -9.81f, 0);
    [SerializeField] private float gravityModifier = 1.5f;
    [SerializeField] private float maxFloatHeight = 15f;

    private bool isLowEnough;
    private bool isPushedUp;

    private void Start()
    {
        // setup gravity
        Physics.gravity = defaultGravity * gravityModifier;

        // apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * startImpulseForce, ForceMode.Impulse);
    }

    private void Update()
    {
        // check if low enough to float
        isLowEnough = transform.position.y < maxFloatHeight;

        // handle effects when pushing the balloon up
        if (isPushedUp)
        {
            // play continuous wind sound when pushed up
            if (!windAudio.isPlaying)
                windAudio.Play();

            // play wind particle
            if (!windParticle.isPlaying)
                windParticle.Play();
        }
        else
        {
            // stop wind sound when not pushed up
            if (windAudio.isPlaying)
                windAudio.Stop();

            // stop wind particle
            if (windParticle.isPlaying)
                windParticle.Stop();
        }
    }

    private void FixedUpdate()
    {
        // while space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) &&
            isLowEnough &&
            !GameManager.singleton.IsGameOver)
        {
            // push the balloon up
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);

            isPushedUp = true;
        }
        else
        {
            isPushedUp = false;
        }
    }

    private void Die()
    {
        // disable mesh
        playerMesh.enabled = false;

        // freeze
        playerRb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        // player collides with ground
        if (other.gameObject.CompareTag("Ground"))
        {
            // make the player bounce
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

            // play bounce sound
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // player collides with bomb
        if (other.gameObject.CompareTag("Bomb"))
        {
            HitBomb(other);
        }
        // player collides with money
        else if (other.gameObject.CompareTag("Money"))
        {
            HitCoin(other);
        }
    }

    private void HitCoin(Collider coinCollider)
    {
        // play fireworks
        fireworksParticle.Play();

        // play money collect sound
        playerAudio.PlayOneShot(moneySound, 1.0f);

        // destroy money
        Destroy(coinCollider.gameObject);

        // add score
        GameManager.singleton.AddScore(1);
    }

    private void HitBomb(Collider bombCollider)
    {
        // play explosion effects
        explosionParticle.Play();
        playerAudio.PlayOneShot(explodeSound, 1.0f);

        // destroy bomb
        Destroy(bombCollider.gameObject);

        // player death logic
        Die();

        // trigger game over
        GameManager.singleton.GameOver();
    }
}
