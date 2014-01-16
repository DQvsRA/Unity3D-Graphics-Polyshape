using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
    [Range(3, 30)] public int rows = 3;
    [Range(3, 30)] public int columns = 3;

    private GameObject go, container;
    private Polygon poly;
    
    // Use this for initialization
    private void Start()
    {
        int sides = Random.Range(3, 8);

        go = new GameObject();
        go.name = "Polygon";
        container = new GameObject();
        container.name = "Poly Container";
        poly = go.AddComponent("Polygon") as Polygon;
        poly.Generate(sides);
        poly.StartInteractivity(OnRegenerate);
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector2 size = poly.size;
        Transform ct = container.transform;
        foreach (Transform child in ct) Destroy(child.gameObject);
        go.renderer.enabled = true;
       
        Vector3 pos = new Vector3();
        ct.position = new Vector3(0, 0, 0);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                pos = new Vector3(i * size.x * 2, j * size.y * 2, 0);
                (Instantiate(go, pos, Quaternion.identity) as GameObject).transform.parent = container.transform;
            }
        }
        Debug.Log(pos);
        ct.position = new Vector3(-pos.x * 0.5f, -pos.y * 0.5f, 0);
        go.renderer.enabled = false;
    }

    private void OnRegenerate()
    {
        GenerateGrid();
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
