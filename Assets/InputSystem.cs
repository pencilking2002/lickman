using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
  
    public Vector3 GetWASDInput() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
}
