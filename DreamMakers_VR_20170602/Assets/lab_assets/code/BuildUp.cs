using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUp : MonoBehaviour 
{
	public float speed = 1f;
	public float StartTime = 1f;
	public Transform colliderGrp;
	private Rigidbody tmpRdb;
	private Vector3 StartPos;

	// Use this for initialization
	void Awake()
	{
		StartPos = transform.position;
	}
	void Start () 
	{
		//StartBuildUp ();
	}

	void ActivateRigids(bool state)
	{
		if (colliderGrp) 
		{
			for (int i = 0; i < colliderGrp.childCount; i++) 
			{
				tmpRdb = colliderGrp.GetChild (i).GetComponent<Rigidbody> ();
				if (tmpRdb != null) 
				{
					tmpRdb.isKinematic = !state;
				}
				//print (colliderGrp.GetChild (i));
			}
			//print (colliderGrp.childCount);
		}
	}

	void ActivateColliders(bool state)
	{
		if (colliderGrp) 
		{
			for (int i = 0; i < colliderGrp.childCount; i++) 
			{
				colliderGrp.GetChild (i).GetComponent<Collider> ().enabled = state;
				//print (colliderGrp.GetChild (i));
			}
			//print (colliderGrp.childCount);
		}
	}

	public void StartBuildUp ()
	{
		transform.position = StartPos;
		StartCoroutine (BuildUpMovement());
	}

	IEnumerator BuildUpMovement()
	{
		ActivateColliders (false);
		ActivateRigids(false);
		yield return new WaitForSeconds (StartTime+1.5f); //+3f
		while (transform.localPosition.y < -0.025) 
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, Vector3.zero, Time.deltaTime * speed);
			yield return new WaitForEndOfFrame ();
		}
		//transform.localPosition = Vector3.zero;
		yield return new WaitForEndOfFrame ();
		print (gameObject.name + " DONE");
		yield return new WaitForSeconds (0.1f);
		ActivateRigids(true);
		ActivateColliders (true);

	}
}
