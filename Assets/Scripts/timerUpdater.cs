using UnityEngine;

public class timerUpdater : MonoBehaviour
{
    TopDownCarController topDownCarController;

    public ClockTimer clockTimer;
    bool started = false;
    bool isColliding = false;



    void Awake () {
        topDownCarController = GetComponentInParent<TopDownCarController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        Debug.Log(collision2D.gameObject.tag);
        if (started) {
            if (isColliding){
                return;
            }
            if (collision2D.gameObject.CompareTag("Cone")) {
                isColliding = true;
                clockTimer.increaseConeCount();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D) {
        Debug.Log (collider2D. gameObject.tag);
        if (collider2D.gameObject.CompareTag("Start")) {
            clockTimer.setStartTime(Time.time);
            started = true;
        }
    }
}
