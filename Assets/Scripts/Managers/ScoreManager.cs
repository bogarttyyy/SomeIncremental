using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    
    public void AddScore(int amount)
    {
        score += amount;
    }
}