using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleNPC : MonoBehaviour
{
    // Start is called before the first frame update


    Animator  m_Animator;
    public GameObject nextCucumberToDestroy;

    //variables responder ataque
    public bool cherryhit = false;
    public float smoothTime = 3.0f;
    public Vector3 smootVelocity = Vector3.zero;
    public HealthManager healthManager;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        healthManager = GameObject.Find("Health").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (cherryhit)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform transPlayer = player.transform;
            this.gameObject.transform.LookAt(transPlayer);
            transform.position = Vector3.SmoothDamp(transform.position, transPlayer.position,ref smootVelocity, smoothTime);
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            healthManager.ReduceHealth();
            Debug.Log(HealthManager.currentHealth);

            if (!cherryhit)
            {
                
                GameObject thePlayer = collision.gameObject;
                Transform trans = thePlayer.transform;
                this.gameObject.transform.LookAt(trans);
               
                m_Animator.Play("attack_OnGround");
                StartCoroutine(DestroyBeetle());
                BeetlePatrol.isAttacking = true;
            }
            else
            {
                m_Animator.Play("attack_Standing");
                StartCoroutine(DestroyBeetle());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cucumber"))
        {
            nextCucumberToDestroy = other.gameObject;
            BeetlePatrol.isEating = true;
            m_Animator.Play("Eat_OnGround");
            StartCoroutine(DestroyCucumber());
        }
        if (other.gameObject.CompareTag("Cherry"))
        {
            BeetlePatrol.isAttacking = true;
            cherryhit = true;
            m_Animator.Play("Stand");
            ScoreManager.currentPoints += 20;
        }
    }

    IEnumerator DestroyCucumber()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(nextCucumberToDestroy.gameObject);
        BeetlePatrol.isEating = false;
    }

    IEnumerator DestroyBeetle()
    {
        yield return new WaitForSeconds(5.0f);
        m_Animator.Play("die_OnGround");
        Destroy(this.gameObject);
    }

    IEnumerator DestroyBeetleStanding()
    {
        yield return new WaitForSeconds(5.0f);
        m_Animator.Play("die_OnGround");
        Destroy(this.gameObject);
        cherryhit = false;
    }
}
