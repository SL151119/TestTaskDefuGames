using UnityEngine;
using DG.Tweening;

public class ColorController : MonoBehaviour
{
    [Header("Materials")]
    public Material playerMaterial;
    [SerializeField] private Material waterMaterial;

    [Header("Colors")]
    [SerializeField] private Color[] materialColors;

    [HideInInspector]
    public string currentPlayerColor;

    private int _randomIndex;

    private void Start()
    {
        SetRandomColor();
    }
    private void OnDestroy()
    {
        playerMaterial.color = Color.white;
        waterMaterial.color = Color.white;
    }

    public void SetRandomColor()
    {
        GenerateRandomColor();
        CheckCurrentColor(_randomIndex);
        playerMaterial.color = materialColors[_randomIndex];
        waterMaterial.color = playerMaterial.color;
    }

    private void GenerateRandomColor()
    {
        _randomIndex = Random.Range(0, materialColors.Length);
    }

    //assigning a tag to a player color
    private void CheckCurrentColor(int colorIndex)
    {
        switch(colorIndex)
        {
            case 0:
                currentPlayerColor = "Orange";
                break;
            case 1:
                currentPlayerColor = "Green";
                break;
            case 2:
                currentPlayerColor = "Blue";
                break;
        }
    }

    public string GetCurrentColor()
    {
        return currentPlayerColor;
    }

    public void SetWrongColorEffect()
    {
        playerMaterial.DOColor(Color.gray, 0.2f).SetEase(Ease.InFlash, 2,0); //blinking of the player's color on a wrong hit
    }
}
