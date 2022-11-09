using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSystem : MonoBehaviour
{

    public static ToolSystem Instance { get; private set; }
    public BaseTool SelectedTool { get; private set; }

    public bool UsingTool { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
