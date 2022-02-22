using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryControl : MonoBehaviour
{


    public Rigidbody cherryRb;
    public float throwDistance = 500f;
    public float timeToDestroy = 3f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerManager.currentCherryCount > 0)
            {
                ThrowCherry();
            }
        }
    }


    void ThrowCherry()
    {
        Rigidbody cherryClone = (Rigidbody)Instantiate(cherryRb, transform.position, transform.rotation);
        cherryClone.useGravity = true;
        cherryClone.constraints = RigidbodyConstraints.None;
        cherryClone.AddForce(player.transform.forward * throwDistance);
        Destroy(cherryClone.gameObject, timeToDestroy);
        PlayerManager.currentCherryCount -= 1;
    }



}
