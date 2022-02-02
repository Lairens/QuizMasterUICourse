using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "NewQuestion")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] [Range(0, 3)] int correctAnswerIndex;

    public string GetQuestion { get { return question; } }
    public int GetCorrectAnswerIndex { get { return correctAnswerIndex; } }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
}
