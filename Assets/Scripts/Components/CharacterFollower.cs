using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    public Transform character;

    private void Update()
    {
        transform.position = character.position;
    }
}
