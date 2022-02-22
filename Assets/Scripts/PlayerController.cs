using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string idleState, walkState, runState, jumpState, throwState, dieState;
    bool isWalking, isRunning, isJumping, isIdle, iDead, forward, backward, left ,right;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {

        // DOWN STATES
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                forward = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                left = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                backward = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                right = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isWalking)
            {
                isRunning = true;
                isWalking = false;
                m_Animator.SetBool(walkState, false);
                m_Animator.SetBool(runState, true);
            }
        }


        ///ACTIONS
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
      
        if (Input.GetKeyDown(KeyCode.E)|Input.GetMouseButtonDown(0))
        {
            Throw();
        }

        //// UPSATETS
        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
            if(!left && !right && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            if (!forward && !right && !backward)
            {
                StopMotion();
            }
        }
            if (Input.GetKeyUp(KeyCode.S))
            {
                backward = false;
                if (!left && !right && !forward)
                {
                    StopMotion();
                }
            }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            if (!left && !forward && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isRunning && isWalking)
            {
                isRunning = false;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(runState, false);
            }
        }
    }


     void StopMotion()
    {

                    isWalking = false;
                    isRunning = false;
                    isIdle = true;
                    m_Animator.SetBool(idleState, true);
                    m_Animator.SetBool(walkState, false);
                    m_Animator.SetBool(runState, false);

    }
    void Jump()
    {
        m_Animator.SetBool(jumpState, true);
        m_Animator.SetBool(idleState, false);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
        StartCoroutine(ConsumeJump());
    }

    void Throw()
    {
        m_Animator.SetBool(throwState, true);
        m_Animator.SetBool(idleState, false);
        StartCoroutine(ConsumeThrow());
    }

    IEnumerator ConsumeJump()
    {
        yield return new WaitForSeconds(0.66f);
        m_Animator.SetBool(jumpState, false);
        ReturnMoveState();
    }

    IEnumerator ConsumeThrow()
    {
        yield return new WaitForSeconds(0.66f);
        m_Animator.SetBool(throwState, false);
        ReturnMoveState();
    }

    void ReturnMoveState()
    {
        if (isRunning)
        {
            m_Animator.SetBool(runState, true);
        }
        else if (isWalking)
        {
            m_Animator.SetBool(walkState, true);
        }
        else
        {
            m_Animator.SetBool(idleState, true);
        }
    }
}
