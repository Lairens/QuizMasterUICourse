using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class QuizHandler : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestionSO;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer Sprite")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete = false;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        DisplayTimerFillAmount();
    }

    #region Question

    void DisplayQuestion()
    {
        questionText.text = currentQuestionSO.GetQuestion;

        for(int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestionSO.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            GetRandomQuestion();
            DisplayQuestion();
            SetButtonState(true);
            SetDefaultButtonSprites();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        else
        {
            //Print the End Screen
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestionSO = questions[index];

        if(questions.Contains(currentQuestionSO))
        {
            questions.Remove(currentQuestionSO);
        }
    }

    #endregion

    #region Answer

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;

        DisplayAnswer(index);

        SetButtonState(false);
        timer.CancelTimer();

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if(progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    private void DisplayAnswer(int index)
    {
        if (index == currentQuestionSO.GetCorrectAnswerIndex)
        {
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            int correctAnswerIndex = currentQuestionSO.GetCorrectAnswerIndex;
            questionText.text = "Sorry the correct answer was: \n" + currentQuestionSO.GetAnswer(correctAnswerIndex);
            Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctAnswerButtonImage.sprite = correctAnswerSprite;
        }
    }
    
    #endregion

    #region Button

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    #endregion

    #region Timer

    private void DisplayTimerFillAmount()
    {
        timerImage.fillAmount = timer.fillFraction;
        HandleEndOfTimer();
    }

    private void HandleEndOfTimer()
    {
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.IsAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    #endregion
}
