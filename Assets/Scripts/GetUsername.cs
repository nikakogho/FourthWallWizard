using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;

public class GetUsername : MonoBehaviour
{
    //public static GetUsername instance;
    public static string username;
    public static string DisplayName => username;

    void Awake()
    {
        DontDestroyOnLoad(this);
        //instance = this;

        username = Environment.UserName;

        //May Do later
        //string path = Application.streamingAssetsPath;
        //
        //var info = new ProcessStartInfo()
        //{
        //    FileName = path + @"\DN.exe",
        //    WorkingDirectory = path
        //};
        //
        //try
        //{
        //    using(var process = Process.Start(info)) process.WaitForExit();
        //}
        //catch(Exception e)
        //{
        //    UnityEngine.Debug.Log(e);
        //}
        //
        //string filePath = path + @"\dn.txt";
        //
        //if (File.Exists(filePath)) DisplayName = File.ReadAllText(filePath);
    }
}
