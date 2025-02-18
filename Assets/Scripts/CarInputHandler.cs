using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarInputHandler : MonoBehaviour {

	// Components
    TopDownCarController TopDownCarController;

    void Awake() {

    	TopDownCarController = GetComponent<TopDownCarController>();
    }

    void Update() {

    	Vector2 inputVector = Vector2.zero;

    	inputVector.x = Input.GetAxis("Horizontal");
    	inputVector.y = Input.GetAxis("Vertical");

    	TopDownCarController.SetInputVector(inputVector);
    }
}
