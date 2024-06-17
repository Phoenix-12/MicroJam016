using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _scoreCounter;

    public void UpdateScore(int score) => _scoreCounter.text = score.ToString(); 
}