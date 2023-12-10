using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public static bool levelStarted;
    public static bool gameOver;
    public GameObject startMenuLevel;
    public GameObject gameOverMenuLevel;

    public static int gems;
    public TextMeshProUGUI gemsText;

    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        Time.timeScale = 1;
        levelStarted = false;
        gameOver = false;
        gems = 0;
        score = 0;
        //PlayerPrefs.DeleteAll();
    }

    
    void Update()
    {
        gemsText.text = (PlayerPrefs.GetInt("TotalGems", 0) + gems).ToString();
        scoreText.text = "" + score.ToString();
        Touchscreen ts = Touchscreen.current;
        if( ts != null && ts.primaryTouch.press.isPressed && !levelStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            levelStarted=true;
            startMenuLevel.SetActive(false);
        }
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverMenuLevel.SetActive(true);
            PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems", 0) + gems);
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                highScoreText.text = "New HighScore: " + score;
                PlayerPrefs.SetInt("HighScore", score);
            }
            else
            {
                highScoreText.gameObject.SetActive(false);
            }
            this.enabled = false;
        }
    }
}
