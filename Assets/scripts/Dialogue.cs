using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    public string name;
    public Sprite image;
    public int functionToExecute;
    public bool isDialogueFix;

    [TextArea(3, 10), NonReorderable]
    public string[] sentences;

}
