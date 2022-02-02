using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO questionSO;

    void Start()
    {
        questionText.text = questionSO.GetQuestion;
    }
}
