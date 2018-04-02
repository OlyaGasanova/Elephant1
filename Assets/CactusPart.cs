using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CactusPart : MonoBehaviour
{

    private List<ContactPoint> _contacts = new List<ContactPoint>();
    public bool flag = false;
    private bool canContinue = false;

    public float Volume
    {
        get
        {
            return GetComponents<Collider>().Aggregate(0f, (vol, coll) => vol += coll.bounds.size.sqrMagnitude);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (flag == false)
            {
                var otherPart = contact.otherCollider.gameObject.GetComponent<CactusPart>();
                if (otherPart)
                {
                    //flag = true;

                    GameObject first = contact.thisCollider.gameObject;
                    GameObject second = contact.otherCollider.gameObject;
                    var currentJoint = first.GetComponent<CharacterJoint>();

                    if (currentJoint)
                    {
                        var currentPartnerPart = currentJoint.connectedBody.GetComponent<CactusPart>();
                        if (currentPartnerPart != null && (currentPartnerPart.name == second.name || otherPart.Volume < currentPartnerPart.Volume))
                        {
                            continue;
                        }
                    }

                    if (Volume > otherPart.Volume)
                    {
                        continue;
                    }
                    //print(Volume + " vs " + otherPart.Volume);
                    //замена на бОльшую часть
                    if (first.GetComponent<CharacterJoint>())
                    {
                        var oldConn = first.GetComponent<CharacterJoint>().connectedBody.GetComponent<CactusPart>();
                        var secConn = second.GetComponent<CactusPart>();
                        if (oldConn.Volume < secConn.Volume)
                        {
                            first.GetComponent<CharacterJoint>().connectedBody = second.GetComponent<Rigidbody>();
                        }
                    }
                    else {
                        var joint = first.AddComponent<CharacterJoint>();
                        joint.lowTwistLimit = new SoftJointLimit() { limit = 0.1f };
                        joint.highTwistLimit = new SoftJointLimit() { limit = 0.1f };
                        joint.swing1Limit = new SoftJointLimit() { limit = 0.1f };
                        joint.swing2Limit = new SoftJointLimit() { limit = 0.1f };

                        var rb = second.GetComponent<Rigidbody>();
                        joint.connectedBody = rb;
                       // print(first.name + " hit " + second.name);

                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        if (_contacts.Count > 0)
        {
            foreach (var contactPoint in _contacts)
            {
               Gizmos.color = Color.red;
              Gizmos.DrawSphere(contactPoint.point, 0.1f);
            }
        }
    }
}