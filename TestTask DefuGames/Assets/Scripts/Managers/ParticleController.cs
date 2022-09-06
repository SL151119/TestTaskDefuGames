using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ColorController colorControllerScript;

    [Header("ParticleSystem")]
    [SerializeField] private ParticleSystem rightColorParticle;

    private Renderer _rend;

    private void Start()
    {
       _rend = rightColorParticle.GetComponent<Renderer>();
    }

    public void SetParticleColor()
    {
        _rend.material = colorControllerScript.playerMaterial; // the particle takes on the color of the player
    }
    public void PlayParticle()
    {
        rightColorParticle.Play();
    }
}
