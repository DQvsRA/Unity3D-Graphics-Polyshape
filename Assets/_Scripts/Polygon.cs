using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Polygon : MonoBehaviour {

    public Vector3 point = Vector3.up;
    [Range(-1, 10)]public int numberOfPoints = -1;
    public bool GenerateAtStart = false;

    private Mesh        mesh;
    private Material    material;
    private Shader      shader;
    private Vector3[]   vertices;
    private int[]       triangles;

    void Start () {
        if (GenerateAtStart)
        {
            StartCoroutine(CheckKeyDown());
            Generate();
        }
    }

    public void Generate(int count = -1)
    {
        shader = Shader.Find("Sprites/Default");
        material = new Material(shader);

        GetComponent<MeshRenderer>().material = material;

        GenerateShape(count);
    }

    IEnumerator CheckKeyDown()
    {
        while (true)
        {
            while (!Input.GetKeyDown(KeyCode.G)) yield return null;
            GenerateRandomShape();
            yield return null;
        }
    }

	private void GenerateRandomShape()
	{
        int randomNumber = numberOfPoints;
        while (randomNumber == numberOfPoints) randomNumber = Random.Range(4, 7);
	    numberOfPoints = randomNumber;
            
	    GenerateShape();
	}

    public void GenerateShape(int pointCount = -1)
    {
        if (numberOfPoints < 0 || pointCount < 0) numberOfPoints = Random.Range(4, 7);
        else numberOfPoints = pointCount < 0 ? numberOfPoints : pointCount;
        mesh = new Mesh();
        mesh.name = "PolygonMesh";
        vertices = mesh.vertices = new Vector3[numberOfPoints + 1];
        triangles = mesh.triangles = new int[numberOfPoints * 3];
        float angle = -360f / numberOfPoints;
        for (int v = 1, t = 1; v < vertices.Length; v++, t += 3)
        {
            vertices[v] = Quaternion.Euler(0f, 0f, angle * (v - 1)) * point;
            triangles[t] = v;
            triangles[t + 1] = v + 1;
        }

        triangles[triangles.Length - 1] = 1;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
        material.SetColor("_Color", Utils_RandomColor());
    }

    private Color Utils_RandomColor(float alpha = 1.0f)
    {
        Vector3 randomColor = Random.insideUnitSphere;
        float r = Mathf.Abs(randomColor.x);
        float g = Mathf.Abs(randomColor.y);
        float b = Mathf.Abs(randomColor.z);
        Color result = new Color(r, g, b, alpha);
        return result;
    }

    private float Utils_RandomColorFloat()
    {
        Color color = Utils_RandomColor();
        float result = color.GetHashCode();
        return result;
    }

    private Vector4 Utils_RandomColorVector(float alpha = 1.0f)
    {
        Vector3 randomColor = Random.insideUnitSphere;
        float r = Mathf.Abs(randomColor.x);
        float g = Mathf.Abs(randomColor.y);
        float b = Mathf.Abs(randomColor.z);
        Vector4 result = new Vector4(r, g, b, alpha);
        return result;
    }
}
