using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

[RequireComponent(typeof(Animator))]
public class AnimatorSelfDestruct : MonoBehaviour
{
    
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            Destroy(gameObject);
    }

}
}