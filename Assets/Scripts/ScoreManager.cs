using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{


    public static int currentPoints;
    Text pointsText;
    private void Awake()
    {
        pointsText = GetComponent<Text>();
        currentPoints = 0;
    }

    private void Update()
    {
        pointsText.text = currentPoints.ToString();
    }



}
