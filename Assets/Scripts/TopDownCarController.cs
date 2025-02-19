using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopDownCarController : MonoBehaviour {

	[Header("Car settings")]
	public float driftFactor = 0.95f;
	public float accelerationFactor = 30.0f;
	public float turnFactor = 3.5f;
	public float dragFactor = 1.5f;
	public float maxSpeed = 20.0f;

	// Local variables
	float accelerationInput = 0;
	float steeringInput = 0;

	float rotationAngle = 0;

	float velocityVsUp = 0;

	Rigidbody2D carRigidbody2D;

	void Awake() {
		carRigidbody2D = GetComponent<Rigidbody2D>();
	}

    void Start() {

    }

    void Update() {

    }

    void FixedUpdate() {
    	ApplyEngineForce();

    	KillOrthogonaVelocity();

    	ApplySteering();
    }


    void ApplyEngineForce() {

    	// Calculate how much "forward" we are going in terms of the direction of our velocity
    	velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.linearVelocity);

    	// Limit so we cannot go faster than the max speed in the "forward" direction
    	if (velocityVsUp > maxSpeed && accelerationInput > 0) {
    		return;
    	}

    	// Limit so we cannot go faster than the 25 percent of the max speed in the "reveerse" direction
    	if (velocityVsUp < -maxSpeed * .25f && accelerationInput < 0) {
    		return;
    	}

    	// Limit sot we cannot go faster in any direction while accelerating
    	if (carRigidbody2D.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) {
    		return;
    	}

    	// Apply drag if there is no accelerationInput so the car stops when the palyer lets fo of the accelerator
    	if (accelerationInput == 0) {
    		carRigidbody2D.linearDamping = Mathf.Lerp(carRigidbody2D.linearDamping, dragFactor, Time.fixedDeltaTime * 3);
    	} else {
    		carRigidbody2D.linearDamping = 0;
    	}

    	// Create a force for the engine
    	Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

    	// Apply force and pushes the car forward
    	carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }

    void ApplySteering() {

    	// Limit the cars ability to turn when moving slowly
    	float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.linearVelocity.magnitude / 8);
    	minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

    	// Update the rotation angle based on input
    	rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

    	// Apply steering by rotating teh car object
    	carRigidbody2D.MoveRotation(rotationAngle);
	}

	void KillOrthogonaVelocity() {
		Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.linearVelocity, transform.up);
		Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.linearVelocity, transform.right);

		carRigidbody2D.linearVelocity = forwardVelocity + rightVelocity * driftFactor;
	}

	float GetLateralVelocity() {
		return Vector2.Dot(transform.right, carRigidbody2D.linearVelocity);
	}

	public bool IsTireScreeching(out float lateralVelocity, out bool isBraking) {
		lateralVelocity = GetLateralVelocity();
		isBraking = false;

		if (accelerationInput < 0 && velocityVsUp > 0) {
			isBraking = true;
			return true;
		}

		if (Mathf.Abs(GetLateralVelocity()) > 2.0f) {
			return true;
		}

		return false;
	}

	public void SetInputVector(Vector2 inputVector) {
		steeringInput = inputVector.x;
		accelerationInput = inputVector.y;
	}
}
