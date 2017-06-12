using UnityEngine;
using System.Collections;

public class UiTarget : MonoBehaviour 
{

	public delegate void TargetEnterDelegate();
	public TargetEnterDelegate OnTargetEnter;

	public GameObject m_target;
	public float speed = 5;
	private GameObject m_UiManager;

	public bool forScreenCompletion = false;

	private Vector3 targetPos;
	private float dist;

	// Use this for initialization
	void Start () 
	{
		m_UiManager = GameObject.Find("UI_MANAGER");
		targetPos = m_target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject == m_target) 
		{
			GetComponent<UiDrag> ().SetDraggableState (false);
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
		OnTargetEnter ();
		CalcDistance ();
		//for (int i = 0; i < 50; i++)
		while(dist > 0.01f)
		{
			transform.position = Vector3.Lerp (transform.position, m_target.transform.position, speed * Time.deltaTime);
			//transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, m_target.transform.eulerAngles, speed * Time.deltaTime);
			CalcDistance ();

			yield return new WaitForEndOfFrame();
		}
		transform.position = m_target.transform.position;
		if (forScreenCompletion) 
		{
			m_UiManager.GetComponent<UiManager> ().SetScreen2Completion ();
		}
	}
}
