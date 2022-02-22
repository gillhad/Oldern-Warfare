using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    Text victoryText;
    private void Awake()
         
    {
        victoryText = GetComponent<Text>();
        //victoryText.text = "";
        victoryText.enabled = false;
    }


    private void Update()
    {
        if(BeetleManager.currentBeetle == 0)
        {
            victoryText.text = "HAS GANAO";
            victoryText.enabled = true;
        }

        if(Cucumbermanager.currentCucumberCount == 0 || PlayerManager.currentLifes==0)
        {
            victoryText.text = "game over";
            victoryText.enabled = true;
        }
    }
    

}
