using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "0";
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = $"{newScore}";
    }
}