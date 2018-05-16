using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour {

    //初期位置
    private Vector3 startPosition;
    //目的位置
    private Vector3 destination;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        SetDestination(transform.position);
	}

    public void CreateRandomPosition()
    {
        //Vector2でランダムな座標を得る。
        var randDestination = Random.insideUnitCircle * 8;

        SetDestination(startPosition + new Vector3(randDestination.x, 0, randDestination.y));
    }

    public void SetDestination(Vector3 position)
    {
       destination = position;
    }

    public Vector3 GetDestination()
    {
        return destination;
    }
}
