using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{


    float horizontal, vertical;
    Rigidbody m_Rigidbody;
    public float jumpForce, moveSpeed, runSpeed; // configuración de la máxima

    private float currentJumpForce = 0, currentMoveSpeed = 0; // velocidad original para hacer los cálculos

    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;
        m_Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        CheckGroundStatus();
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            m_Rigidbody.AddForce(0, jumpForce, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            currentMoveSpeed = runSpeed;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))  // No chequeamos el suelo porque levantar shift en el aire implica error
        {

            currentMoveSpeed = moveSpeed;
        }


    }


    private float turnAmount,forwardAmount;
    [SerializeField] float stationaryTurnAround = 180; // añadimos un máximo a la velocidad de giro
    [SerializeField] float movingTurnSpeed = 360; // velocidad durante movimiento
    public Transform m_camera;
    private Vector3 cameraForward; // Donde estan los ojos del personaje vs camera TRABAJAMOS CON LA CAMARA no el personaje para movernos
    private Vector3 move;
    private bool jump;
    private Vector3 groundNormal;
    private void FixedUpdate()
    {
        if(m_camera != null)
        {
            cameraForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized; // escalamos la velocidad 
            move = vertical * cameraForward + horizontal * m_camera.right; // Conb esto cogemos el movimiento que hacemos al darle fuerza
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }
        if(move.magnitude > 0)
        {
            Move(move); //    le damos un valor al movimiento que tenemos creado
        }

    }

    private bool isGrounded;  // comprueba si el personaje esta en el suelo
    [SerializeField] float groundCheckDistance = 0.1f; //raycas ten 3d
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, //empezar a contar unos centimetros mas encima de la suela del jugador
                                 Vector3.down,                      // Mandamos un rayo hacia abajo
                                out hitInfo,                        // alamcenamos en la variable
                                 groundCheckDistance))
        {
            isGrounded = true;
            groundNormal = hitInfo.normal;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }



    }

    private void Move(Vector3 move)
    {

        if(move.magnitude > 1.0f) //  Funcionamos con vectores, con lo que le damos un valor max de 1 para que sea escalable, multiplicaremos el moviomiento x1 siempre
        {
            move.Normalize();
        }
        move = transform.InverseTransformDirection(move); // arreglamos la diferencia con el personaje al ser un "hijo"
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal); //modificamos el movimiento segun el vector normal a la superficie, bajadas / subidas 
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z; // Z lo seleccionamos como "delante"
        m_Rigidbody.velocity = transform.forward * currentMoveSpeed;
        ApplyRotation();
    }

    void ApplyRotation(){
        float turnSpeed = Mathf.Lerp(stationaryTurnAround, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnSpeed*turnAmount*Time.deltaTime, 0); // giramos en el eje Y

    }

    [SerializeField] float moveSpeedMultiplier = 1.0f;
    private void OnAnimatorMove()
    {
       if(isGrounded && Time.deltaTime > 0)
        {
            Vector3 vel = m_Animator.deltaPosition * moveSpeedMultiplier / Time.deltaTime;
            vel.y = m_Rigidbody.velocity.y; // mantener velocida de salto
            m_Rigidbody.velocity = vel;
        } 
    }


}
