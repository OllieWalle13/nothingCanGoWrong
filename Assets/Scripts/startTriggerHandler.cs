using UnityEngine;

public class startTriggerHandler : MonoBehaviour
{
    public ClockTimer clockTimer;
    bool started = false;

    void OnTriggerEnter2D(Collider2D collider2D) {
        Debug.Log (collider2D. gameObject.tag);
        if (collider2D.gameObject.CompareTag("Player")) {
            if (!started) {
                clockTimer.setStartTime(Time.time);
                started = true;
            }
        }
    }
}
