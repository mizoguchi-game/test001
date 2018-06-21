using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour {

    [SerializeField] private DropType dropType;
    private Rigidbody rigid;
    private bool enterDropFlag;
    [SerializeField] private float enterDropTime = 5f;
    [SerializeField] private float stayDropTime = 5f;
    private float elapsedtime = 0f;

    public enum ColliderState
    {
        Absent,
        Stay
        
    }

    private enum DropType
    {
        Stay,
        Enter,
        Time
    }

    private ColliderState state;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
        enterDropFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (dropType)
        {
            case DropType.Enter:
                EnterDropFloor();
                break;
            case DropType.Stay:
                StayDropFloor();
                break;
            default:
                break;
        }
    }

    private void EnterDropFloor()
    {
        if(state == ColliderState.Stay && enterDropFlag == false)
        {
            enterDropFlag = true;
        }

        if (enterDropFlag == false)
        {
            return;
        }
        else
        {
            elapsedtime += Time.deltaTime;

            if (enterDropTime <= elapsedtime)
            {
                rigid.isKinematic = false;
            }
        }
    }

    private void StayDropFloor()
    {
        if (state == ColliderState.Stay)
        {
            elapsedtime += Time.deltaTime;
        }
        else
        {
            elapsedtime = 0;
        }

        if (enterDropTime <= elapsedtime)
        {
            rigid.isKinematic = false;
        }

    }

    public void SetState(ColliderState dropState)
    {
        state = dropState;
    }
}
