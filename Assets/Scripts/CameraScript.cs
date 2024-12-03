using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void Start ()
	{
        SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }

	void FixedUpdate ()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}

	public void SetTarget(Transform _target)
	{
		target = _target;
	}

}