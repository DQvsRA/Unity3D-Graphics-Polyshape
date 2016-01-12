using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Logic : MonoBehaviour {

	// Use this for initialization
    public Generator generator;
    Process process = new Process();

    void Start () {
        UnityEngine.Debug.Log("Logic Start");
        CheckBarcode();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generator.DoWork("Generate");
        }
        //string line = process.StandardOutput.ReadLine();
        //if (line != null)
        //{
        //    UnityEngine.Debug.Log("line:" + line);
        //    generator.DoWork(line);
        //}
    }

    private void CheckBarcode()
    {
        UnityEngine.Debug.Log("Thread Prepare");
        //process.StartInfo.FileName = "BarScanner/zbarcam.exe";
        //process.StartInfo.Arguments = "--prescale=640x480";
        //process.StartInfo.UseShellExecute = false;
        //process.StartInfo.RedirectStandardOutput = true;
        //process.Start();
        //Threaded.RunAsync(() =>
        //{
        //UnityEngine.Debug.Log("Thread Start");
        //string line = process.StandardOutput.ReadLine();
        //while (line != null)
        //{
        //    UnityEngine.Debug.Log("line:" + line);
        //    line = process.StandardOutput.ReadLine();
        //Threaded.QueueOnMainThread(() =>
        //{
        generator.DoWork("");
            //});
            //}
            //process.WaitForInputIdle(15000);
            //process.WaitForExit();
        //});

    }
    void OnDestroy()
    {
        //process.Kill();
        //process.Dispose();
    }
}
