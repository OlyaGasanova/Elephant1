using System.Collections.Generic;
using UnityEngine;

public class CactusPart : MonoBehaviour
{
    private List<ContactPoint> _contacts = new List<ContactPoint>();

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
            _contacts.Add(contact);
        }
        Debug.Break();
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