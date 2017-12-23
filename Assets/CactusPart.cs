using System;
using System.Collections.Generic;
using UnityEngine;

public class CactusPart : MonoBehaviour
{
    private List<ContactPoint> _contacts = new List<ContactPoint>();
    public bool flag = false;
    private bool canContinue = false;
    private void OnCollisionEnter(Collision collision)
    {
       
        foreach (ContactPoint contact in collision.contacts)
        {

            if (flag == false)
            {
                if (contact.otherCollider.gameObject.GetComponent<CactusPart>())
                {
                    flag = true;

                    GameObject First = contact.thisCollider.gameObject;
                    GameObject Second = contact.otherCollider.gameObject;

                    //замена на бОльшую часть
                    if (First.GetComponent<CharacterJoint>())
                    {
                        var firstConnColl = First.GetComponent<CharacterJoint>().connectedBody.GetComponent<Collider>();
                        var secConnColl = Second.GetComponent<Collider>();
                        if (firstConnColl.bounds.size.sqrMagnitude > secConnColl.bounds.size.sqrMagnitude) { }
                        else { First.GetComponent<CharacterJoint>().connectedBody = Second.GetComponent<Rigidbody>(); }
                    }
                    else {

                        First.AddComponent<CharacterJoint>();
                        CharacterJoint connectbody = First.GetComponent<CharacterJoint>();
                        Rigidbody rb = Second.GetComponent<Rigidbody>();
                        First.GetComponent<CharacterJoint>().connectedBody = Second.GetComponent<Rigidbody>();
                        print(contact.thisCollider.name + " hit " + contact.otherCollider.name);

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