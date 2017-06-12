using UnityEngine;
using System.Collections;

public class UiOutClouds : drop {

	public float speed = 50;
	private Vector3 targetPos;
	private float dist;


	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Outside") 
		{
			targetPos = col.gameObject.transform.position;
			StartCoroutine (SmoothTarget());
		}
	}

	void CalcDistance()
	{
		dist = transform.position.magnitude - targetPos.magnitude;
		dist = Mathf.Abs (dist);
	}

	IEnumerator SmoothTarget()
	{
		GetComponent<UiDrag> ().SetDraggableState (false);
		GetComponent<BoxCollider> ().enabled = false;
		CalcDistance ();
		//for (int i = 0; i < 500; i++) 
		while(dist > 1f)
		{
			transform.position = Vector3.Lerp (transform.position, targetPos, speed * Time.deltaTime);
			CalcDistance ();
			yield return new WaitForFixedUpdate();
		}
		transform.position = targetPos;

		Harakiri ();
	}
}
