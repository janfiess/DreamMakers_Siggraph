using System.Collections;
using UnityEngine;

public class Game_LetterGizmo : MonoBehaviour {

	public float gizmoSize = 0.1f;
	public Color gizmoColor = Color.yellow;
	void OnDrawGizmos(){
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere(transform.position, gizmoSize);
	}
}
