using UnityEngine;
using TMPro;

public class ClockTimer : MonoBehaviour
{
    public ResultHandler resultHandler;
    public TMP_Text timer;
    public TMP_Text conesHit;
    float startTime = 0;
    float endTime = 0;
    float coneCount = 0;
    float coneTime = 0;

    void Awake () {
        startTime = Time.time;
    }

    void Start () {
        timer.text = secondsToString(startTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime != 0 && endTime == 0) {
            timer.text = secondsToString(Time.time - startTime + coneTime);
        }
        conesHit.text = "Cones Hit: " + coneCount.ToString() + " +" + coneTime.ToString() + " seconds";
    }

    public static string secondsToString(float seconds) {
        int min = (int)seconds / 60;
        float sec = seconds % 60;
        float frac = (seconds * 100) % 100;

        return string.Format("{0:00}:{1:00}:{2:000}", min, sec, frac);
    }

    public void increaseConeCount() {
        coneCount += 1;
        coneTime += 2;
    }

    public void setStartTime(float time) {
        startTime = time;
    }

    public void setEndTime(float time) {
        endTime = time;
        finish(startTime, coneTime);
    }

    void finish (float startTime, float coneTime) {
        float totalTime = Time.time - startTime + coneTime;
        Debug.Log(totalTime);

        if (totalTime == 22) {
            resultHandler.setWinResult(3);
        } else if (totalTime < 22) {
            resultHandler.setWinResult(1);
        } else {
            resultHandler.setWinResult(2);
        }
    }
}
