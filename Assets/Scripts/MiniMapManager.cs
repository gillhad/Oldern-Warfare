using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    RawImage map;

    private void Awake()
    {
        map = GetComponent<RawImage>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.enabled = !map.enabled;
        }
    }
}
