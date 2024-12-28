using System;
using Scripts.Data;
using Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private Text leftScoreText;
    [SerializeField] private Text rightScoreText;

    private void OnEnable()
    {
        Events.OnLeftScoreChanged += LeftScoreChanged;
        Events.OnRightScoreChanged += RightScoreChanged;
    }
    private void OnDisable()
    {
        Events.OnLeftScoreChanged -= LeftScoreChanged;
        Events.OnRightScoreChanged -= RightScoreChanged;
    }

    private void LeftScoreChanged(int leftScore)
    {
        Data.LeftScore += leftScore;
        leftScoreText.text = Data.LeftScore.ToString();
    }
    private void RightScoreChanged(int rightScore)
    {
        Data.RightScore += rightScore;
        rightScoreText.text = Data.RightScore.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Data.LeftScore = 0;
            Data.RightScore = 0;
            Debug.Log("berra");
        }
    }
}
