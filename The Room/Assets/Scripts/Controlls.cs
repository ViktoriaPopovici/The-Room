using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controlls : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction = null;
    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint joint = null;
    private Interactable currentInteractable = null;
    private List<Interactable> contactInteractables = new List<Interactable>();

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        //Down
        if (grabAction.GetStateDown(pose.inputSource))
        {
            Pickup();
        }
        else if (grabAction.GetStateUp(pose.inputSource)) //Up
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Interactable"))
        {
            return;
        }
        contactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!other.gameObject.CompareTag("Interactable"))
        if (!other.gameObject.tag.Contains("Interactable"))
        {
            return;
        }
        contactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        currentInteractable = GetNearestInteractable();

        if (!currentInteractable)
        {
            return;
        }

        if (currentInteractable.activeControlls)
        {
            currentInteractable.activeControlls.Drop();
        }

        currentInteractable.transform.position = transform.position;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;

        currentInteractable.activeControlls = this;
    }

    public void Drop()
    {
        if (!currentInteractable)
        {
            return;
        }

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        joint.connectedBody = null;

        currentInteractable.activeControlls = null;
        currentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {

        Interactable nearest = null;
        float minDist = float.MaxValue;
        float dist = 0.0f;
        foreach (Interactable interactable in contactInteractables)
        {
            dist = (interactable.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = interactable;
            }
        }
        return nearest;
    }
}
