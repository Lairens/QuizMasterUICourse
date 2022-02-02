using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO questionSO;
    [SerializeField] GameObject[] answerButtons;
    
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        questionText.text = questionSO.GetQuestion;

        for(int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = questionSO.GetAnswer(i);
        }
    }

    public void OnAswerSelected(int index)
    {
        if(index == questionSO.GetCorrectAnswerIndex)
        {
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            int correctAnswerIndex = questionSO.GetCorrectAnswerIndex;
            questionText.text = "Sorry the correct answer was: \n" + questionSO.GetAnswer(correctAnswerIndex);
            Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctAnswerButtonImage.sprite = correctAnswerSprite;
        }
    }
}
