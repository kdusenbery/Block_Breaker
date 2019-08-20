using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0.1f, 5f)] [SerializeField] float gameSpeed;
    [SerializeField] int pointsPerBlock;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] bool autoPlayEnabled;

    // state variables
    [SerializeField] int playerScore;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;

        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreTxt.text = playerScore.ToString();
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddPoints()
    {
        playerScore += pointsPerBlock;
        scoreTxt.text = playerScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool AutoPlayEnabled()
    {
        return autoPlayEnabled;
    }
}
