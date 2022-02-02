using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool loadNextQuestion;
    public float fillFraction;

    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    bool isAnsweringQuestion;
    float timerValue;


    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {
            HandleQuestionTime();
        }
        else
        {
            HandleAnswerTime();
        }
    }

    #region TimerStateLogic
    private void HandleQuestionTime()
    {
        if (timerValue > 0)
        {
            fillFraction = timerValue / timeToCompleteQuestion;
        }
        else
        {
            isAnsweringQuestion = false;
            timerValue = timeToShowCorrectAnswer;
        }
    }

    private void HandleAnswerTime()
    {
        if (timerValue > 0)
        {
            fillFraction = timerValue / timeToShowCorrectAnswer;
        }
        else
        {
            isAnsweringQuestion = true;
            timerValue = timeToCompleteQuestion;
            loadNextQuestion = true;
        }
    }
    #endregion
}
