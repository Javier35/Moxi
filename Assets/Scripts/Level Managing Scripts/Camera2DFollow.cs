using System;
using UnityEngine;


public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
	public float offcenterX = 0;
	public float offcenterY = 1.0f;

    private Vector3 m_CurrentVelocity;
	private Vector3 TargetPos;
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
			TargetPos = target.position;
			TargetPos.x = target.position.x + offcenterX;
			TargetPos.y = target.position.y + offcenterY;
			TargetPos.z = transform.position.z;
			transform.position = TargetPos;
		}
    }

	public void StopFollowing(){
		allowFollow = false;
	}
	public void StartFollowing(){
		allowFollow = true;
	}
}

