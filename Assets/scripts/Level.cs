using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour

{
    [SerializeField]public int breakableBlocks;  // serialize for debugging purposes

    ScreenLoader screenLoader;
    private void Start()
    {
        screenLoader = FindObjectOfType<ScreenLoader>();


    }

    public void CountBlocks()
    {
        breakableBlocks++;

    }


    public void BlockIsDestroyed()
    {
        breakableBlocks--;

        if (breakableBlocks <= 0)
        {
            screenLoader.LoadNextScene();
        }
    }

    }
    

