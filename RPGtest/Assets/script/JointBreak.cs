using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour {

    [SerializeField] private GameObject gameObject;

    public void OnJointBreak(float breakForce)
    {
        Destroy(gameObject);
    }
}
