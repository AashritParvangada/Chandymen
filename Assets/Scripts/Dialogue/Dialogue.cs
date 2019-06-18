using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(1, 2)]
    public string[] S_Names;

    [TextArea(3, 10)]
    public string[] S_Sentences;

}
