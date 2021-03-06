﻿using System;
using System.Linq;
using UnityEngine;
using Parse;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

public class Generator : MonoBehaviour
{
    private int _prevRows;
    private int _prevCols;

    [Range(3, 30)] public int rows = 3;
    [Range(3, 30)] public int columns = 3;

    private GameObject go, container;
    private Polygon poly;
    private string userline = "initialize";

    // Use this for initialization
    private void Start()
    {
        int sides = Random.Range(3, 8);

        _prevRows = rows;
        _prevCols = columns;

        go = new GameObject();
        go.name = "Polygon";
        container = new GameObject();
        container.name = "Poly Container";
        poly = go.AddComponent<Polygon>() as Polygon;
        poly.Generate(sides);
        poly.StartInteractivity(OnRegenerate);
        OnRegenerate();

        ParseAnalytics.TrackAppOpenedAsync();
    }

    private void GenerateGrid()
    {
        Vector2 size = poly.size;
        Transform ct = container.transform;
        foreach (Transform child in ct) Destroy(child.gameObject);
        go.GetComponent<Renderer>().enabled = true;
       
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
        
        ct.position = new Vector3(-pos.x * 0.5f, -pos.y * 0.5f, 0);
        go.GetComponent<Renderer>().enabled = false;
    }

    public void DoWork(string line)
    {
        userline = line;
        Debug.Log("DOWORK -> userline: " + userline);
        int sides = Random.Range(3, 8);
        poly.GenerateShape();
        OnRegenerate();
    }

    private void OnRegenerate()
    {
        GenerateGrid();
        ManageAnalytics();
    }

    private void ManageAnalytics()
    {
        Color color = poly.color;
        ParseObject polygonPO = new ParseObject("Polygon");

        int score = Random.Range((int)0, (int)10000);
        string str = "The User Has Score: " + Random.Range((int)0, (int)10000).ToString();
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes("Welcome aboard!");
        IList<object> list = new List<object> { str, score };
        IDictionary<string, object> dictionary = new Dictionary<string, object>
        {
            { "number", Random.Range((int)0, (int)10000) },
            { "string", str }
        };

        var polyGridDictionary = new Dictionary<string, string> {
          { "sides", Convert.ToString(poly.numberOfPoints) },
          { "grid", Convert.ToString(rows.ToString() + "x" + columns.ToString())},
          { "color", Convert.ToString(color.r.ToString() + ";" + color.g.ToString() + ";" + color.b.ToString())},
          { "uuid", userline }
        };
        polygonPO["sides"] = poly.numberOfPoints; // number
        polygonPO["score"] = score; // number
        polygonPO["color"] = polyGridDictionary["color"]; // string
        polygonPO["bytes"] = bytes; // array-bytes
        polygonPO["finished"] = false; // boolean
        polygonPO["dictionary"] = dictionary; // object
        polygonPO["list"] = list; // object
        polygonPO["date"] = DateTime.Now; // date

        var gameScore = new ParseObject("GameScore")
        {
            { "score", Random.Range((int)0, (int)10000) },
            { "playerName", "Sean Plott" },
            { "cheatMode", false },
            { "skills", new List<string> { "pwnage", "flying" } },
        };
        
        ParseAnalytics.TrackEventAsync("GeneratePolyGrid", polyGridDictionary).ContinueWith(b=> {
            gameScore.SaveAsync().ContinueWith(t => {
                polygonPO.SaveAsync();
            });
        });
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
