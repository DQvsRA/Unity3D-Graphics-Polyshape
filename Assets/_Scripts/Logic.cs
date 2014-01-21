using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Logic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.Debug.Log("Logic Start");
        
        Process compiler = new Process();
        compiler.StartInfo.FileName = "BarScanner/zbarcam.exe";
        compiler.StartInfo.Arguments = "--prescale=640x480";
        compiler.StartInfo.UseShellExecute = false;
        compiler.StartInfo.RedirectStandardOutput = true;
        compiler.Start();

        string line = compiler.StandardOutput.ReadLine();
        while (line != null)
        {
            UnityEngine.Debug.Log("line:" + line);
            line = compiler.StandardOutput.ReadLine();
        }
        
        compiler.WaitForExit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
