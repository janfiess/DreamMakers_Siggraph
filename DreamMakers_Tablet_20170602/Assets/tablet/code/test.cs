using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	[HideInInspector]
	public float m_offsetX;
	[HideInInspector]
	public float m_offsetY;

	private RectTransform rTrans;
	private Vector3 mousePos;

	private Vector3 lastPos;
	private Vector3 curPos;
	private Vector3 newVel;

	// Use this for initialization
	void Start () 
	{
		rTrans = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetMouseButtonUp (0)) 
		{
			SetVelocity ();
		}

	}

	public void OnDragBegin()
	{
		mousePos = Input.mousePosition;
		m_offsetX = rTrans.localPosition.x - mousePos.x;
		m_offsetY = rTrans.localPosition.y - mousePos.y;
	}

	public void OnDrag()
	{
		lastPos = transform.position;
		mousePos = Input.mousePosition;
		Vector3 targetPos = new Vector3 (mousePos.x + m_offsetX, mousePos.y + m_offsetY, 0);
		print (targetPos);
		rTrans.localPosition = targetPos;
	}

	private void SetVelocity()
	{
		curPos = transform.position;
		newVel = (curPos - lastPos) * Time.deltaTime * 100;
		lastPos = curPos;
		GetComponent<Rigidbody2D>().velocity = newVel;
		print (newVel);
	}
}
