using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public static CameraFollower sharedInstance;
    public GameObject followTarget;

    public float movementSmoothness = 1.0f;
    public float rotationSmootheness = 1.0f;
    
    public bool canFollow = true; // Cuando debe o no seguir al personaje


    private void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (followTarget == null || canFollow == false)
        {
            return; // return se usa para devolver un vacío
        }
        transform.position = Vector3.Lerp(transform.position, followTarget.transform.position, Time.deltaTime * movementSmoothness); //time delta time mide el ultimo tiempo de renderizado, Lerp Lineal interpolation ir de un punto a otro en linea
        transform.rotation = Quaternion.Slerp(transform.rotation, followTarget.transform.rotation, Time.deltaTime * rotationSmootheness); // Slerp usa sphere, no linear
     }


}
