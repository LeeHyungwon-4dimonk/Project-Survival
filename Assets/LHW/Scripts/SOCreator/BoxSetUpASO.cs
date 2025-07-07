using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BoxSetUpA", menuName = "Assets/Create New BoxSetUpA")]
public class BoxSetUpASO : ScriptableObject
{
    public List<BoxSetup> BoxSetUpA = new List<BoxSetup>();
}

[System.Serializable]
public class BoxSetup
{
    public string ItemName;
    public float ProbType1;
    public float ProbType2;
    public float ProbType3;
    public int ItemCount;
}
