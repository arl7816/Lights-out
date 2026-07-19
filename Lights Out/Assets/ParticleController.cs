using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ParticleDataClass
{
    public ParticleSystem particleSystem { get; private set; }
    public bool canBeUsed { get; private set; }
    public float timeUntilActiveAgain {get; private set;}

    public ParticleDataClass(ParticleSystem particleSystem, bool canBeUsed = true, float timeUntilActiveAgain = 0f)
    {
        this.particleSystem = particleSystem;
        this.canBeUsed = canBeUsed;
        this.timeUntilActiveAgain = timeUntilActiveAgain; 
    }

    public void setCanBeUsed(bool newState)
    {
        this.canBeUsed = newState; 
    }
}

[RequireComponent(typeof(PlayerState)),
    RequireComponent(typeof(BoxRaycasting))]
public class ParticleController : MonoBehaviour
{
    private PlayerState playerState;
    private BoxRaycasting collisionDetection;

    [SerializeField]
    private ParticleSystem basicGroundHitParticleSystem;
    private ParticleDataClass basicGroundHitParticleDataClass;

    [SerializeField]
    private ParticleSystem fallingParticleSystem;
    private ParticleDataClass fallingParticleDataClass;

    private void Awake()
    {
        fetchComponents();
        createNeededDataClasses();
        ensureParticleSystemsCollected();
    }

    private void OnEnable()
    {
        collisionDetection.GroundEntered += attemptGroundHit;
        playerState.playerStartedHeavyFall += startFallingSystem;
        playerState.playerStoppedHeavyFall += stopFallingSystem;
    }

    private void OnDisable()
    {
        collisionDetection.GroundEntered -= attemptGroundHit;
        playerState.playerStartedHeavyFall -= startFallingSystem;
        playerState.playerStoppedHeavyFall -= stopFallingSystem;
    }

    private void fetchComponents()
    {
        playerState = gameObject.GetComponent<PlayerState>();
        collisionDetection = gameObject.GetComponent<BoxRaycasting>();
    }

    private void createNeededDataClasses()
    {
        basicGroundHitParticleDataClass = new ParticleDataClass(
            particleSystem: basicGroundHitParticleSystem,
            canBeUsed: true,
            timeUntilActiveAgain: .2f
        );

        fallingParticleDataClass = new ParticleDataClass(
            particleSystem: fallingParticleSystem,
            canBeUsed: true
        );
    }

    private void ensureParticleSystemsCollected()
    {
        if (basicGroundHitParticleDataClass.particleSystem == null)
        {
            Debug.LogError(getMissingVariableErrorMessage(variableName: "Basic Ground Hit Particle System"));
        }

        if (fallingParticleDataClass.particleSystem == null)
        {
            Debug.LogError(getMissingVariableErrorMessage(variableName: "Falling Particle System"));
        }
    }

    private string getMissingVariableErrorMessage(string variableName)
    {
        return "Missing " + variableName + ". Please insert missing element in editor.";
    }

    private void attemptGroundHit()
    {
        if (basicGroundHitParticleDataClass.particleSystem == null) return;

        if (!basicGroundHitParticleDataClass.canBeUsed) return;

        StartCoroutine(
            activateGroundHit()
        );
    }

    private IEnumerator activateGroundHit()
    {
        basicGroundHitParticleDataClass.setCanBeUsed(false);
        basicGroundHitParticleDataClass.particleSystem.Play();
        yield return new WaitForSeconds(basicGroundHitParticleDataClass.timeUntilActiveAgain);
        basicGroundHitParticleDataClass.setCanBeUsed(true);
    }

    private void startFallingSystem()
    {
        if (fallingParticleDataClass.particleSystem == null) return;
        fallingParticleDataClass.particleSystem.Play();
    }

    private void stopFallingSystem()
    {
        if (fallingParticleDataClass.particleSystem == null) return;
        fallingParticleDataClass.particleSystem.Stop();
    }
}
