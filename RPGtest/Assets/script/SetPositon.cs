using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetPositon : MonoBehaviour {
    //初期位置
    private Vector3 startPosition;
    //目的位置
    private Vector3 destination;

    //巡回する位置
    [SerializeField]
    private Transform[] patrolPositions;
    //次に巡回する位置
    private int nowPos = 0;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        setDestination(transform.position);

        var patrolparent = GameObject.Find("PatrolPositions");
        for(int i = 0; i < patrolparent.transform.childCount; i++)
        {
            patrolPositions[i] = patrolparent.transform.GetChild(i);
        }
	}

    public void CreateRandomPosition()
    {
        //ランダムなvecter2の値を取得する。
        var randDestination = Random.insideUnitCircle * 8;
        setDestination(startPosition + new Vector3(randDestination.x,0f,randDestination.y));
  
    }
	
    //巡回地点を順に回る
    public void NextPosition()
    {
        setDestination(patrolPositions[nowPos].position);
        nowPos++;
        if (nowPos >= patrolPositions.Length)
        {
            nowPos = 0;
        }
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
