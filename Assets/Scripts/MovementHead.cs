using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class MovementHead : MonoBehaviour
{
    [Header("Movement Mouse")]
    public float headSmoothing;

    [Header("Movement Not Mouse")]
    public float speed;

    [Header("Position")]
    public float floorYClamp;


    [Header("Control Scheme")]
    public bool useArrowKeys;

    [Header("Transforms")]
    public Transform neckConnectPoint;
    public Transform body;

    [Header("Debug")]
    public float neckLength; // this variable will be changed outside this script

    
    Plane infinitePlane;
    Vector3 m_DistanceFromCamera;

    void Start()
    {
        //m_DistanceZ = 
        //This is how far away from the Camera the plane is placed
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

        //Create a new plane with normal (0,0,1) at the position away from the camera you define in the Inspector. This is the plane that you can click so make sure it is reachable.
        infinitePlane = new Plane(Vector3.forward, m_DistanceFromCamera);
    }


    void FixedUpdate()
    {
        UpdateInfinitePlaneAccordingToBodyZPosition();

        if (useArrowKeys)
            MoveHeadUsingTranslation();
        else
            MoveHeadBasedOnMousePosition();
    }

    private void UpdateInfinitePlaneAccordingToBodyZPosition()
    {
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, body.position.z);

        //Create a new plane with normal (0,0,1) at the position away from the camera you define in the Inspector. This is the plane that you can click so make sure it is reachable.
        infinitePlane = new Plane(Vector3.forward, m_DistanceFromCamera);
    }

    private void MoveHeadBasedOnMousePosition()
    {
        transform.position = Vector3.Lerp(transform.position, GetHeadGoalPosition(transform.position), Time.deltaTime * headSmoothing);
    }
    private void MoveHeadUsingTranslation()
    {
        Vector3 direction = new Vector3(ManagerInputSystem.instance.GetArrowKeyInput().x, ManagerInputSystem.instance.GetArrowKeyInput().y, 0f);
        transform.Translate(direction * speed * Time.deltaTime);
        transform.position = ClampPositionHalfCircle(transform.position, neckLength);
        transform.position = new Vector3(transform.position.x, transform.position.y, body.position.z);
    }

    private Vector3 GetHeadGoalPosition(Vector3 currentPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (infinitePlane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);

            //Move your cube GameObject to the point where you clicked
            Vector3 movePosition = ClampPositionHalfCircle(hitPoint, neckLength);

            return movePosition;
        }

        return new Vector3(currentPosition.x,currentPosition.y,body.position.z);
    }


    private Vector3 ClampPositionHalfCircle( Vector3 position, float neckLength)
    {
        Vector3 circleCenter = neckConnectPoint.transform.position;

        Vector3 circlePosition = (position - circleCenter).normalized * neckLength + circleCenter;

        circlePosition = new Vector3(circlePosition.x,Mathf.Max(circlePosition.y, floorYClamp),circlePosition.z);

        if (Vector3.Distance(position, circleCenter) > neckLength)
            return ClampYHeight(circlePosition);
        else
            return ClampYHeight(position);

    }

    private Vector3 ClampYHeight(Vector3 vector)
    {
        return new Vector3(vector.x, Mathf.Max(vector.y, floorYClamp), vector.z);
    }
}
