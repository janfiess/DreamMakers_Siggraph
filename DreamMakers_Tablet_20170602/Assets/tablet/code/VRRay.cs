using UnityEngine;
using System.Collections;

public class VRRay : MonoBehaviour 
{
	public Camera GuiCam;
	public Camera VrCam;
	public GameObject VrTexture;


	public Vector3 GetVRPoint(Vector3 pos)
	{
		Vector3 vrPos = Vector3.zero;
		Ray ray = GuiCam.ScreenPointToRay (pos);
		//Ray ray = GuiCam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == VrTexture)
		{
			Vector3 texPos = hit.textureCoord;
//			print (texPos);
			ray = VrCam.ViewportPointToRay(texPos);
			if (Physics.Raycast(ray, out hit))
			{
				vrPos = hit.point;
				// hit should now contain information about what was hit through secondCamera
			}
			
		}
		return vrPos;
	}
}