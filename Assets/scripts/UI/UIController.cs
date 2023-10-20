using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text scoreText;
    public void setScore(int score) {
        scoreText.text = ($"Score : {score}");
    }
}
