﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PVR_InteractionController {

    public float range = 100;
    public ScoreController scoreController;

    [HideInInspector]
    public float timer;
    [HideInInspector]
    public bool gunSoundAllowed;
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public bool bulletsReady;
    [HideInInspector]
    public bool akMagReady;
    
    private AKScript akScript;
    private EnfieldScript enfieldScript;
    private PistolScript pistolScript;   

    public override void Awake()
    {
        base.Awake();

        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        enfieldScript = GetComponent<EnfieldScript>();
        akScript = GetComponent<AKScript>();
        pistolScript = GetComponent<PistolScript>();
        enfieldScript.enabled = false;
        akScript.enabled = false;
        akMagReady = false;
    }

    public override void Update()
    {
        base.Update();


        timer += Time.deltaTime;

        if (objectBeingInteractedWith.name == "Enfield")
        {
            enfieldScript.enabled = true;
        }
        else if(objectBeingInteractedWith.name != "Enfield")
        {
            enfieldScript.enabled = false;
        }
        if (objectBeingInteractedWith.name == "AK-47")
        {
            akScript.enabled = true;
        }
        else if(objectBeingInteractedWith.name != "AK-47")
        {
            akScript.enabled = false;
        }
        if (objectBeingInteractedWith.name == "1911")
        {
            pistolScript.enabled = true;
        }
        else if(objectBeingInteractedWith.name != "1911")
        {
            pistolScript.enabled = false;
        }
    }

    public void ShootGun(GameObject raycastBegin, double points, float shootingDelay)
    {
        timer = 0;
        Vector3 lineOrigin = raycastBegin.transform.position;
        RaycastHit hit = new RaycastHit();
        scoreController.AddToBulletCount(1);
        audioSource.Play();

        if (Physics.Raycast(lineOrigin, raycastBegin.transform.forward, out hit, range))
        {
            print("hit " + hit.collider.gameObject);
        }
        if (hit.collider.gameObject.CompareTag("Target"))
        {
            if (hit.collider.gameObject.name == "KnightCollider")
            {
                scoreController.AddScore(points);
            }
            if(hit.collider.gameObject.name == "KnightColliderHigh")
            {
                scoreController.AddScore(points * 2);
            }
            if(hit.collider.gameObject.name == "KnightColliderFar")
            {
                scoreController.AddScore(points * 5);
            }
        }
    }

    public void PlayGunSound(float shootingDelay)
    {
        if (gunSoundAllowed && timer >= shootingDelay)
        {
            audioSource.Play();
            Debug.Log("Sound should be happening");
        }
        gunSoundAllowed = false;
    }

    public void SetupSound(AudioClip shotClips)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.2f;
        audioSource.clip = shotClips;
    }
}
