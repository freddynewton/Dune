using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    public Outline outline;
    public float ThrowSpeed = 50;

    public bool isDragged;

    public SpriteRenderer infoSprite;

    private PlayerMovement Player;
    private InteractableManager InteractableManager;
    public Rigidbody Rigidbody;
    private SpaceshipManager SpaceShip;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>();
        SpaceShip = FindObjectOfType<SpaceshipManager>();
        InteractableManager = Player.gameObject.GetComponent<InteractableManager>();
    }

    private void Update()
    {
        CheckSpaceShipDistance();
    }

    public void FadeSprite(bool active)
    {
        if (active)
        {
            LeanTween.alpha(infoSprite.gameObject, 1f, 0.2f);
        }
        else
        {
            LeanTween.alpha(infoSprite.gameObject, 0f, 0.2f);
        }
    }

    public void FollowPlayer()
    {
        if (isDragged)
        {
            gameObject.transform.LeanMove(Player.gameObject.transform.position + new Vector3(0, 3, 0), 0.1f).setOnComplete(() => {
                FollowPlayer();
            });

        }
    }

    public void Throw()
    {
        isDragged = false;
        LeanTween.cancel(gameObject);
        var dir = Player.DirectionVelocity;

        Rigidbody.AddForce((dir == Vector3.zero ? Player.transform.forward : dir) * ThrowSpeed * Time.deltaTime, ForceMode.Impulse);

        Debug.Log("Throw " + (dir == Vector3.zero ? Player.transform.forward : dir));
    }

    private void CheckSpaceShipDistance()
    {
        if (Vector3.Distance(gameObject.transform.position, SpaceShip.gameObject.transform.position) < SpaceShip.DeliverDistance && !isDragged)
        {
            SpaceShip.Deliver();
            InteractableManager.Interactables.Remove(this);
            Destroy(gameObject);
        }
    }
}
