using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeWatcher : MonoBehaviour
{
    public OutOfModeAction outOfModeAction;

    [Tooltip("List of modes where the object should exist")]
    public List<Mode> activeModes;

    public enum OutOfModeAction
    {
        DisableSelf,
        DestroySelf
    }
}
