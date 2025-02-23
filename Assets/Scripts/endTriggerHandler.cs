using UnityEngine;

public class endTriggerHandler : MonoBehaviour
{
    public ClockTimer clockTimer;

    void OnTriggerEnter2D(Collider2D collider2D) {
        Debug.Log (collider2D. gameObject.tag);
        if (collider2D.gameObject.CompareTag("Player")) {
            clockTimer.setEndTime(Time.time);
        }
    }
}
