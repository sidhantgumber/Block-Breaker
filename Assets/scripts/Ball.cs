
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters

    [SerializeField] Paddle paddle1;
    [SerializeField] float xVelocity = 2f;
    [SerializeField] float yVelocity = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f; // to prevent boring ball loops
    bool hasLaunched = false;


    // state

    Vector2 paddleToBallVector;

    // cached component references, jab inka use ek se zaada jagah ho to cache kardo, time bachaao
    AudioSource myAudioSource;

    Rigidbody2D myRigidBody2d;


    // Start is called before the first frame update

    void Start()
    {

        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hasLaunched == false)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
        
        
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {

            myRigidBody2d.velocity = new Vector2(xVelocity, yVelocity);
            hasLaunched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f,randomFactor));
        if (hasLaunched == true)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2d.velocity += velocityTweak;
        }
    }

   
}
