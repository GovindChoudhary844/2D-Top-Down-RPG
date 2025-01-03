using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        // Prevents the object from being destroyed when a new scene is loaded
        DontDestroyOnLoad(gameObject);
    }
}
