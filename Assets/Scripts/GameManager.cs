using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public float testFloat;

    void Awake()
    {
        InitInstance();
    }

    private void InitInstance()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Instance = null;
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
