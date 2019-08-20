using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] Paddle paddle;
    [SerializeField] float xPush;
    [SerializeField] float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor;

    // state
    Vector2 paddleToBallVector;
    public bool hasStarted = false;

    // Cached Component References
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        gameSession = GetComponent<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockToPaddle();
            LaunchOnClick();           
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor), UnityEngine.Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            if(collision.gameObject.name == "Paddle")
            {
                myAudioSource.PlayOneShot(ballSounds[0]);
            }
            else if (collision.gameObject.name.Contains("Block"))
            {
                myAudioSource.PlayOneShot(ballSounds[1]);
            }

            myRigidBody2D.velocity += velocityTweak;
        }            
    }

    public bool HasStarted()
    {
        return hasStarted;
    }
}
