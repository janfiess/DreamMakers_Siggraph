using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiDrag : MonoBehaviour {

	[HideInInspector]
	public float m_offsetX;
	[HideInInspector]
	public float m_offsetY;

	public float speed = 25;

	private RectTransform rTrans;
	private Vector3 mousePos;

	[HideInInspector]
	public bool m_active = true;
	private Vector3 velocity = Vector3.zero;

	private Vector3 lastPos;
	private Vector3 curPos;

	public GameObject particlePrefab;
	private GameObject particleInstance;

	private List<Vector3> positions = new List<Vector3>();

	// Use this for initialization
	void Start () 
	{
		rTrans = GetComponent<RectTransform> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (particleInstance != null) 
		{
			particleInstance.transform.position = transform.position;
		}
	}

	public void KillParticles()
	{
		if (particleInstance != null) 
		{
			Destroy (particleInstance);
		}
	}

	public void BeginDrag()
	{
		if (m_active) 
		{
			mousePos = Input.mousePosition;
			m_offsetX = rTrans.localPosition.x - mousePos.x;
			m_offsetY = rTrans.localPosition.y - mousePos.y;
			StopAllCoroutines ();
			StartCoroutine (Dragging ());
			if (particlePrefab != null) {
				particleInstance = Instantiate (particlePrefab) as GameObject;
			}
		}
	}

	public void OnDrag()
	{
		/*
		if (m_active) 
		{
			mousePos = Input.mousePosition;
			Vector3 targetPos = new Vector3 (mousePos.x + m_offsetX, mousePos.y + m_offsetY, 0);
			//rTrans.localPosition = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, 0.3f);
			rTrans.localPosition = Vector3.Lerp(rTrans.localPosition, targetPos, 0.3f);
			//rTrans.localPosition = new Vector3 (mousePos.x + m_offsetX, mousePos.y + m_offsetY, 0);
		}
		*/
	}

	public void SetDraggableState(bool state)
	{
		m_active = state;
	}

	public Vector3 GetVel()
	{
		Vector3 vel = curPos - lastPos;
		return vel;
	}

	IEnumerator Dragging()
	{
		positions.Clear ();
		while (Input.GetMouseButton(0)) 
		{
			if (m_active) 
			{

				mousePos = Input.mousePosition;
				Vector3 targetPos = new Vector3 (mousePos.x + m_offsetX, mousePos.y + m_offsetY, 0);

				//print (targetPos + "  " +  transform.position);
				Vector3 mousePosWorld = Camera.main.ScreenToViewportPoint(mousePos);
				//print(mousePosWorld);


				positions.Add (mousePosWorld);
				if (positions.Count > 10) 
				{
					positions.Remove (positions [0]);
				}

				lastPos = curPos;
				curPos = transform.position;

				//rTrans.localPosition = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, 0.3f);
				rTrans.localPosition = Vector3.Lerp (rTrans.localPosition, targetPos, speed * Time.deltaTime);
				//rTrans.localPosition = new Vector3 (mousePos.x + m_offsetX, mousePos.y + m_offsetY, 0);
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
		}
		OnDragEnd ();
	}

	void OnDragEnd()
	{
		if (m_active) 
		{
			float velocities = 0f;
			for (int i = positions.Count - 1; i > 0; i--) {
				velocities += (positions [i] - positions [i - 1]).magnitude;
			}

			float averageVel = velocities / positions.Count;
//			print (averageVel);

			Destroy (particleInstance, 1f);
			Vector3 direction = curPos - lastPos;
			float vel = direction.magnitude;
			//direction = direction.normalized;
			GetComponent<AudioSource> ().time = 0.0f; // 0.2f
			GetComponent<AudioSource> ().Play ();
			StartCoroutine (AfterDrag (direction, averageVel));
		}
	}

	IEnumerator AfterDrag(Vector3 direction, float averageVel)
	{
		while (m_active) 
		{
			transform.Translate (direction.normalized * averageVel*0.7f, Space.World);
			yield return new WaitForEndOfFrame ();
		}
	}
}
