using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager_Canvas : MonoBehaviour
{
    public GameObject InputPanel;
    public InputField CommandLine;
    public Text PrintOutput;

    private void Start()
    {
        InputPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            InputPanel.SetActive(!InputPanel.activeSelf);
            if (InputPanel.activeSelf)
            {
                CommandLine.ActivateInputField();
            }
        }
    }

    public void CommandInputCallback()
    {
        print(CommandLine.text);
        DebugManager.Instance.CommandInput(CommandLine.text);
    }

    public void Print(string s)
    {
        PrintOutput.text = s;
    }
}
