using UnityEngine;

public class CharacterFail : MonoBehaviour
{
    [SerializeField] private Character character;

    private void Update()
    {
        transform.rotation = transform.parent.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //karakter fail
            character.EndLevel(false, true);
        }
    }
}
