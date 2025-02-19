using UnityEngine;

public class WheelParticleHandler : MonoBehaviour {
    
    // local variable
    float particleEmissionRate = 0;

    // Components
    TopDownCarController topDownCarController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake() {

    	topDownCarController = GetComponentInParent<TopDownCarController>();

    	particleSystemSmoke = GetComponent<ParticleSystem>();

    	particleSystemEmissionModule = particleSystemSmoke.emission;

    	particleSystemEmissionModule.rateOverTime = 0;
    }

    void Update() {
    	// Reduce particles over time
    	particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
    	particleSystemEmissionModule.rateOverTime = particleEmissionRate;

    	if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking)) {
    		// if the care tires are scheeching then we'll emit smoke, more if the player is braking
    		if (isBraking) {
    			particleEmissionRate = 100;
    		} else { // If the player is drifting we'll emit smoke based on how much the player is drifting
    			particleEmissionRate = Mathf.Abs(lateralVelocity) * 10;
    		}
    	}
    }
}
