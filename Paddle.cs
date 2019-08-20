using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // config params
    [SerializeField] float screenWidth;
    [SerializeField] float paddleMin;
    [SerializeField] float paddleMax;
    
    // cached references
    GameSession gameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.HasStarted() || !gameSession.AutoPlayEnabled())
        {
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            paddlePos.x = Mathf.Clamp(GetXPos(), paddleMin, paddleMax);
            transform.position = paddlePos;
        }        
    }

    private float GetXPos()
    {
        if (gameSession.AutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return (Input.mousePosition.x / Screen.width * screenWidth);
        }
    }
}
