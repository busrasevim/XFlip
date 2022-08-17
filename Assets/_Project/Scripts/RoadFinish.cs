using UnityEngine;

public class RoadFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character _char = other.GetComponentInParent<Character>();
        if (_char == null) return;

        _char.EndLevel(_char.characterOrder == 1);
    }
}
