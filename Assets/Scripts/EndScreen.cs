using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations! \nYou got a score of " + scoreKeeper.CalculateScore() + "%";
    }
}
