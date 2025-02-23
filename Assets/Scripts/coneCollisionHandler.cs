using UnityEngine;

public class coneCollisionHandler : MonoBehaviour
{
    public ClockTimer clockTimer;
    bool coneHit = false;

    void OnCollisionEnter2D(Collision2D collision2D) {
        Debug.Log(collision2D.gameObject.tag);
        if (!coneHit) {
            if (collision2D.gameObject.CompareTag("Player")) {
                clockTimer.increaseConeCount();
                coneHit = true;
            }
        }
    }
}
