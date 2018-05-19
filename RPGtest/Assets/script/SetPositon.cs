using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositon : MonoBehaviour {
    //初期位置
    private Vector3 startPosition;
    //目的位置
    private Vector3 destination;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        setDestination(transform.position);
	}

    public void CreateRandomPosition()
    {
        //ランダムなvecter2の値を取得する。
        var randDestination = Random.insideUnitCircle * 8;
        setDestination(startPosition + new Vector3(randDestination.x,0f,randDestination.y));
  
    }
	
    public void setDestination(Vector3 position)
    {
        destination = position;
    }

    public Vector3 GetDestination()
    {
        return destination;
    }
}
