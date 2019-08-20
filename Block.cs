using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject sparklesVFX;
    [SerializeField] Sprite[] boxSprites;

    // cached reference
    Level level;

    // state variables
    [SerializeField] int timesHit; // only serialized for debug purpose

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();

        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = boxSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }        
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;

        if (boxSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = boxSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(gameObject.name + " - Block sprite is missing from array!");
        }
        
    }

    private void DestroyBlock()
    {
        DestroyBlockFX();
        Destroy(gameObject, 0);
        level.BlockDestryed();
        FindObjectOfType<GameSession>().AddPoints();
    }

    private void DestroyBlockFX()
    {
        GameObject sparkles = Instantiate(sparklesVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.5F);
        Destroy(sparkles, 2);
    }
}
