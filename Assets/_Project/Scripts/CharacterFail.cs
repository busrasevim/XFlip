using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFail : MonoBehaviour
{
    public Character character;

    private void Update()
    {
        transform.rotation = transform.parent.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //karakter fail
            Debug.Log("character failed");

            character.EndLevel(false);
        }
    }
}
