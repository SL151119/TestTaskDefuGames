using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshPro playerLevelText;
    [SerializeField] private TextMeshProUGUI maxScoreText;

    public void AddLevelValue(int _playerLevel)
    {
        playerLevelText.text = "lv." + _playerLevel.ToString();
    }

    public void UpdateMaxScore(int _maxScore)
    {
        maxScoreText.text = "Max Score: " + _maxScore.ToString();
    }
}
