using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    QuizHandler quizHandler;
    EndScreen endScreen;

    void Start()
    {
        quizHandler = FindObjectOfType<QuizHandler>();
        endScreen = FindObjectOfType<EndScreen>();

        quizHandler.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if(quizHandler.isComplete)
        {
            quizHandler.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
