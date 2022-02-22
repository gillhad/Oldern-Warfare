using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryManager : MonoBehaviour
{

    Text cherryTextCount;

    
    private void Awake()
    {
        cherryTextCount = GetComponent<Text>();
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        cherryTextCount.text = PlayerManager.currentCherryCount.ToString();
    }
}
    




