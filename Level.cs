using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Parameters
    [SerializeField] public int blockCount;
    
    // Cached Reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        blockCount++;
    }

    public void BlockDestryed()
    {
        blockCount--;

        if (blockCount <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
