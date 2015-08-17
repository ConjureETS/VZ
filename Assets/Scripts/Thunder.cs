using UnityEngine;
using System.Collections;

public class Thunder : MonoBehaviour 
{
	public int minInterval;
	public int maxInterval;
	public float lightningTime;

	Light light;
	// Use this for initialization
	void Start () 
	{
		light = GetComponent<Light> (); 
		StartCoroutine(GenerateLightning());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator GenerateLightning()
	{
		while(true)
		{
		
			var RandomNumber = Random.Range(minInterval,maxInterval);
			yield return new WaitForSeconds (RandomNumber);
			light.enabled = true;
			yield return new WaitForSeconds (lightningTime);
			light.enabled = false;
		}
		//else 
		
		//StartCoroutine(GenerateLightning());
		//return new WaitForSeconds (0);
	}
}
