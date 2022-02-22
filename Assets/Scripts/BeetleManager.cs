using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour
{
    private string m_Tag = "Beetle";
    public static int currentBeetle = 0;
    Text beetleTextCount;


    public GameObject[] cucumbers;

    private void Awake()
    {
        beetleTextCount = GetComponent<Text>();
        currentBeetle = 1;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cucumbers = GameObject.FindGameObjectsWithTag(m_Tag);
        currentBeetle = cucumbers.Length;
        beetleTextCount.text = currentBeetle.ToString();
    }
}