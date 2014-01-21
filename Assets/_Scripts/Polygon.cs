using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private Action CallbackFunction { get; set; }

    public Vector2 size;
    public Color color;

    void Start () {
        if (GenerateAtStart)
        {
            Generate();
        }
    }

    public void StartInteractivity(Action callback)
    {
        CallbackFunction = callback;
        StartCoroutine(CheckKeyDown());
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
        Debug.Log("GENERATE SHAPE"); 
        while (true)
        {
            while (!Input.GetKeyDown(KeyCode.G)) yield return null;
            GenerateRandomShape();
            CallbackFunction();
            yield return null;
        }
    }

	private void GenerateRandomShape()
	{
        int randomNumber = numberOfPoints;
        while (randomNumber == numberOfPoints) randomNumber = Random.Range(3, 8);
	    numberOfPoints = randomNumber;
        GenerateShape(numberOfPoints);
	}

    public void GenerateShape(int pointCount = -1)
    {
        if (numberOfPoints < 0 || pointCount < 0) numberOfPoints = Random.Range(3, 8);
        else numberOfPoints = pointCount < 0 ? numberOfPoints : pointCount;
        mesh = new Mesh();
        size = new Vector2(0, 0);
        mesh.name = "PolygonMesh";
        vertices = mesh.vertices = new Vector3[numberOfPoints + 1];
        triangles = mesh.triangles = new int[numberOfPoints * 3];
        float angle = -360f / numberOfPoints;
        Vector3 vertex;
        
        for (int v = 1, t = 1; v < vertices.Length; v++, t += 3)
        {
            vertex = Quaternion.Euler(0f, 0f, angle * (v - 1)) * point;
            vertices[v] = vertex;
            if (vertex.x > size.x) size.x = vertex.x;
            if (vertex.y > size.y) size.y = vertex.y;
            triangles[t] = v;
            triangles[t + 1] = v + 1;
        }

        triangles[triangles.Length - 1] = 1;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
        color = Utils_RandomColor();
        material.SetColor("_Color", color);
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
