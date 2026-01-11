using NSBLib.EventChannelSystem;
using NSBLib.Helpers;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    [SerializeField] IntEventChannel updateScore;
    [SerializeField] FloatEventChannel addTimer;
    
    public void AddScore(int amount)
    {
        score += amount;
        updateScore?.Invoke(score);
        if (amount >0)
            addTimer?.Invoke(2);
    }

    public void Evaluate(BigEnvelope envelope)
    {
        var envelopeAddress = envelope.GetAddress();
        var stamps = envelope.GetStamps();

        int ctr = 0;
        foreach (var stamp in stamps)
        {
            if (envelopeAddress.State == stamp.GetState())
            {
                ctr++;
            }
        }

        AddScore(ctr);
    }
}