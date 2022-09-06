using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ColorController colorControllerScript;
    [SerializeField] private PlayerCollisions playerCollisionsScript;
    [SerializeField] private ScoreManager scoreManagerScript;
    [SerializeField] private SpawnController spawnControllerScript;

    [Header("Menus")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private GameObject plateLevel;

    public Action OnStartGame;
    public Action OnRestartGame;

    //[HideInInspector]
    public bool gameIsStarted;

    //[HideInInspector]
    public bool isLosed;

    //[HideInInspector]
    public bool isFinished;

    private void Start()
    {
        ResetPlayerPosition();
        gameIsStarted = false;
        isLosed = false;
    }

    public void StartGameOnClick()
    {
        gameIsStarted = true;
        OnStartGame?.Invoke();
    }

    public IEnumerator GameOverMenu()
    {
        isLosed = true;
        AudioManager.Instance.StopBackgroundMusic();
        AudioManager.Instance.PlayAudio(AudioManager.Instance.playerDeadSound);
        DisablePlate();
        yield return new WaitForSecondsRealtime(2);
        loseMenu.SetActive(true);
    }

    public IEnumerator WinMenu()
    {
        isFinished = true;
        AudioManager.Instance.StopBackgroundMusic();
        AudioManager.Instance.PlayAudio(AudioManager.Instance.winSound);
        yield return new WaitForSecondsRealtime(4);
        winMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        AudioManager.Instance.PlayBackgroundMusic();
        gameIsStarted = true;
        isLosed = false;
        isFinished = false;
        OnRestartGame?.Invoke();
        plateLevel.SetActive(true);
        ResetPlayerScale();
        colorControllerScript.SetRandomColor();
        ResetPlayerLevel();
        ResetMaxScore();
        ResetPlayerPosition();
        spawnControllerScript.Spawn();
    }

    public void DisablePlate()
    {
        plateLevel.SetActive(false);
    }

    private void ResetPlayerScale()
    {
        player.transform.localScale = playerCollisionsScript.startPlayerScale;
        playerCollisionsScript.currentPlayerScale = player.transform.localScale;
    }

    private void ResetPlayerLevel()
    {
        playerCollisionsScript.playerLevel = 0;
        scoreManagerScript.AddLevelValue(playerCollisionsScript.playerLevel);
    }

    private void ResetMaxScore()
    {
        playerCollisionsScript._maxScore = 0;
        scoreManagerScript.UpdateMaxScore(playerCollisionsScript._maxScore);
    }

    private void ResetPlayerPosition()
    {
        player.transform.position = startPosition;
    }
}
