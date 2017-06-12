using UnityEngine;
using System.Collections;

public class UiOutTarget : MonoBehaviour {

	private Vector3 startPos;
	public float speed = 5;

	// Use this for initialization
	void Start () 
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Outside") 
		{
			StartCoroutine (SmoothTarget());
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Outside") 
		{
			//StopAllCoroutines ();
			//StartCoroutine (CalculateBounce());
		}
	}

	IEnumerator CalculateBounce()
	{
		Vector3 vel = GetComponent<UiDrag> ().GetVel ();
		print ("VEL: " +  vel);
		return null;
		//return yield new WaitForEndOfFrame();
	}

	IEnumerator SmoothTarget()
	{
//		print ("tetet");
		GetComponent<UiDrag> ().SetDraggableState (false);
		GetComponent<BoxCollider> ().enabled = false;
		for (int i = 0; i < 50; i++) 
		{
			transform.position = Vector3.Lerp (transform.position, startPos, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		transform.position = startPos;
		GetComponent<UiDrag> ().SetDraggableState (true);
		GetComponent<BoxCollider> ().enabled = true;
	}
}
