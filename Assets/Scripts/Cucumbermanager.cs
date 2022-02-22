using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cucumbermanager : MonoBehaviour
{
    // Start is called before the first frame update


    private string m_Tag = "Cucumber";
    public static int currentCucumberCount = 0;
    Text cucumberTextCount;


    public GameObject[] cucumbers;

    private void Awake()
    {
        cucumberTextCount = GetComponent<Text>();
        currentCucumberCount = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cucumbers = GameObject.FindGameObjectsWithTag(m_Tag);
        currentCucumberCount = cucumbers.Length;
        cucumberTextCount.text = currentCucumberCount.ToString();
    }
}
