using UnityEngine;
using DG.Tweening;

public class PlayerCollisions : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ColorController colorControllerScript;
    [SerializeField] private ScoreManager scoreManagerScript;
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private ParticleController particleControllerScript;
    [SerializeField] private PlayerMovement playerMovementScript;

    [Header("Scale Values")]
    [SerializeField] private Vector3 changeScaleValues; //values by which the scale will change
    public Vector3 startPlayerScale;

    [Header("Level Value")]
    [SerializeField] private int levelValueToChange;

    [HideInInspector]
    public Vector3 currentPlayerScale;

    [HideInInspector]
    public int playerLevel = 0;

    [HideInInspector]
    public int _maxScore = 0;

    private void Start()
    {
        currentPlayerScale = startPlayerScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(colorControllerScript.GetCurrentColor()))
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.rightColorSound);
            particleControllerScript.SetParticleColor();
            particleControllerScript.PlayParticle();
            IncreaseScale();
            SetLevelPlayer(levelValueToChange);
            SetMaxScore();
            Destroy(other.gameObject);
        }

        if (!other.gameObject.CompareTag(colorControllerScript.GetCurrentColor()) && !other.gameObject.CompareTag("Finish"))
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.wrongColorSound);
            colorControllerScript.SetWrongColorEffect();
            DecreaseScale();
            SetLevelPlayer(-levelValueToChange);
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Finish"))
        {
            gameManagerScript.DisablePlate();
            gameManagerScript.isFinished = true;
            playerMovementScript.SetKinematic(true);
            StartCoroutine(gameManagerScript.WinMenu());
        }
    }

    //increase player scale
    private void IncreaseScale()
    {
        if (playerLevel < 10)
        {
            currentPlayerScale = currentPlayerScale + changeScaleValues;
            transform.DOScale(currentPlayerScale, 0.5f);
        }
    }

    //decrease player scale
    private void DecreaseScale()
    {
        if (currentPlayerScale != startPlayerScale)
        {
            currentPlayerScale = currentPlayerScale - changeScaleValues;
            transform.DOScale(currentPlayerScale, 0.5f);
        }
    }

    private void SetLevelPlayer(int _levelValue)
    {
        playerLevel = playerLevel + _levelValue;

        if (playerLevel >= 0)
        {
            scoreManagerScript.AddLevelValue(playerLevel);
        }
        else if (playerLevel < 0)
        {
            playerMovementScript.SetKinematic(true);
            StartCoroutine(gameManagerScript.GameOverMenu());
        }
    }
    
    private void SetMaxScore()
    {
        if (playerLevel > _maxScore)
        {
            _maxScore = playerLevel;
        }
        scoreManagerScript.UpdateMaxScore(_maxScore);
    }
}
