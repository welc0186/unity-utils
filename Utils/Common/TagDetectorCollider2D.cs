using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{
[RequireComponent(typeof(Collider2D))]
public class TagDetectorCollider2D : MonoBehaviour
{
    
    Collider2D _collider2D;
    public bool Detected = false;
    public string Tag = "None";
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == Tag)
            Detected = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Detected = false;
    }

}
}