using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public float Distance = 5;

    public List<Interactable> Interactables = new List<Interactable>();
    public Interactable currentInteractable;

    private void Awake()
    {
        GetInteractables();
    }

    private void FixedUpdate()
    {
        
        SetOutline();
    }

    private void Update()
    {
        currentInteractable = GetClosestInteractable();


        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            if (Vector3.Distance(currentInteractable.gameObject.transform.position, gameObject.transform.position) < 5 && !currentInteractable.isDragged && 
                Interactables.Where(inter => inter.isDragged == true).ToList().Count <= 1)
            {
                currentInteractable.isDragged = true;
                currentInteractable.FollowPlayer();
            }
            else if (Vector3.Distance(currentInteractable.gameObject.transform.position, gameObject.transform.position) < 5 && currentInteractable.isDragged)
            {
                currentInteractable.Throw();
            }
        }
    }

    private void SetOutline()
    {
        foreach (var inter in Interactables)
        {
            if (currentInteractable != null)
            {
                if (inter == currentInteractable)
                {
                    if (Vector3.Distance(inter.gameObject.transform.position, gameObject.transform.position) < 5 && !inter.isDragged)
                    {
                        if (!inter.outline.enabled)
                            inter.FadeSprite(true);
                        inter.outline.enabled = true;
                    }
                    else
                    {
                        if (inter.outline.enabled)
                            inter.FadeSprite(false);
                        inter.outline.enabled = false;
                    }
                }
                else
                {
                    if (inter.outline.enabled)
                        inter.FadeSprite(false);
                    inter.outline.enabled = false;
                }
            }
            else
            {
                if (inter.outline.enabled)
                    inter.FadeSprite(false);
                inter.outline.enabled = false;
            }

        }
    }

    private Interactable GetClosestInteractable()
    {
        if (Interactables.Count <= 0) return null;

        Interactable inter = null;

        foreach (var _inter in Interactables)
        {
            if (inter == null) inter = _inter;
            else
            {
                if (Vector3.Distance(inter.gameObject.transform.position, gameObject.transform.position) > Vector3.Distance(_inter.gameObject.transform.position, gameObject.transform.position))
                {
                    inter = _inter;
                }
            }
        }

        return Vector3.Distance(inter.gameObject.transform.position, gameObject.transform.position) < Distance ? inter : null;
    }

    private void GetInteractables()
    {
        Interactables = FindObjectsOfType<Interactable>().ToList();
    }
}
