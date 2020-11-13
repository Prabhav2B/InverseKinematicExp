using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GlobalScript", order = 1)]
public class GlobalScript : ScriptableObject
{
    public AnimationCurve curve = new AnimationCurve();
}
