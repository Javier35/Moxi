using System;
using UnityEngine;


public class Camera2DFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 m_CurrentVelocity;
	private Vector3 aheadTargetPos;

	private bool allowFollow = true;

    // Use this for initialization
    private void Start()
    {
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
		if (allowFollow) {
			aheadTargetPos = target.position;
			aheadTargetPos.y = target.position.y + 1.0f;
			aheadTargetPos.z = -1;
			transform.position = aheadTargetPos;
		}
    }

	public void StopFollowing(){
		allowFollow = false;
	}
	public void StartFollowing(){
		allowFollow = true;
	}
}

