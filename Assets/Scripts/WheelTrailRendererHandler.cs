using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WheelTrailRendererHandler : MonoBehaviour {
    
    // Components
    TopDownCarController TopDownCarController;
    TrailRenderer TrailRenderer;

    void Awake() {

    	TopDownCarController = GetComponentInParent<TopDownCarController>();

    	TrailRenderer = GetComponent<TrailRenderer>();

    	// Set trail renderer not to emit at the start
    	TrailRenderer.emitting = false;
    }

    void Update () {
    	if (TopDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking)) {
    		TrailRenderer.emitting = true;
    	} else {
    		TrailRenderer.emitting = false;
    	}
    }
}
