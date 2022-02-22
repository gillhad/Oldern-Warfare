using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlePatrol : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isDead = false;
    public static bool isEating;
    public static bool isAttacking;
    public float speed = 5f;
    public float directionChangeInterval = 5f;
    public float maxHeadingChange = 30f;

    Animator beetleAnimator;
    CharacterController controller;
    float heading; // angulo entre 0 - 360º
    Vector3 targetRotation;

   IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRotunine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    private void Start()
    {
        beetleAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        heading = Random.Range(0, 90);
        transform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(NewHeading());
    }

    private void Update()
    {
        if (!isDead&&!isEating&&!isAttacking)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,  // donde esta mirando
                targetRotation, //hacia donde quiero que mire
                Time.deltaTime * directionChangeInterval); // tiempo en hacerlo
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward * speed);
        }
    }
    void NewHeadingRotunine()
    {
        float floor = transform.eulerAngles.y - maxHeadingChange;
        float ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

}
