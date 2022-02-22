using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int currentCherryCount;
    public int tempCurrentCherryCount;
    public bool isCollectingCherries;
    public static int currentLifes;
    bool isDead;

    public Transform[] spawningZone;

    private void Awake()
    {
        currentCherryCount = 1;
        isCollectingCherries = false;
        tempCurrentCherryCount = 0;
        currentLifes = 3;
        isDead = false;
    }

    private void Update()
    {
        if (isCollectingCherries)
        {
            //StartCoroutine(PickCherry());
            if (tempCurrentCherryCount >= 60)  //cada frame
            {
                currentCherryCount += 1;
                tempCurrentCherryCount = 0;
                ScoreManager.currentPoints += 5;
            }
            else
            {
                tempCurrentCherryCount += 1;
            }

        }

        if (HealthManager.currentHealth <= 0 && !isDead)
        {
            currentLifes--;
            isDead = true;

            if (currentLifes == 2)
            {
                Destroy(GameObject.Find("life3"));
                StartCoroutine(RespawnPlayer());
            }
            if (currentLifes == 1)
            {
                Destroy(GameObject.Find("life2"));
                StartCoroutine(RespawnPlayer());
            }
            if (currentLifes == 0)
            {
                Destroy(GameObject.Find("life"));

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CherryTree")){

            Debug.Log("toco un cherry tree");
            isCollectingCherries = true;
            currentCherryCount += 1;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CherryTree"))
        {
            isCollectingCherries = false;
        }
    }

    IEnumerator PickCherry()
    {
        yield return new WaitForSeconds(2.0f);
        currentCherryCount += 1;
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3.0f);
        int randomPos = Random.Range(0, spawningZone.Length);
        this.transform.position = spawningZone[randomPos].transform.position;
        isDead = false;
        HealthManager.currentHealth = 100;
    }
}
