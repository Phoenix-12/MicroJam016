using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshPro _scoreCounter;

    private void UpdateScore(int score) => _scoreCounter.text = score.ToString(); 
}