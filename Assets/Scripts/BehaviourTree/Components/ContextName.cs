using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ContextName")]
public class ContextName : ScriptableObject
{
#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "";
#endif
}
