using System.Collections.Generic;
using UnityEngine;

public class CactusPart : MonoBehaviour
{
    private List<ContactPoint> _contacts = new List<ContactPoint>();
    private bool flag = false;
    private void OnCollisionEnter(Collision collision)
    {
       
        foreach (ContactPoint contact in collision.contacts)
        {

            if (flag == false)
            {
                flag = true;
                if (!contact.otherCollider.gameObject.GetComponent<CharacterJoint>())
                {
                    GameObject First = contact.thisCollider.gameObject;
                    GameObject Second = contact.otherCollider.gameObject;
                    First.AddComponent<CharacterJoint>();
                    CharacterJoint connectbody = First.GetComponent<CharacterJoint>();
                    Rigidbody rb = Second.GetComponent<Rigidbody>();
                    First.GetComponent<CharacterJoint>().connectedBody = Second.GetComponent<Rigidbody>();
                   // Debug.DrawRay(contact.point, contact.normal, Color.white);
                    print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                    //First.GetComponent<Renderer>().material.SetColor("_Albedo", Color.black);
                    // Second.GetComponent<Renderer>().material.SetColor("_Albedo", Color.white);
                    //Physics.IgnoreCollision(contact.thisCollider, contact.otherCollider, true);
                }
                //Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
        }
        //Debug.Break();
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