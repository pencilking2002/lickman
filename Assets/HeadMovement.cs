using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class HeadMovement : MonoBehaviour
{
    [Header("Movement")]
    public float headSmoothing;
    public float floorYClamp;

    [Header("Transforms")]
    public Transform neckConnectPoint;

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
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHead();
    }

    private void MoveHead()
    {
        transform.position = Vector3.Lerp(transform.position, GetHeadGoalPosition(transform.position), Time.deltaTime * headSmoothing);
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
            return ClampPositionHalfCircle(hitPoint,neckLength);
        }

        return currentPosition;
    }


    private Vector3 ClampPositionHalfCircle( Vector3 position, float neckLength)
    {
        Vector3 circleCenter = new Vector3(
            neckConnectPoint.transform.position.x,
            neckConnectPoint.transform.position.y,
            0f );

        Vector3 circlePosition = (position - circleCenter).normalized * neckLength + circleCenter;

        circlePosition = new Vector3(circlePosition.x,Mathf.Max(circlePosition.y, floorYClamp),0f);

        if (Vector3.Distance(position, circleCenter) > neckLength)
            return ClampYHeight(circlePosition);
        else
            return ClampYHeight(position);

    }

    private Vector3 ClampYHeight(Vector3 vector)
    {
        return new Vector3(vector.x, Mathf.Max(vector.y, floorYClamp), 0f);
    }
}
