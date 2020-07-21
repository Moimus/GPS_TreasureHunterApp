using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public Text textBox;
    public static DebugUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log(string line)
    {
        textBox.text = textBox.text + "\n" + ">" + line;
    }

    public void Clear()
    {
        textBox.text = "";
    }
}
