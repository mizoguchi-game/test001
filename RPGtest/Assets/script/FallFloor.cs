using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour {

    public enum CollisionState
    {
        Exit,
        Stay,
        Enter,
        
    }

    private CollisionState state;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setState(CollisionState dropState)
    {
        state = dropState;
    }
}
