using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Blob
{
    public Camera camera;
	
    // Start is called before the first frame update
    void Start()
    {
		GetCommonComponents();
    }

    // Update is called once per frame
    void Update()
	{
		
		Vector3 worldPoint = GetWorldTouchPosition();
		Vector3 newForward = (worldPoint - transform.position);
		if (newForward.magnitude > 10e-3f)
			transform.forward = newForward.normalized;
		direction = transform.forward;
		BlobUpdate();
	}

	private Vector3 GetWorldTouchPosition()
	{
		Vector3 viewPortPoint = camera.ScreenToViewportPoint(Input.mousePosition);
		viewPortPoint.x = Mathf.Clamp01(viewPortPoint.x);
		viewPortPoint.y = Mathf.Clamp01(viewPortPoint.y);
		viewPortPoint.z = camera.WorldToViewportPoint(transform.position).z;
		Vector3 worldPoint = camera.ViewportToWorldPoint(viewPortPoint);
		worldPoint.y = transform.position.y;
		return worldPoint;
	}
	private void OnCollisionEnter(Collision collision)
	{
		HandleCollision(collision);
	}
}
