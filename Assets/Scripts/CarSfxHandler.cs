using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSfxHandler : MonoBehaviour {
    
    [Header("Audio Source")]
    public AudioSource tiresSreechingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    // components
    TopDownCarController topDownCarController;


    void Awake () {
    	topDownCarController = GetComponentInParent<TopDownCarController>();
    }

    void Update () {
    	UpdateEngineSFX();
    	UpdateTiresScreechingSFX();
    }

    void UpdateEngineSFX() {
    	// Handle engine sfx
    	float velocityMagnitude = topDownCarController.GetVelocityMagnitude();


    	// Increase the engien volume as the car goes faster
    	float desiredEngineVolume = velocityMagnitude * 0.05f;

    	// But keep a mininum level so it plays even if the car is idle
    	desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

    	engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

    	desiredEnginePitch = velocityMagnitude * 0.2f;
    	desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
    	engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX () {
    	if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking)) {
    		if (isBraking) {
    			tiresSreechingAudioSource.volume = Mathf.Lerp(tiresSreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
    			tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
    		} else {
    			tiresSreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * .20f;
    			tireScreechPitch = Mathf.Abs(lateralVelocity) * .1f;    		}
    	} else {
    		tiresSreechingAudioSource.volume = Mathf.Lerp(tiresSreechingAudioSource.volume, 0, Time.deltaTime * 10);
    	}
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
    	float relativeVelocity = collision2D.relativeVelocity.magnitude;

    	float volume = relativeVelocity * 0.1f;

    	carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
    	carHitAudioSource.volume = volume;

    	if (!carHitAudioSource.isPlaying) {
    		carHitAudioSource.Play();
    	}
    }
}
