using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    int sides = Random.Range(4, 7);
        for (int i = 0; i < 15; i++)
	    {
	        GameObject go = new GameObject();
	        Polygon poly = go.AddComponent("Polygon") as Polygon;
            poly.Generate(sides);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
