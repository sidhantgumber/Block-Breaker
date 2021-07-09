using System;
using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Block : MonoBehaviour

   
    {
        // config params

        [SerializeField] AudioClip breakingEffect;
        [SerializeField] GameObject blockSparklesVFX;
      //  [SerializeField] int maxHits;    // isko serialize karke manually set karna padh raha tha, instead neeche handlehit vaale method mei logic ke anusaar iski value arraylength+1 set karke kaam bachaaya
        [SerializeField] Sprite[] hitSprites;

        // Cached reference
        Level level;

    // state variables 
        [SerializeField] int timesHit; // only serialized for debug purposes
    
        
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
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }

        else
        {
            ShowNextHitSprites();
        }
            
    }

    private void ShowNextHitSprites()
    {
        int spriteIndex = timesHit - 1;

        // if else block mei daala taaki console mei pata lag jaaye agar block ke inspector vaali array mei sprite manually add karna bhool gaye ho
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite missing from array" +gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockIsDestroyed();

        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakingEffect, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
