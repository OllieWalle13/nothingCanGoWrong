using UnityEngine;
using TMPro;

public class ResultHandler : MonoBehaviour
{
    TMP_Text resultText;

    void Awake() {
        resultText = GetComponentInChildren<TMP_Text>();
    }

    public void setWinResult (int result) {
        if (result == 1) {
            resultText.text = ("You Win!");
        } else if (result == 2) {
            resultText.text = ("You Lost!");
        } else {
            resultText.text = ("You Tied!");
        }
    }
}
