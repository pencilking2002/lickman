using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInputSystem : MonoBehaviour
{
    public static ManagerInputSystem instance;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetWASDInput() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public Vector3 GetArrowKeyInput() => new Vector2(Input.GetAxis("HorizontalArrows"), Input.GetAxis("VerticalArrows"));
}
