using UnityEngine;
using System.Collections;

public class UiFloating : MonoBehaviour 
{
	public float frequency = 10f;
	public float amplitude = 0.5f;
	public float offset = 0.0f;
	public float seed = 0.5f;

	private GameObject floatingObj;
	private Transform floatingTran;
	private Vector3 startPos;
	private bool m_acvive = true;

	// Use this for initialization
	void Start () 
	{
		startPos = gameObject.transform.position;
		floatingObj = new GameObject ();
		floatingObj.name = "f_" + this.name;
		floatingTran = floatingObj.transform;

		floatingTran.position = Vector3.zero;
		floatingTran.SetParent(gameObject.transform);
		floatingTran.localEulerAngles = Vector3.zero;
		floatingTran.SetParent (gameObject.transform.parent);

		floatingTran.localScale = Vector3.one;
		transform.SetParent (floatingTran);
		transform.position = startPos;

		// subscribe to UiTarget delegate
		if (GetComponent<UiTarget> () != null) 
		{
			GetComponent<UiTarget> ().OnTargetEnter += DeactivateFloating;
		}
	}
		
	// Update is called once per frame
	/**
	 * hier documentation block für function
	 * 
	 * 
	 * */

	void DeactivateFloating()
	{
		m_acvive = false;
	}

	void FixedUpdate () 
	{
		if (m_acvive) 
		{
			Vector3 nextPos = new Vector3 (Mathf.PerlinNoise (0, offset + Time.fixedTime * Time.deltaTime * frequency) * amplitude, 0, Mathf.PerlinNoise (0, offset + seed + Time.fixedTime * Time.deltaTime * frequency) * amplitude);
			floatingTran.position = nextPos;
		}
	}
}
