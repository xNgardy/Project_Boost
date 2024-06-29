using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    private CollisionHandler _collisionHandler;
    void Start()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        LoadToNextLevel();
    }

    void LoadToNextLevel()
    {
        if (Input.GetKey(KeyCode.L))
        {
            _collisionHandler.LoadNextLevel();
        }
    }

    
}
