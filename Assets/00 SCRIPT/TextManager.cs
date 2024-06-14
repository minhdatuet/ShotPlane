using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : Singleton<TextManager>
{
    [SerializeField] Text scoresText;
    [SerializeField] Text winText;
    int score = 0;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            SetScoresText();
            if (score == 16)
            {
                ShowWinText();
                StopGame();
            }
        }
    }

    public void SetScoresText()
    {
        scoresText.text = "Scores: " + score.ToString();
    }

    private void ShowWinText()
    {
        winText.gameObject.SetActive(true);
    }

    private void StopGame()
    {
        // Dừng trò chơi
        Time.timeScale = 0f;
    }
}
