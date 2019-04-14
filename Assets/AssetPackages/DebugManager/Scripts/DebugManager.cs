using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    private static DebugManager instance;
    public static DebugManager Instance
    {
        get { return instance ?? (instance = new GameObject().AddComponent<DebugManager>()); }
    }

    public delegate void ExampleEvent();
    public event ExampleEvent OnExampleEvent;

    public delegate void FPSDebugToggle();
    public event FPSDebugToggle OnFPSDebugToggle;

    DebugManager_Canvas debugCanvasRef;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gameObject.name = "Debug Manager";
    }

    private void Start()
    {
        if (debugCanvasRef == null)
        {
            CreateUI();
        }
    }

    public void Initialize()
    {
        //empty function to create a debug manager in the scene if there is not one already. this is necessary to pull up the command line
    }

    public void CommandInput(string s)
    {
        s = s.ToLowerInvariant();
        switch (s)
        {
            case "example":
                if (OnExampleEvent != null)
                {
                    OnExampleEvent.Invoke();
                }
                break;
            case "fps":
                if (OnFPSDebugToggle != null)
                {
                    OnFPSDebugToggle.Invoke();
                }
                break;
        }
    }

    public void Print(string s)
    {
        if (debugCanvasRef == null)
        {
            CreateUI();
        }
        debugCanvasRef.Print(s);
    }

    void CreateUI()
    {
        GameObject obj = Instantiate(Resources.Load("Canvas_Debug_Prefab") as GameObject);
        obj.transform.SetParent(transform);
        debugCanvasRef = obj.GetComponent<DebugManager_Canvas>();

        if (EventSystem.current == null)
        {
            GameObject e = new GameObject("Event System");
            e.AddComponent<EventSystem>();
            e.AddComponent<StandaloneInputModule>();
        }
    }
}
